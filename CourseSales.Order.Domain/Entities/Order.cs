using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseSales.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = default!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AdressId { get; set; }
        public decimal TotalPrice { get; set; }
        public float? DiscountRate { get; set; }
        public Guid? PaymentId { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public Adress Adress { get; set; } = null!;
        public static string GenerateCode()
        {
            Random rnd = new Random();
            var orderCode = new StringBuilder("10");
            for (int i = 0; i < 8; i++)
            {
                orderCode.Append(rnd.Next(10));
            }
            return orderCode.ToString();

        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate, int adressId)
        {
            return new Order()
            {
                Id = NewId.NextSequentialGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                AdressId = adressId,
                Created = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0,
                DiscountRate = discountRate,
            };
        }
        public static Order CreateUnPaidOrder(Guid buyerId, float? discountRate)
        {
            return new Order()
            {
                Id = NewId.NextSequentialGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                TotalPrice = 0,
                DiscountRate = discountRate,
            };
        }
        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);
            CalculateTotalPrice();
        }
        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(x => x.UnitPrice);
            if (DiscountRate.HasValue)
            {
                TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
            }
        }
        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentNullException(nameof(discountPercentage), "İndirim oranı 0-100 arası olmalıdır");
            }
            DiscountRate = discountPercentage;
            CalculateTotalPrice();
        }

        public void SetPaidStatus(Guid paymenId)
        {
            Status = OrderStatus.Paid;
            this.PaymentId = paymenId;
        }
    }
}
