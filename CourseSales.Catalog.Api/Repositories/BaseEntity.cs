using MongoDB.Bson.Serialization.Attributes;

namespace CourseSales.Catalog.Api.Repositories
{
    public class BaseEntity
    {
        //mongodb tarafındaki id sütun erişimi snow flakes kullanacağız indexlemeyi kolaylaştırır 
        [BsonElement("_id")]
        public Guid Id { get; set; }
    }
}
