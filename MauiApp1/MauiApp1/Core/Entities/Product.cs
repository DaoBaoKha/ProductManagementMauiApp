using MongoDB.Bson.Serialization.Attributes;

namespace MauiApp1.Core.Entities
{
    public class Product : BaseEntity
    {

        [BsonElement("product_id")]
        public string ProductId { get; set; } = string.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("category")]
        public string Category { get; set; } = string.Empty;

        [BsonElement("price")]
        public int Price { get; set; }

        [BsonElement("stock")]
        public int Stock { get; set; }

    }
}
