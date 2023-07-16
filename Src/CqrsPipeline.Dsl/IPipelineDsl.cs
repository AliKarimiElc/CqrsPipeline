using CqrsPipeline.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace CqrsPipeline.Dsl;

public interface IPipelineRootDsl
{
    public CommandDsl<TCommand> AddCommand<TCommand>() where TCommand : ICommand;
}

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

public class CommandDsl<TCommand> where TCommand : ICommand
{
    private readonly IServiceCollection _serviceCollection;

    internal CommandDsl(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public CommandHandlerDsl<TCommand> ValidateWith<TValidator>() where TValidator : class, IValidator<TCommand>
    {
        _serviceCollection.AddTransient<TValidator>();
        _serviceCollection.AddTransient<IValidator<TCommand>, TValidator>();
        return new CommandHandlerDsl<TCommand>(_serviceCollection);
    }

    public CommandDsl<TCommand> HandleWith<TCommandHandler>() where TCommandHandler : class, ICommandHandler<TCommand>
    {
        _serviceCollection.AddTransient<TCommandHandler>();
        _serviceCollection.AddTransient<ICommandHandler<TCommand>, TCommandHandler>();
        return this;
    }
}

public class CommandHandlerDsl<TCommand> : Pipeline where TCommand : ICommand
{
    private readonly IServiceCollection _serviceCollection;

    public CommandHandlerDsl(IServiceCollection serviceCollection):base()
    {
        _serviceCollection = serviceCollection;
    }

    public Pipeline HandleWith<TCommandHandler>() where TCommandHandler : class, ICommandHandler<TCommand>
    {
        _serviceCollection.AddTransient<TCommandHandler>();
        _serviceCollection.AddTransient<ICommandHandler<TCommand>, TCommandHandler>();
        return _serviceCollection;
    }
}

public class QueryDsl
{

}