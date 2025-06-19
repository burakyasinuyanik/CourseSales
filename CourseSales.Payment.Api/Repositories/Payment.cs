using MassTransit;

namespace CourseSales.Payment.Api.Repositories
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public DateTime Created { get; set; }
        public Decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(Guid userId, string orderCode, decimal amount)
        {

            Create(userId, orderCode, amount);

        }

        public void Create(Guid userId, string orderCode, decimal amount)
        {
            Id = NewId.NextSequentialGuid();
            Created = DateTime.UtcNow;
            UserId = userId;
            OrderCode = orderCode;
            Amount = amount;
            Status = PaymentStatus.Pendding;


        }

        public void SetStatus(PaymentStatus status)
        {
            Status = status;
        }

    }
    public enum PaymentStatus
    {
        Success = 1,
        Failed = 2,
        Pendding = 3
    }
}
