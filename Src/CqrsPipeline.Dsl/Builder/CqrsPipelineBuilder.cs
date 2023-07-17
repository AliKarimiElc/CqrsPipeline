using CqrsPipeline.Pipeline.Command;
using CqrsPipeline.Pipeline.Query;

namespace CqrsPipeline.Dsl.Builder;

internal class CqrsPipelineBuilder
{
    private readonly ICommandPipeline _commandPipeline;
    private readonly IQueryPipeline _queryPipeline;



    public CqrsPipelineBuilder(IDependencyResolver dependencyResolver)
    {
        _commandPipeline = dependencyResolver.Resolve<ICommandPipeline>();
        _queryPipeline = dependencyResolver.Resolve<IQueryPipeline>();
    }

    public 
}