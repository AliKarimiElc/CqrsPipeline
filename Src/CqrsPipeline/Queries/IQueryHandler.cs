namespace CqrsPipeline.Queries
{
    /// <summary>
    /// IQueryHandler is a definition for query handler that handle specific query and return QueryResult
    /// </summary>
    /// <typeparam name="TQuery">Type of query that implement IQuery</typeparam>
    /// <typeparam name="TData">The type of expected data from query</typeparam>
    /// <typeparam name="TQueryResult">The type of QueryResult that data containerized with it</typeparam>
    public interface IQueryHandler<in TQuery, TData, TQueryResult>
        where TQuery : class, IQuery<TData> where TQueryResult : QueryResult<TData>
    {
        /// <summary>
        /// Execute query , async
        /// </summary>
        /// <param name="query">Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TQueryResult> ExecuteAsync(TQuery query,
            CancellationToken cancellationToken = new CancellationToken());
    }
}