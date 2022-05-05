using Microsoft.EntityFrameworkCore;

namespace CqrsPipeline.DataAccess.EntityFramework.SqlServer
{
    /// <summary>
    /// Default implementation of IQueryRepository
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class QueryRepository<TDbContext,TEntity, TKey> : IQueryRepository<TEntity, TKey>
        where TEntity : Entity<TKey> where TKey : IEquatable<TKey> where TDbContext : DbContext
    {
        /// <summary>
        /// DbContext
        /// </summary>
        protected readonly TDbContext DbContext;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext"></param>
        protected QueryRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}