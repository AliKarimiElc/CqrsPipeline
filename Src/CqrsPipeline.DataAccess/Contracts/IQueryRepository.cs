namespace CqrsPipeline.DataAccess
{
    /// <summary>
    /// Contract of query repositories
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IQueryRepository<TEntity, TKey> where TEntity : Entity<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}