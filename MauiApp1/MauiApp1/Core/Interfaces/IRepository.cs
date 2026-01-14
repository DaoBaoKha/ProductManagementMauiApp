using System.Linq.Expressions;
using MauiApp1.Core.Entities;

namespace MauiApp1.Core.Interfaces;

/// <summary>
/// Generic repository interface for CRUD operations
/// </summary>
/// <typeparam name="T">Entity type that inherits from BaseEntity</typeparam>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Get entity by ID
    /// </summary>
    Task<T?> GetByIdAsync(string id);

    /// <summary>
    /// Get all entities (non-deleted)
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Find entities matching a predicate
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Add new entity
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Update existing entity
    /// </summary>
    Task UpdateAsync(string id, T entity);

    /// <summary>
    /// Soft delete entity (mark as deleted)
    /// </summary>
    Task DeleteAsync(string id);

    /// <summary>
    /// Hard delete entity (permanently remove)
    /// </summary>
    Task HardDeleteAsync(string id);

    /// <summary>
    /// Count entities matching a predicate
    /// </summary>
    Task<long> CountAsync(Expression<Func<T, bool>>? predicate = null);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    Task<bool> ExistsAsync(string id);
}
