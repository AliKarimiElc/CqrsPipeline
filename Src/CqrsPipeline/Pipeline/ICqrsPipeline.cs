using CqrsPipeline.Pipeline.Command;
using CqrsPipeline.Pipeline.Query;

namespace CqrsPipeline.Pipeline;

/// <summary>
/// Use can use this for implement your own pipeline
/// </summary>
public interface ICqrsPipeline : ICommandPipeline, IQueryPipeline
{
}