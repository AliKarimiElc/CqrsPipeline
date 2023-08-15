namespace CqrsPipeline.Commands;

public interface ICommandHandlerResolver : IDependencyResolver
{
    ICommandHandler<TCommand> ResolveHandlerFor<TCommand>()
        where TCommand : ICommand
        => Resolve<ICommandHandler<TCommand>>();

    ICommandHandler<TCommand, TData> ResolveHandlerFor<TCommand, TData>()
        where TCommand : ICommand<TData>
        => Resolve<ICommandHandler<TCommand, TData>>();

    ICommandHandler<TCommand, TResult, TData> ResolveHandlerFor<TCommand, TResult, TData>()
        where TCommand : ICommand<TResult, TData>
        where TResult : CommandResult<TData>
        => Resolve<ICommandHandler<TCommand, TResult, TData>>();
}