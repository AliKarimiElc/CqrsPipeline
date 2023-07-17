using CqrsPipeline.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Dsl;

public interface IPipelineRootDsl
{
    public CommandDsl<TCommand> AddCommand<TCommand>() where TCommand : ICommand;
}

public static class PipelineStartupExtension
{
    public static Pipeline GetPipeline(this IServiceCollection service)
    {
        return new Pipeline(service);
    }

    public static Pipeline GetPipeline(this IServiceProvider service)
    {
        return service.GetRequiredService<Pipeline>();
    }
}