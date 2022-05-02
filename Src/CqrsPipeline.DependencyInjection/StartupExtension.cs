using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.DependencyInjection
{
    /// <summary>
    /// Register Cqrs pipeline services
    /// </summary>
    public static class StartupExtension
    {
        #region Command

        /// <summary>
        /// Register command pipeline
        /// </summary>
        /// <param name="services"></param>
        /// <param name="commandHandlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommandPipeline(this IServiceCollection services, IEnumerable<Assembly> commandHandlerAssemblies)
        {
            services.AddFluentValidators(commandHandlerAssemblies);
            services.AddCommandHandlers(commandHandlerAssemblies);
            services.AddCommandDispatcher();
            return services;
        }

        #endregion

        #region Query

        /// <summary>
        /// Register query pipeline
        /// </summary>
        /// <param name="services"></param>
        /// <param name="queryHandlerAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueryPipeline(this IServiceCollection services,
            IEnumerable<Assembly> queryHandlerAssemblies)
        {
            services.AddQueryHandlers(queryHandlerAssemblies);
            return services;
        }

        #endregion
    }
}