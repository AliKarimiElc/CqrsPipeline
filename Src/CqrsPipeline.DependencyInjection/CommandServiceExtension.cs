using System.Reflection;
using CqrsPipeline.Commands;
using CqrsPipeline.Commands.Dispatchers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.DependencyInjection;

/// <summary>
/// Register command handlers and dispatchers
/// </summary>
public static class CommandServiceExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddFluentValidators(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddValidatorsFromAssemblies(assembliesForSearch);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembliesForSearch"></param>
    /// <returns></returns>
    public static IServiceCollection AddCommandHandlers(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>), typeof(ICommandHandler<,,>));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCommandDispatcher(this IServiceCollection services)
    {
        services.AddTransient<CommandDispatcher, CommandDispatcher>();
        services.AddTransient<CommandDispatcherExceptionHandlerDecorator, CommandDispatcherExceptionHandlerDecorator>();
        services.AddTransient<CommandDispatcherValidationDecorator, CommandDispatcherValidationDecorator>();
        services.AddTransient<ICommandDispatcher, CommandDispatcherValidationDecorator>();
        return services;
    }
}