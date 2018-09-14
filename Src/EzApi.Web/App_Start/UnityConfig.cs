using System;
using System.Data.Entity;
using CommonServiceLocator;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;
using Unity;
using System.Text;
using Unity.AspNet.Mvc;
using Unity.ServiceLocation;
using System.Linq;

namespace Ez.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value; 
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            UnityConfigRegistrationsGenerated.RegisterTypes(container);
            UnityConfigServiceRegistrationsGenerated.RegisterTypes(container);
            UnityConfigRegistrations.RegisterSingleton(container);
            UnityConfigRegistrations.RegisterTypes(container);
            UnityConfigRegistrations.RegisterTransient(container);

            var serviceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() =>
            {
                return serviceLocator;
            });
        }

        
        /// <summary>
        /// Dumps the unity container.
        /// </summary>
        /// <param name="theContainer">The container.</param>
        /// <returns></returns>
        public static string DumpUnityContainer(this IUnityContainer theContainer)
        {
            string regName, regType, mapTo, lifetime, rec;
            var sb = new StringBuilder();
            rec = string.Format("Container has {0} Registrations:", theContainer.Registrations.ToList().Count());
            sb.AppendLine(rec);
            System.Diagnostics.Debug.WriteLine(rec);
            foreach (var item in theContainer.Registrations)
            {
                regType = item.RegisteredType.Name;
                mapTo = item.MappedToType.Name;
                regName = item.Name ?? "[default]";
                lifetime = item.LifetimeManager.LifetimeType.Name;
                if (mapTo != regType)
                {
                    mapTo = " -> " + mapTo;
                }
                else
                {
                    mapTo = string.Empty;
                }
                lifetime = lifetime.Substring(0, lifetime.Length - "LifetimeManager".Length);
                rec = string.Format("+ {0}{1}  '{2}'  {3}", regType, mapTo, regName, lifetime);
                sb.AppendLine(rec);
                System.Diagnostics.Debug.WriteLine(rec);
            }
            return sb.ToString(); ;
        }
    }
}
