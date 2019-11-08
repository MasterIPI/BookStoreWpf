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

            List<TypeInfo> interfaces = new List<TypeInfo>();
            List<TypeInfo> implementations = new List<TypeInfo>();

            List<TypeInfo> definedTypes = GetAllReferencedTypes(baseInterfaceType);

            if (definedTypes.Count == 0)
            {
                return;
            }

            for (int type = 0; type < definedTypes.Count; type++)
            {
                TypeInfo currClass = definedTypes[type];
                if (currClass.IsClass)
                {
                    implementations.Add(currClass);
                    continue;
                }

                if (currClass.IsInterface)
                {
                    interfaces.Add(currClass);
                }
            }

            if (interfaces.Count != implementations.Count)
            {
                return;
            }

            for (int interfaceTypeIndex = 0; interfaceTypeIndex < interfaces.Count; interfaceTypeIndex++)
            {
                TypeInfo currInterface = interfaces.ElementAt(interfaceTypeIndex);
                TypeInfo implementation = implementations.FirstOrDefault(type => type.ImplementedInterfaces.Any(inter => inter == currInterface));

                if (!(implementation is null) && !(currInterface is null))
                {
                    serviceCollection.AddTransient(currInterface, implementation);
                    implementations.Remove(implementation);
                }
            }
        }

        private static List<TypeInfo> GetAllReferencedTypes(Type typeToSearch)
        {
            HashSet<TypeInfo> loadedTypes = new HashSet<TypeInfo>();

            foreach (TypeInfo type in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "*.dll", SearchOption.AllDirectories)
                                                      .Select(name => Assembly.LoadFile(name))
                                                      .SelectMany(assembly => assembly.DefinedTypes)
                                                      .Where(type => type.IsPublic
                                                                     && type.ImplementedInterfaces.Any(inter => (
                                                                                                                  inter.Name.Equals(typeToSearch.Name)
                                                                                                                  && (
                                                                                                                      inter.Assembly.CodeBase.Equals(typeToSearch.Assembly.CodeBase)
                                                                                                                      && inter.Assembly.Location.Equals(typeToSearch.Assembly.Location)
                                                                                                                      && inter.Assembly.FullName.Equals(typeToSearch.Assembly.FullName)
                                                                                                                      )
                                                                                                                ) || typeToSearch.IsAssignableFrom(inter)
                                                                                                      )
                                                                     && ((!type.IsAbstract && type.IsClass) || (type.IsInterface && type.IsAbstract))
                                                            )
                                                      .ToList())
            {

                loadedTypes.Add(type);
            }

            return loadedTypes.ToList();
        }
    }
}
