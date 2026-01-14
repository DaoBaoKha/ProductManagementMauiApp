using System.Linq.Expressions;
using MauiApp1.Core.Entities;
using MauiApp1.Core.Interfaces;
using MongoDB.Driver;

namespace MauiApp1.Infrastructure.Data.Repositories;

/// <summary>
/// Base MongoDB repository implementation
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class MongoRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbContext context, string? collectionName = null)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    public virtual async Task<T?> GetByIdAsync(string id)
    {
        return await _collection
            .Find(x => x.Id == id && !x.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection
            .Find(x => !x.IsDeleted)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection
            .Find(Builders<T>.Filter.And(
                Builders<T>.Filter.Where(predicate),
                Builders<T>.Filter.Eq(x => x.IsDeleted, false)))
            .ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = null;
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public virtual async Task UpdateAsync(string id, T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        await _collection.ReplaceOneAsync(x => x.Id == id && !x.IsDeleted, entity);
    }

    public virtual async Task DeleteAsync(string id)
    {
        var update = Builders<T>.Update
            .Set(x => x.IsDeleted, true)
            .Set(x => x.UpdatedAt, DateTime.UtcNow);

        await _collection.UpdateOneAsync(x => x.Id == id, update);
    }

    public virtual async Task HardDeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(x => x.Id == id);
    }

    public virtual async Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return await _collection.CountDocumentsAsync(x => !x.IsDeleted);
        }

        return await _collection.CountDocumentsAsync(
            Builders<T>.Filter.And(
                Builders<T>.Filter.Where(predicate),
                Builders<T>.Filter.Eq(x => x.IsDeleted, false)));
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        return await _collection
            .Find(x => x.Id == id && !x.IsDeleted)
            .AnyAsync();
    }
}
