namespace MauiApp1.Infrastructure.Configuration;

/// <summary>
/// MongoDB connection settings
/// </summary>
public class MongoDbSettings
{
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "MauiAppDb";
    public int MaxConnectionPoolSize { get; set; } = 100;
    public int MinConnectionPoolSize { get; set; } = 10;
    public int ServerSelectionTimeoutMs { get; set; } = 5000;
}
