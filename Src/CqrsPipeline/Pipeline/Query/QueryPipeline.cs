using CqrsPipeline.Queries;
using System.Collections.Concurrent;

namespace CqrsPipeline.Pipeline.Query;

public class QueryPipeline : IQueryPipeline
{
    private ConcurrentDictionary<Type, Type> _queryHandlers = new();

    private readonly IDependencyResolver _resolver;

    public QueryPipeline(IDependencyResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task<TQueryResult> DispatchQueryAsync<TQuery, TData, TQueryResult>(TQuery query,
        CancellationToken cancellationToken = new())
        where TQuery : class, IQuery<TData>
        where TQueryResult : QueryResult<TData>
    {
        var handler = _resolver.Resolve<IQueryHandler<TQuery, TData, TQueryResult>>();
        return await handler.ExecuteAsync(query, cancellationToken);
    }
}