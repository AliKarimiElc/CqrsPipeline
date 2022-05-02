using CqrsPipeline.Exceptions;
using Microsoft.Extensions.Logging;

namespace CqrsPipeline.Commands.Dispatchers
{
    /// <summary>
    /// Command dispatcher exception handler decorator that handle exceptions
    /// </summary>
    public class CommandDispatcherExceptionHandlerDecorator : CommandDispatcherDecorator
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="logger"></param>
        public CommandDispatcherExceptionHandlerDecorator(
            CommandDispatcher commandDispatcher, ILogger<CommandDispatcherExceptionHandlerDecorator> logger)
            : base(commandDispatcher)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        public override async Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync<TCommand, TData>(command);
            }
            catch (CqrsPipelineException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling<TCommand, TData>(ex);
            }
        }

        /// <inheritdoc />
        public override async Task<TResult> SendAsync<TCommand, TResult, TData>(TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync<TCommand,TResult, TData>(command);
            }
            catch (CqrsPipelineException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling<TCommand,TResult, TData>(ex);
            }
        }

        /// <inheritdoc />
        public override async Task<CommandResult> SendAsync<TCommand>(TCommand command)
        {
            try
            {
                return await CommandDispatcher.SendAsync(command);
            }
            catch (CqrsPipelineException ex)
            {
                _logger.LogError(ex, ex.Message);
                return await DomainExceptionHandling(ex);
            }
        }

        private async Task<CommandResult> DomainExceptionHandling(CqrsPipelineException exception)
        {
            var type = typeof(CommandResult);
            dynamic commandResult = new CommandResult();
            if (type.IsGenericType)
            {
                var d1 = typeof(CommandResult<>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe) ?? throw new Exception();
            }

            if (exception.InputParameters?.Any() ?? false)
                commandResult.AddError(exception.InputParameters.Select(x =>
                    new CommandError(exception.ErrorCode, exception.Message, x.PropertyName)).ToList());
            else
                commandResult.AddError(new CommandError(exception.ErrorCode, exception.Message));

            return await Task.FromResult((commandResult as CommandResult)!);
        }

        private async Task<CommandResult<TData>> DomainExceptionHandling<TCommand, TData>(CqrsPipelineException exception)
        {
            var type = typeof(CommandResult<TData>);
            dynamic commandResult = new CommandResult<TData>();
            if (type.IsGenericType)
            {
                var d1 = typeof(CommandResult<TData>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe) ?? throw new Exception();
            }

            if (exception.InputParameters?.Any() ?? false)
                commandResult.AddError(exception.InputParameters.Select(x =>
                    new CommandError(exception.ErrorCode, exception.Message, x.PropertyName)).ToList());
            else
                commandResult.AddError(new CommandError(exception.ErrorCode, exception.Message));

            return await Task.FromResult((commandResult as CommandResult<TData>)!);
        }

        private async Task<TResult> DomainExceptionHandling<TCommand,TResult, TData>(CqrsPipelineException exception)
        {
            var type = typeof(TResult);
            dynamic commandResult = new CommandResult<TData>();
            if (type.IsGenericType)
            {
                var d1 = typeof(CommandResult<TData>);
                var makeMe = d1.MakeGenericType(type.GetGenericArguments());
                commandResult = Activator.CreateInstance(makeMe) ?? throw new Exception();
            }

            if (exception.InputParameters?.Any() ?? false)
                commandResult.AddError(exception.InputParameters.Select(x =>
                    new CommandError(exception.ErrorCode, exception.Message, x.PropertyName)).ToList());
            else
                commandResult.AddError(new CommandError(exception.ErrorCode, exception.Message));

            return await Task.FromResult((TResult)commandResult);
        }

    }
}