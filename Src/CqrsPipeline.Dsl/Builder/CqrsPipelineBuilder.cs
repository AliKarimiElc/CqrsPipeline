using CqrsPipeline.Commands;
using CqrsPipeline.Pipeline;
using CqrsPipeline.Pipeline.Command;
using CqrsPipeline.Pipeline.Query;
using CqrsPipeline = CqrsPipeline.Pipeline.CqrsPipeline;

namespace CqrsPipeline.Dsl.Builder;

public class CqrsPipelineBuilder
{
    private readonly ICommandPipeline _commandPipeline;
    private readonly IQueryPipeline _queryPipeline;
    private readonly ICqrsPipeline _pipeline;

    public CqrsPipelineBuilder()
    {
        var resolver = new DependencyResolver();
        _pipeline = new CqrsPipeline(new CommandPipeline(resolver),new QueryPipeline(resolver));
    }
}