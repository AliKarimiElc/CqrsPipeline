using CqrsPipeline.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Dsl;

public class CommandDsl<TCommand> where TCommand : ICommand
{
    private readonly IServiceCollection _serviceCollection;

    internal CommandDsl(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    // public CommandHandlerDsl<TCommand> ValidateWith<TValidator>() where TValidator : class, IValidator<TCommand>
    // {
    //     _serviceCollection.AddTransient<TValidator>();
    //     _serviceCollection.AddTransient<IValidator<TCommand>, TValidator>();
    //     return new CommandHandlerDsl<TCommand>(_serviceCollection);
    // }

    public CommandDsl<TCommand> HandleWith<TCommandHandler>() where TCommandHandler : class, ICommandHandler<TCommand>
    {
        _serviceCollection.AddTransient<TCommandHandler>();
        _serviceCollection.AddTransient<ICommandHandler<TCommand>, TCommandHandler>();
        return this;
    }
}