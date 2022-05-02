using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Commands.Dispatchers;

/// <summary>
/// Command dispatcher for send commands to handlers
/// </summary>
public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Create instance of command dispatcher
    /// </summary>
    /// <param name="serviceProvider"></param>
    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Send command to handler
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TCommand"></typeparam>
    /// <returns></returns>
    public async Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
        return await handler.ExecuteAsync(command);
    }

    /// <summary>
    /// Send command to handler
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public async Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TData>>();
        return await handler.ExecuteAsync(command);
    }

    /// <summary>
    /// Send command to handler
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <typeparam name="TData"></typeparam>
    /// <returns></returns>
    public async Task<TResult> SendAsync<TCommand, TResult, TData>(TCommand command) where TCommand : ICommand<TResult, TData> where TResult : CommandResult<TData>, new()
    {
        var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult, TData>>();
        return await handler.ExecuteAsync(command);
    }
}