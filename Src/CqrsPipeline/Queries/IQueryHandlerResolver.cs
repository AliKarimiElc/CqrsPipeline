using CqrsPipeline.Commands;

namespace CqrsPipeline.Queries;

public interface IQueryHandlerResolver : IDependencyResolver
{
    IQueryHandler<TQuery, TData, TQueryResult> ResolveHandlerFor<TQuery, TData, TQueryResult>()
        where TQuery : class, IQuery<TData>
        where TQueryResult : QueryResult<TData>
        => Resolve<IQueryHandler<TQuery, TData, TQueryResult>>();

    ICommandHandler<TCommand, TData> ResolveHandlerFor<TCommand, TData>()
        where TCommand : ICommand<TData>
        => Resolve<ICommandHandler<TCommand, TData>>();
}