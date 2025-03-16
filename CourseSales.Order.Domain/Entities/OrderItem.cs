namespace CourseSales.Order.Domain.Entities
{
    public class OrderItem : BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;

        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(productName))
            {
                throw new ArgumentNullException(nameof(productName), "Ürün adı boş olamaz.");
            }
            if (UnitPrice < 0)
            {
                throw new ArgumentOutOfRangeException("Ürün fiyatı 0'dan küçük olamaz.");
            }

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
        public void UpdatePrice(decimal newPrice)
        {

            if (newPrice < 0)
            {
                throw new ArgumentOutOfRangeException("Fiyat 0 dan küçük olamaz");
            }
            this.UnitPrice += newPrice;

        }

        public void ApplyDiscount(double discountPercentage)
        {

            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentOutOfRangeException("İndirim oranı 100 ile 0 arasında olmalıdır.");
            }

            this.UnitPrice -= (this.UnitPrice * ((decimal)discountPercentage / 100));
        }

        public bool IsSameItem(OrderItem otherItem)
        {
            return this.ProductId == otherItem.ProductId;
        }
    }
}
