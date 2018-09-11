﻿using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace Ez.Web
{
    /// <summary>
    /// An implementation of the <see cref="IDependencyResolver"/> interface that wraps a Unity container.
    /// Thank you https://damienbod.com/2013/10/01/self-host-webapi-with-owin-and-unity/
    /// 
    /// </summary>
    public sealed class CustomUnityDependencyResolver : IDependencyResolver
    {
        private IUnityContainer container;
        private SharedDependencyScope sharedScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomUnityDependencyResolver"/> class for a container.
        /// </summary>
        /// <param name="container">The <see cref="IUnityContainer"/> to wrap with the <see cref="IDependencyResolver"/>
        /// interface implementation.</param>
        public CustomUnityDependencyResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            this.container = container;
            this.sharedScope = new SharedDependencyScope(container);
        }

        /// <summary>
        /// Reuses the same scope to resolve all the instances.
        /// </summary>
        /// <returns>The shared dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            return this.sharedScope;
        }

        /// <summary>
        /// Disposes the wrapped <see cref="IUnityContainer"/>.
        /// </summary>
        public void Dispose()
        {
            this.container.Dispose();
            this.sharedScope.Dispose();
        }

        /// <summary>
        /// Resolves an instance of the default requested type from the container.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type"/> of the object to get from the container.</param>
        /// <returns>The requested object.</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return this.container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves multiply registered services.
        /// </summary>
        /// <param name="serviceType">The type of the requested services.</param>
        /// <returns>The requested services.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        private sealed class SharedDependencyScope : IDependencyScope
        {
            private IUnityContainer container;

            public SharedDependencyScope(IUnityContainer container)
            {
                this.container = container;
            }

            public object GetService(Type serviceType)
            {
                return this.container.Resolve(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return this.container.ResolveAll(serviceType);
            }

            public void Dispose()
            {
                // NO-OP, as the container is shared.
            }
        }
    }
}