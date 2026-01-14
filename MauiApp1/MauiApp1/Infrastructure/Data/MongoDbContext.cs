using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MauiApp1.Infrastructure.Data;

/// <summary>
/// MongoDB context for managing database connections and collections
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client;

    public MongoDbContext(IOptions<Configuration.MongoDbSettings> settings)
    {
        var mongoSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
        mongoSettings.MaxConnectionPoolSize = settings.Value.MaxConnectionPoolSize;
        mongoSettings.MinConnectionPoolSize = settings.Value.MinConnectionPoolSize;
        mongoSettings.ServerSelectionTimeout = TimeSpan.FromMilliseconds(settings.Value.ServerSelectionTimeoutMs);

        _client = new MongoClient(mongoSettings);
        _database = _client.GetDatabase(settings.Value.DatabaseName);
    }

    /// <summary>
    /// Get a MongoDB collection
    /// </summary>
    public IMongoCollection<T> GetCollection<T>(string? collectionName = null)
    {
        return _database.GetCollection<T>(collectionName ?? typeof(T).Name);
    }

    /// <summary>
    /// Get the database instance
    /// </summary>
    public IMongoDatabase Database => _database;
}
