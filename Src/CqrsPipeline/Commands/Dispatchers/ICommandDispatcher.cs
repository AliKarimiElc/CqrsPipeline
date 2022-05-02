namespace CqrsPipeline.Commands.Dispatchers
{
    /// <summary>
    /// Contract of command dispatcher
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Send command to handler
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;

        /// <summary>
        /// Send command to handler
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>;

        /// <summary>
        /// Send command to handler
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        Task<TResult> SendAsync<TCommand, TResult, TData>(TCommand command) where TCommand : ICommand<TResult, TData> where TResult : CommandResult<TData>, new();
    }
}
