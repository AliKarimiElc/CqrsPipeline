namespace CqrsPipeline.Queries;

/// <summary>
/// Base class for query results
/// </summary>
/// <typeparam name="TData"></typeparam>
public class QueryResult<TData>
{
    /// <summary>
    /// Result of the query
    /// </summary>
    public TData? Data { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="data"></param>
    public QueryResult(TData? data)
    {
        Data = data;
    }
}