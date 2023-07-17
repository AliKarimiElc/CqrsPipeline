using CqrsPipeline.Commands;

namespace CqrsPipeline.Pipeline.Command;

public interface ICommandPipeline
{
    Task<CommandResult> DispatchCommandAsync<TCommand>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand;

    Task<CommandResult<TData>> DispatchCommandAsync<TCommand, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand<TData>;

    Task<TResult> DispatchCommandAsync<TCommand, TResult, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TResult : CommandResult<TData>
        where TCommand : ICommand<TResult, TData>;

}