using BookStore.Api.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static void RegisterClassesFromBaseInterfaceTransient<TBaseInterface>(this IServiceCollection serviceCollection) where TBaseInterface : class
        {
            TypeInfo baseInterfaceType = typeof(TBaseInterface) as TypeInfo;
            Assembly baseInterfaceAssembly = baseInterfaceType.Assembly;

            if (!baseInterfaceType.IsInterface)
            {
                throw new TypeLoadException("Only interfaces (base) are allowed.");
            }

            if (baseInterfaceType.ImplementedInterfaces.Any(implementedInterface => implementedInterface.IsAssignableFrom(baseInterfaceType)
                                                                                    || (
                                                                                         implementedInterface.Assembly.CodeBase.Equals(baseInterfaceType.Assembly.CodeBase)
                                                                                         && implementedInterface.Assembly.Location.Equals(baseInterfaceType.Assembly.Location)
                                                                                         && implementedInterface.Assembly.FullName.Equals(baseInterfaceType.Assembly.FullName)
                                                                                       )
                                                           ))
            {
                throw new TypeLoadException("Only base interfaces are allowed.");
            }

            Dictionary<Type,TypeInfo> definedTypes = GetAllImplementedTypes(baseInterfaceType);

            if (definedTypes.Count == 0)
            {
                return;
            }

            foreach(var group in definedTypes)
            {
                serviceCollection.AddTransient(group.Key, group.Value);
            }
        }

        private static Dictionary<Type, TypeInfo> GetAllImplementedTypes(TypeInfo interfaceToSearch)
        {
            Func<Type, bool> intefaceClassSelector = (implementedInterface) =>
            interfaceToSearch.IsAssignableFrom(implementedInterface)
            || (
                 implementedInterface.Name.Equals(interfaceToSearch.Name)
                 && implementedInterface.Assembly.CodeBase.Equals(interfaceToSearch.Assembly.CodeBase)
                 && implementedInterface.Assembly.Location.Equals(interfaceToSearch.Assembly.Location)
                 && implementedInterface.Assembly.FullName.Equals(interfaceToSearch.Assembly.FullName)
               );

            Dictionary<Type, TypeInfo> result = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll", SearchOption.AllDirectories)
                                                         .Select(name => Assembly.LoadFile(name))
                                                         .SelectMany(assembly => assembly.DefinedTypes)
                                                         .Where(type => type.IsPublic
                                                                        && !type.IsAbstract
                                                                        && type.IsClass
                                                                        && type.ImplementedInterfaces.Any(intefaceClassSelector))
                                                         .GroupBy(type => type.ImplementedInterfaces.First(inter => !((inter as TypeInfo).ImplementedInterfaces.FirstOrDefault(intefaceClassSelector) is null)))
                                                         .ToDictionary(group => group.Key, group => group.FirstOrDefault());

            return result;
        }
    }
}
