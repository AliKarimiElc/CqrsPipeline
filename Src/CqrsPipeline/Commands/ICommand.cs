namespace CqrsPipeline.Commands;

/// <summary>
/// Contract of commands without data
/// </summary>
public interface ICommand
{
}

/// <summary>
/// Contract of commands with data
/// </summary>
public interface ICommand<TData> : ICommand
{
}

/// <summary>
/// Contract of commands with data and you can choose different types of envelope for them
/// </summary>
public interface ICommand<TResult,TData> : ICommand<TData> where TResult : CommandResult<TData>
{
}