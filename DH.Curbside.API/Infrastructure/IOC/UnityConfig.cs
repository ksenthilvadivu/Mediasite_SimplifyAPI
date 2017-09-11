using AutoMapper;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Configuration;
using System.Web.Http;
using DH.Curbside.Core.Application.AutoMapper;

namespace DH.Curbside.API.Infrastructure.IOC
{
    /// <summary>
    /// Handles unity configuration
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// Registers unity configurations
        /// </summary>
        public static void RegisterComponents()
        {
            IUnityContainer container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unityCurbside");
            section.Configure(container);            

            container.AddNewExtension<Interception>();
            var config = new AutoMapperConfiguration().Configure();
            container.RegisterInstance<IMapper>(config.CreateMapper());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}