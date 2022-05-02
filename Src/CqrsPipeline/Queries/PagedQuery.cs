namespace CqrsPipeline.Queries;

/// <summary>
/// The query container type for PagedQueries , When query data is list and you need pagination, use it
/// </summary>
/// <typeparam name="TData">Expected data from this query</typeparam>
public class PagedQuery<TData> : IQuery<TData>
{
    /// <summary>
    /// Expected page number
    /// </summary>
    public long Page { get; set; }
    /// <summary>
    /// Expected number of records per page
    /// </summary>
    public long Records { get; set; }
}