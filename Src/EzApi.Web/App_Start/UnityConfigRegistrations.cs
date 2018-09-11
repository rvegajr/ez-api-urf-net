
using System.Data.Entity;
using System.Security.Claims;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Lifetime;
using System.Web.Http.ExceptionHandling;
using NLog;
using System.Web.Http.Tracing;
using Ez.Common;
using Ez.Web.Infrastructure.NLogTrace;
using Ez.Web.Infrastructure;
using Ez.Common.Logging;
using Ez.Entities.Models;
using Ez.Repository;

namespace Ez.Web
{
    /// <summary></summary>
    public enum UnityRegistrationLifetimeManagerType
    {
        /// <summary></summary>
        Singleton,
        /// <summary></summary>
        PerRequest,
        /// <summary></summary>
        Transient,
        /// <summary></summary>
        NotControlled
    }

    /// <summary></summary>
    public static class UnityConfigRegistrations
    {
        /// <summary></summary>
        public static LifetimeManager ToUnityLifeTimeManager(this UnityRegistrationLifetimeManagerType lifetimeManagerType)
        {
            switch (lifetimeManagerType)
            {
                case UnityRegistrationLifetimeManagerType.Singleton:
                    return new ContainerControlledLifetimeManager();
                case UnityRegistrationLifetimeManagerType.PerRequest:
                    return new PerRequestLifetimeManager();
                case UnityRegistrationLifetimeManagerType.Transient:
                    return new TransientLifetimeManager();
                case UnityRegistrationLifetimeManagerType.NotControlled:
                    return new ExternallyControlledLifetimeManager();
                default:
                    return new TransientLifetimeManager();
            }
        }

        /// <summary></summary>
        public static void RegisterTypes(IUnityContainer container)
        {
            RegisterTypes(container, UnityRegistrationLifetimeManagerType.Transient);
            RegisterPerRequest(container);
        }
        /// <summary></summary>
        public static void RegisterTypesOwin(IUnityContainer container)
        {
            RegisterTypes(container, UnityRegistrationLifetimeManagerType.Transient);
            RegisterPerRequest(container);
        }
        /// <summary></summary>
        public static void RegisterTransient(IUnityContainer container)
        {
        }
        /// <summary></summary>
        public static void RegisterSingleton(IUnityContainer container)
        {
            if (AppSettings.Instance.TraceType == TraceType.TraceWriter)
            {
                container.RegisterType<ITraceWriter, NLogTraceWriter>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            }
            container.RegisterType<IExceptionLogger, NLogExceptionLogger>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType<IExceptionHandler, GlobalExceptionHandler>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType<ICustomLogHander, CustomLogHandler>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType(typeof(IEzLogger<>), typeof(EzLogger<>),
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType<IEzLogger, EzLogger<WebApiApplication>>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType(typeof(ILogger<>), typeof(EzLogger<>),
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
            container.RegisterType<ILogger, EzLogger<WebApiApplication>>(
                UnityRegistrationLifetimeManagerType.Singleton.ToUnityLifeTimeManager());
        }
        /// <summary> </summary>
        public static void RegisterPerRequest(IUnityContainer container)
        {
            //container.RegisterType<DbContext, EzEntities>(UnityRegistrationLifetimeManagerType.PerRequest.ToUnityLifeTimeManager());
            //container.RegisterType<IEzStoredProcedures, EzEntities>(UnityRegistrationLifetimeManagerType.PerRequest.ToUnityLifeTimeManager());
        }

        /// <summary></summary>
        public static void RegisterTypes(IUnityContainer container, UnityRegistrationLifetimeManagerType lifetimeManagerType)
        {
            container
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(lifetimeManagerType.ToUnityLifeTimeManager())
                .RegisterType<IUnitOfWorkAsyncEx, UnitOfWorkEx>(lifetimeManagerType.ToUnityLifeTimeManager())
            ;
        }
    }
}