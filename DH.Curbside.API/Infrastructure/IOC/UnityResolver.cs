using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace DH.Curbside.API.Infrastructure.IOC
{
    /// <summary>
    /// Handles unity configuration
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        /// <summary>
        /// Unity Container
        /// </summary>
        protected IUnityContainer Container;

        /// <summary>
        /// Unity resolver constructor
        /// </summary>
        /// <param name="container">Unity Container</param>
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        /// <summary>
        /// Returns the  resolver type
        /// </summary>
        /// <param name="serviceType">Service Type object
        /// </param>
        /// <returns>object</returns>
        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
        /// <summary>
        /// Returns the  list of resolver types
        /// </summary>
        /// <param name="serviceType">Service Type Object</param>
        /// <returns>IEnumerable object</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
        
        /// <summary>
        /// Creates dependency scope
        /// </summary>
        /// <returns>Dependency cope object</returns>
        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        /// <summary>
        /// Disposes container
        /// </summary>
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}