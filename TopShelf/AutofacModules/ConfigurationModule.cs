using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ConfigInjector.Configuration;

namespace TopShelf.AutofacModules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var workerAssembly = typeof(ConfigurationModule).Assembly;

            //ConfigurationConfigurator.RegisterConfigurationSettings()
            //    .FromAssemblies(workerAssembly)
            //    .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
            //        .AsSelf()
            //        .SingleInstance())
            //    .DoYourThing();
        }
    }
}
