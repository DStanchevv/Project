using Microsoft.AspNetCore.Identity;
using SportWave.Data.Models;
using System.Reflection;
using static SportWave.Common.GeneralAppConstants;

namespace SportWave.Infrastructure.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] serviceTypes = serviceAssembly.GetTypes().Where(t => t.Name.EndsWith("Service") && !t.IsInterface).ToArray();

            foreach (var impType in serviceTypes)
            {
                Type? interfaceType = impType.GetInterface($"I{impType.Name}");
                if (interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for the service with name: {impType.Name}");
                }

                services.AddScoped(interfaceType, impType);
            }
        }

        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            IServiceProvider serviceProvider = scopedServices.ServiceProvider;

            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            RoleManager<IdentityRole<Guid>> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }
                
                IdentityRole<Guid> role = new IdentityRole<Guid>(AdminRoleName);
                await roleManager.CreateAsync(role);
            })
                .GetAwaiter()
                .GetResult();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(EmployeeRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role = new IdentityRole<Guid>(EmployeeRoleName);
                await roleManager.CreateAsync(role);
            })
                .GetAwaiter()
                .GetResult();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(UserRoleName))
                {
                    return;
                }

                IdentityRole<Guid> role = new IdentityRole<Guid>(UserRoleName);
                await roleManager.CreateAsync(role);
            })
               .GetAwaiter()
               .GetResult();

            return app;
        }
    }
}
