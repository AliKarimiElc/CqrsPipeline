using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Queries.Dispatcher;

/// <summary>
/// Default implementation of query dispatcher
/// </summary>
public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceScopeFactory _serviceFactory;
    
    /// <summary>
    /// Create instance of query dispatcher
    /// </summary>
    /// <param name="serviceScopeFactory"></param>
    public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Send query to query handler
    /// </summary>
    /// <param name="query"></param>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TQueryResult"></typeparam>
    /// <returns></returns>
    public async Task<TQueryResult> SendAsync<TQuery, TData, TQueryResult>(TQuery query) 
        where TQuery : class, IQuery<TData> where TQueryResult : QueryResult<TData>
    {
        using var serviceScope = _serviceFactory.CreateScope();
        var handler = serviceScope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TData, TQueryResult>>();
        return await handler.ExecuteAsync(query);
    }
}