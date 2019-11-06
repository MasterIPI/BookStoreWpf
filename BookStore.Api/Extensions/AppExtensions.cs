using BookStore.Api.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookStore.Api.Extensions
{
    public static class AppExtensions
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                using (ApplicationContext context = scope.ServiceProvider.GetService<ApplicationContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            return webHost;
        }

        public static void RegisterClassesFromBaseInterfaceTransient<TBaseInterface>(this IServiceCollection serviceCollection, Assembly baseInterfaceAssembly) where TBaseInterface : class
        {
            Type baseInterfaceType = typeof(TBaseInterface);

            if (!baseInterfaceType.IsInterface)
            {
                throw new TypeLoadException("Only interfaces (base) are allowed.");
            }

            if (baseInterfaceAssembly != baseInterfaceType.Assembly)
            {
                throw new ArgumentException("Provided assembly is not matching assembly, containing TBaseInterface");
            }

            if (((TypeInfo)baseInterfaceType).ImplementedInterfaces.Any(interfaceType => interfaceType.Assembly == baseInterfaceType.Assembly
                                                                                         || interfaceType.Assembly == baseInterfaceAssembly))
            {
                throw new TypeLoadException("Only base interfaces are allowed.");
            }

            var referencedAssemblies = Assembly.GetExecutingAssembly()
                                                   .GetReferencedAssemblies();
            var definedTypes = referencedAssemblies
                                                   .Select(name => Assembly.Load(name))
                                                   .SelectMany(assembly => assembly.DefinedTypes)
                                                   .Where(type => type.ImplementedInterfaces.Count(
                                                                                                   inter => (inter.Name == baseInterfaceType.Name 
                                                                                                                && inter.Assembly == baseInterfaceType.Assembly)
                                                                                                                || baseInterfaceType.IsAssignableFrom(inter)
                                                                                                  ) > 0
                                                                  && (!type.IsAbstract || (type.IsInterface && type.IsAbstract)))
                                                   .GroupBy(type => type).ToDictionary();

            
        }
    }
}
