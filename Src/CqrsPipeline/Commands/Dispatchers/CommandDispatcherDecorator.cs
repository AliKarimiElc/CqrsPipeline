namespace CqrsPipeline.Commands.Dispatchers
{
    /// <summary>
    /// Command dispatcher decorator base class for add some decorators to command pipeline
    /// </summary>
    public abstract class CommandDispatcherDecorator : ICommandDispatcher
    {
        /// <summary>
        /// Instance of command dispatcher
        /// </summary>
        protected readonly ICommandDispatcher CommandDispatcher;

        /// <summary>
        /// Default constructor for decorators
        /// </summary>
        /// <param name="commandDispatcher"></param>
        protected CommandDispatcherDecorator(ICommandDispatcher commandDispatcher) => CommandDispatcher = commandDispatcher;

        /// <summary>
        /// Decorated send method of command dispatcher
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public abstract Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>;

        /// <summary>
        /// Decorated send method of command dispatcher
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public abstract Task<TResult> SendAsync<TCommand, TResult, TData>(TCommand command)
            where TCommand : ICommand<TResult, TData>
            where TResult : CommandResult<TData>, new();

        /// <summary>
        /// Decorated send method of command dispatcher
        /// </summary>
        /// <param name="command"></param>
        /// <typeparam name="TCommand"></typeparam>
        /// <returns></returns>
        public abstract Task<CommandResult> SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}