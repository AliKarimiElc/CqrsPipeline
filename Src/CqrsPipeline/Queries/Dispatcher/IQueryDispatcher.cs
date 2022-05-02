namespace CqrsPipeline.Queries.Dispatcher;

/// <summary>
/// Contract for dispatcher of queries.
/// </summary>
public interface IQueryDispatcher
{
    /// <summary>
    /// Send queries to query handler for execution and return back result of it
    /// </summary>
    /// <param name="query"></param>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TQueryResult"></typeparam>
    /// <returns></returns>
    public Task<TQueryResult> SendAsync<TQuery, TData, TQueryResult>(TQuery query)
        where TQuery : class, IQuery<TData>
        where TQueryResult : QueryResult<TData>;
}