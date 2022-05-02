using System.Collections;

namespace CqrsPipeline.Queries;

/// <summary>
/// PagedQueryResult , This is a queryResult containerType that you use it when query is a pageQuery or even you need pagination
/// </summary>
/// <typeparam name="TData"></typeparam>
public class PagedQueryResult<TData> : QueryResult<TData> where TData : ICollection
{
    /// <summary>
    /// Number of total record count of ExpectedData
    /// </summary>
    public long TotalRecordCount { get; set; }
    /// <summary>
    /// Number of record that match with query parameters and filters
    /// </summary>
    public long FilteredRecordCount { get; set; }
    /// <summary>
    /// Number of loaded records in this query
    /// </summary>
    public long CurrentRecordCount { get; set; }

    /// <summary>
    /// Create instance of PagedQueryResult
    /// </summary>
    /// <param name="data"></param>
    /// <param name="totalRecordCount"></param>
    /// <param name="filteredRecordCount"></param>
    public PagedQueryResult(TData data, long totalRecordCount, long filteredRecordCount) : base(data)
    {
        CurrentRecordCount = data.Count;
        FilteredRecordCount = filteredRecordCount;
        TotalRecordCount = totalRecordCount;
    }
}