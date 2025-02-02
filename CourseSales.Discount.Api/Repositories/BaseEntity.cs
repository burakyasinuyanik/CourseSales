using MongoDB.Bson.Serialization.Attributes;

namespace CourseSales.Discount.Api.Repositories
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
