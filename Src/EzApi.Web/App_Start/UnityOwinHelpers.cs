using Ez.Web.Owin;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity;
using Unity.NLog;

namespace Ez.Web
{
    /// <summary></summary>
    public static class UnityOwinHelpers
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<NLogExtension>(); // add logger extension
            RegisterTypes(container);
            return container;
        });

        /// <summary></summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        //private static readonly Type[] EmptyTypes = new Type[0];

        /// <summary></summary>
        /// <summary></summary>
        public static IEnumerable<Type> GetTypesWithCustomAttribute<T>(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }

        /// <summary></summary>
        public static void RegisterTypes(IUnityContainer container)
        {
            //var myAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("SelfHostWebApiOwin")).ToArray();

            container.RegisterType(typeof(Startup));
            UnityConfigRegistrationsGenerated.RegisterTypesOwin(container);
            UnityConfigServiceRegistrationsGenerated.RegisterTypesOwin(container);
            UnityConfigRegistrations.RegisterTypesOwin(container);
            UnityConfigRegistrations.RegisterSingleton(container);
            /*
            container.RegisterTypes(
                UnityHelpers.GetTypesWithCustomAttribute<UnityIoCContainerControlledAttribute>(myAssemblies),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled,
                null
                ).RegisterTypes(
                         UnityHelpers.GetTypesWithCustomAttribute<UnityIoCTransientLifetimeAttribute>(myAssemblies),
                         WithMappings.FromMatchingInterface,
                         WithName.Default,
                         WithLifetime.Transient);
            */
        }

    }
}