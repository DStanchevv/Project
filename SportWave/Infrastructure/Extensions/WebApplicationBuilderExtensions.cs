using System.Reflection;

namespace SportWave.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if(serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly.GetTypes().Where(t => t.Name.EndsWith("Service") && !t.IsInterface).ToArray();

            foreach (var impType in serviceTypes)
            {
                Type? interfaceType = impType.GetInterface($"I{impType.Name}");
                if(interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for the service with name: {impType.Name}");
                }

                services.AddScoped(interfaceType, impType);
            }
        }
    }
}
