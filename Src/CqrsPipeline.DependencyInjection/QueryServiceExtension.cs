using System.Reflection;
using CqrsPipeline.Queries;
using CqrsPipeline.Queries.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsPipeline.DependencyInjection
{
    /// <summary>
    /// An extension for add register query service and handlers
    /// </summary>
    public static class QueryServiceExtension
    {
        /// <summary>
        /// Register query handlers and query dispatcher
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembliesForSearch"></param>
        /// <returns></returns>
        public static IServiceCollection AddQueryHandlers(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch) =>
            services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,,>),
                typeof(IQueryDispatcher));
    }
}