namespace CqrsPipeline.Commands;

/// <summary>
/// Contract for command handlers
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<CommandResult> ExecuteAsync(TCommand command);

    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CommandResult> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Contract for command handlers with expected data
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TData"></typeparam>
public interface ICommandHandler<in TCommand, TData> where TCommand : ICommand<TData>
{
    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<CommandResult<TData>> ExecuteAsync(TCommand command);

    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<CommandResult<TData>> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}

/// <summary>
/// Contract for command handlers with expected data in expected envelope
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResult"></typeparam>
/// <typeparam name="TData"></typeparam>
public interface ICommandHandler<in TCommand, TResult, TData> where TCommand : ICommand<TResult, TData>
    where TResult : CommandResult<TData>
{
    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<TResult> ExecuteAsync(TCommand command);

    /// <summary>
    /// Execute command
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResult> ExecuteAsync(TCommand command, CancellationToken cancellationToken);
}