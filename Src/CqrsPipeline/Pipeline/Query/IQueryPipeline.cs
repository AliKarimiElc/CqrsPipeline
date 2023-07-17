using CqrsPipeline.Queries;

namespace CqrsPipeline.Pipeline.Query;

public interface IQueryPipeline
{
    Task<TQueryResult> DispatchQueryAsync<TQuery, TData, TQueryResult>(TQuery query,
        CancellationToken cancellationToken = new CancellationToken())
        where TQuery : class, IQuery<TData>
        where TQueryResult : QueryResult<TData>;
}