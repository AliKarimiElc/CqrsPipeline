using System.Linq.Expressions;

namespace CqrsPipeline.DataAccess
{
    /// <summary>
    /// Contract of command repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICommandRepository<TEntity, in TKey>
        where TEntity : Entity<TKey> where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Find entity by it`s key async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(TKey key);
        /// <summary>
        /// Find entity with custom condition async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Check entity with expected condition exist or not async
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// Insert new entity async
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity aggregate);
        /// <summary>
        /// Insert a range of entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task CreateRangeAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// Update entity async
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity aggregate);
        /// <summary>
        /// Delete entity async
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        Task<TEntity> DeleteAsync(TEntity aggregate);
        /// <summary>
        /// Delete a range of entity async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task DeleteRange(IEnumerable<TEntity> entities);
        /// <summary>
        /// Check one of the entity field is unique in the set or not async
        /// </summary>
        /// <param name="checkFunction"></param>
        /// <returns></returns>
        Task<bool> IsFieldValueUnique(Expression<Func<TEntity, bool>> checkFunction);
    }
}