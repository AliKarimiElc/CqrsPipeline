using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace CqrsPipeline.DependencyInjection
{
    /// <summary>
    /// Extensions for resolve dependencies and service registration
    /// </summary>
    public static class DependencyResolver
    {
        /// <summary>
        /// Register assignable types in services with Transient lifetime
        /// </summary>
        /// <param name="services">Instance of IServiceCollection that services register in it</param>
        /// <param name="assembliesForSearch">Assemblies for search assignable type implementation in them</param>
        /// <param name="assignableTo">Types that we want register implementations for them</param>
        /// <returns></returns>
        public static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
            return services;
        }

        /// <summary>
        /// Register assignable types in services with Scope lifetime
        /// </summary>
        /// <param name="services">Instance of IServiceCollection that services register in it</param>
        /// <param name="assembliesForSearch">Assemblies for search assignable type implementation in them</param>
        /// <param name="assignableTo">Types that we want register implementations for them</param>
        /// <returns></returns>
        public static IServiceCollection AddWithScopedLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
            return services;
        }

        /// <summary>
        /// Register assignable types in services with Singleton lifetime
        /// </summary>
        /// <param name="services">Instance of IServiceCollection that services register in it</param>
        /// <param name="assembliesForSearch">Assemblies for search assignable type implementation in them</param>
        /// <param name="assignableTo">Types that we want register implementations for them</param>
        /// <returns></returns>
        public static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            return services;
        }

        /// <summary>
        /// Load and get assemblies by that`s name
        /// </summary>
        /// <param name="assemblyNames">name of assemblies for load</param>
        /// <returns></returns>
        public static Assembly[] GetAssemblies(string[] assemblyNames)
        {
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            var loadMeLibraries = dependencies.Where(x => IsCandidateCompilationLibrary(x, assemblyNames));

            return loadMeLibraries.Select(loadMeLibrary => Assembly.Load(new AssemblyName(loadMeLibrary.Name)))
                .ToArray();
        }

        private static bool IsCandidateCompilationLibrary(Library compilationLibrary, string[] assemblyName)
        {
            return assemblyName.Any(d => compilationLibrary.Name.Contains(d))
                   || compilationLibrary.Dependencies.Any(d => assemblyName.Any(c => d.Name.Contains(c)));
        }
    }
}