using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CqrsPipeline.DataAccess.EntityFramework.SqlServer
{
    /// <summary>
    /// Default implementation of ICommandRepository for entity framework core with sql server database
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CommandRepository<TDbContext,TEntity, TKey> : ICommandRepository<TEntity, TKey>
        where TKey : IEquatable<TKey> where TEntity : Entity<TKey> where TDbContext : DbContext
    {
        /// <summary>
        /// DbContext
        /// </summary>
        protected readonly TDbContext DbContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        protected CommandRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(TKey key)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(x=>x.Id.Equals(key));
        }

        /// <inheritdoc />
        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().AnyAsync(predicate);
        }

        /// <inheritdoc />
        public async Task<TEntity> CreateAsync(TEntity aggregate)
        {
            return (await DbContext.Set<TEntity>().AddAsync(aggregate)).Entity;
        }

        /// <inheritdoc />
        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        /// <inheritdoc />
        public async Task<TEntity> UpdateAsync(TEntity aggregate)
        {
            return await Task.Run(() => DbContext.Set<TEntity>().Update(aggregate).Entity);
        }

        /// <inheritdoc />
        public Task<TEntity> DeleteAsync(TEntity aggregate)
        {
            return Task.Run(() => DbContext.Set<TEntity>().Remove(aggregate).Entity);
        }

        /// <inheritdoc />
        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => DbContext.Set<TEntity>().RemoveRange(entities));
        }

        /// <inheritdoc />
        public async Task<bool> IsFieldValueUnique(Expression<Func<TEntity, bool>> checkFunction)
        {
            try
            {
                var item = await DbContext.Set<TEntity>().SingleOrDefaultAsync(checkFunction);
                return (item is null);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}