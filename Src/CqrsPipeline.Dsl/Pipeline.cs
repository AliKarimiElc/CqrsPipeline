using CqrsPipeline.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Dsl;

public class Pipeline : IPipelineRootDsl
{
    private readonly IServiceCollection _service;

    public Pipeline(IServiceCollection service)
    {
        _service = service;
    }

    public CommandDsl<TCommand> AddCommand<TCommand>() where TCommand : ICommand
    {
        return new CommandDsl<TCommand>(_service);
    }
}