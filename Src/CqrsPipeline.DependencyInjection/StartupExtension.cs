using System.Reflection;
using CqrsPipeline.DataAccess;
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

        
        
        #region DataAccess

        /// <summary>
        /// Register all command repositories that inherit from ICommandRepository
        /// </summary>
        /// <param name="services"></param>
        /// <param name="commandRepositoryAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommandRepositories(this IServiceCollection services,
            IEnumerable<Assembly> commandRepositoryAssembly)
        {
            return services.AddWithTransientLifetime(commandRepositoryAssembly, typeof(ICommandRepository<,>));
        }

        /// <summary>
        /// Register all query repositories that inherit from IQueryRepository
        /// </summary>
        /// <param name="services"></param>
        /// <param name="queryRepositoryAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueryRepositories(this IServiceCollection services,
            IEnumerable<Assembly> queryRepositoryAssembly)
        {
            return services.AddWithTransientLifetime(queryRepositoryAssembly, typeof(IQueryRepository<,>));
        }

        /// <summary>
        /// Register UnitOfWork repositories that inherit from IUnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <param name="unitOfWorkAssembly"></param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services,
            IEnumerable<Assembly> unitOfWorkAssembly)
        {
            return services.AddWithTransientLifetime(unitOfWorkAssembly, typeof(IUnitOfWork));
        }

        #endregion

    }
}