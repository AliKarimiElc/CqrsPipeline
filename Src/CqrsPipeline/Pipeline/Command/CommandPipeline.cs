using CqrsPipeline.Commands;
using System.Collections.Concurrent;

namespace CqrsPipeline.Pipeline.Command;

public class CommandPipeline : ICommandPipeline
{
    private readonly ConcurrentDictionary<Type,object> _commandHandlers = new();

    private readonly IDependencyResolver _dependencyResolver;

    public void AddCommand<TCommand>()
    {
        _commandHandlers.TryAdd(typeof(TCommand), null);
    }


    public CommandPipeline(IDependencyResolver dependencyResolver)
    {
        _dependencyResolver = dependencyResolver;
    }

    public async Task<CommandResult> DispatchCommandAsync<TCommand>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand
    {
        var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand>>();
        return await handler.ExecuteAsync(command, cancellationToken);
    }

    public async Task<CommandResult<TData>> DispatchCommandAsync<TCommand, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand<TData>
    {
        var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand, TData>>();
        return await handler.ExecuteAsync(command, cancellationToken);
    }

    public async Task<TResult> DispatchCommandAsync<TCommand, TResult, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TResult : CommandResult<TData> where TCommand : ICommand<TResult, TData>
    {
        var handler = _dependencyResolver.Resolve<ICommandHandler<TCommand, TResult, TData>>();
        return await handler.ExecuteAsync(command, cancellationToken);
    }
}