using System.Collections.Concurrent;
using CqrsPipeline.Commands;
using CqrsPipeline.Pipeline.Command;
using CqrsPipeline.Pipeline.Query;
using CqrsPipeline.Queries;

namespace CqrsPipeline.Pipeline;

public class CqrsPipeline : ICqrsPipeline
{
    private readonly ICommandPipeline _commandPipeline;
    private readonly IQueryPipeline _queryPipeline;

    public CqrsPipeline(ICommandPipeline commandPipeline, IQueryPipeline queryPipeline)
    {
        _commandPipeline = commandPipeline;
        _queryPipeline = queryPipeline;
    }

    public async Task<CommandResult> DispatchCommandAsync<TCommand>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand
    {
        return await _commandPipeline.DispatchCommandAsync(command, cancellationToken);
    }

    public async Task<CommandResult<TData>> DispatchCommandAsync<TCommand, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand<TData>
    {
        return await _commandPipeline.DispatchCommandAsync<TCommand, TData>(command, cancellationToken);
    }

    public async Task<TCommandResult> DispatchCommandAsync<TCommand, TCommandResult, TData>(TCommand command,
        CancellationToken cancellationToken = new())
        where TCommand : ICommand<TCommandResult, TData>
        where TCommandResult : CommandResult<TData>
    {
        return await _commandPipeline.DispatchCommandAsync<TCommand, TCommandResult, TData>(command, cancellationToken);
    }

    public async Task<TQueryResult> DispatchQueryAsync<TQuery, TData, TQueryResult>(TQuery query
        , CancellationToken cancellationToken = new())
        where TQueryResult : QueryResult<TData>
        where TQuery : class, IQuery<TData>
    {
        return await _queryPipeline.DispatchQueryAsync<TQuery, TData, TQueryResult>(query, cancellationToken);
    }
}