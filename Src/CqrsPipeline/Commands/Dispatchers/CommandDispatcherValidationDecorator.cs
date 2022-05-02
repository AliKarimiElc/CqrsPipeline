using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.Commands.Dispatchers;

/// <summary>
/// Command dispatcher validator decorator that implemented by FluentValidation package
/// </summary>
public class CommandDispatcherValidationDecorator : CommandDispatcherDecorator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="commandDispatcher"></param>
    /// <param name="serviceProvider"></param>
    public CommandDispatcherValidationDecorator(CommandDispatcherExceptionHandlerDecorator commandDispatcher
        , IServiceProvider serviceProvider) : base(commandDispatcher)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public override Task<CommandResult<TData>> SendAsync<TCommand, TData>(TCommand command)
    {
        var validationResult = Validate<TCommand, TData>(command);
        return validationResult == null
            ? CommandDispatcher.SendAsync<TCommand, TData>(command)
            : Task.FromResult(validationResult);
    }

    /// <inheritdoc />
    public override Task<TResult> SendAsync<TCommand, TResult, TData>(TCommand command)
    {
        var validationResult = Validate<TCommand, TResult, TData>(command);
        return validationResult == null
            ? CommandDispatcher.SendAsync<TCommand, TResult, TData>(command)
            : Task.FromResult(validationResult);
    }

    /// <inheritdoc />
    public override Task<CommandResult> SendAsync<TCommand>(TCommand command)
    {
        var validationResult = Validate(command);
        return validationResult != null ? Task.FromResult(validationResult) : CommandDispatcher.SendAsync(command);
    }

    private CommandResult<TData>? Validate<TCommand, TData>(TCommand command) where TCommand : ICommand<TData>
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        if (validator == null) return null;
        var validationResult = validator.Validate(command);
        if (validationResult.IsValid) return null;
        var res = new CommandResult<TData>();
        res.AddError(validationResult.Errors.Select(x => new CommandError
        {
            AttemptedValue = x.AttemptedValue.ToString(),
            Code = x.ErrorCode,
            Message = x.ErrorMessage,
            PropertyName = x.PropertyName
        }));
        return res;
    }

    private TResult? Validate<TCommand, TResult, TData>(TCommand command) where TCommand : ICommand<TResult, TData> where TResult : CommandResult<TData>, new()
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        if (validator == null) return null;
        var validationResult = validator.Validate(command);
        if (validationResult.IsValid) return null;
        var res = new CommandResult<TData>() as TResult;
        res?.AddError(validationResult.Errors.Select(x => new CommandError
        {
            AttemptedValue = x.AttemptedValue.ToString(),
            Code = x.ErrorCode,
            Message = x.ErrorMessage,
            PropertyName = x.PropertyName
        }));
        return res;
    }

    private CommandResult? Validate<TCommand>(TCommand command) where TCommand : class, ICommand
    {
        var validator = _serviceProvider.GetService<IValidator<TCommand>>();
        if (validator == null) return null;
        var validationResult = validator.Validate(command);
        if (validationResult.IsValid) return null;
        var res = new CommandResult();
        res.AddError(validationResult.Errors.Select(x => new CommandError
        {
            AttemptedValue = x.AttemptedValue.ToString(),
            Code = x.ErrorCode,
            Message = x.ErrorMessage,
            PropertyName = x.PropertyName
        }));
        return res;
    }
}