using System.Reflection;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesAndRepositories(this IServiceCollection services, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    foreach (var @interface in interfaces)
                    {
                        if (@interface.Name == $"I{type.Name}" && @interface.Name != "IDirectDbRepository`1")
                        {
                            services.AddScoped(@interface, type);
                        }
                    }
                }
            }
        }
    }
}