using MauiApp1.Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace MauiApp1.Core.Entities;

/// <summary>
/// Example User entity - you can modify or create your own entities
/// </summary>
[BsonCollection("users")]
public class User : BaseEntity
{
    [BsonElement("username")]
    public string Username { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("fullName")]
    public string FullName { get; set; } = string.Empty;

    [BsonElement("dateOfBirth")]
    public DateTime DateOfBirth { get; set; }

    [BsonElement("avatarUrl")]
    public string AvatarUrl { get; set; } = string.Empty;
}

/// <summary>
/// Attribute to specify MongoDB collection name
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class BsonCollectionAttribute : Attribute
{
    public string CollectionName { get; }

    public BsonCollectionAttribute(string collectionName)
    {
        CollectionName = collectionName;
    }
}
