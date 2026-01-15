using MongoDB.Bson.Serialization.Attributes;

namespace MauiApp1.Core.Entities
{
    public class User : BaseEntity
    {

        [BsonElement("user_id")]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = string.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = string.Empty;

    }
}
