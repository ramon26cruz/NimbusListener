using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Nimbus;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using Nimbus.Logger.Serilog;
using TopShelf.ConfigurationSettings;
using Module = Autofac.Module;

namespace TopShelf.AutofacModules
{
    public class BusModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var workerAssembly = Assembly.GetExecutingAssembly();
            var messageContractsAssembly = typeof(Program).Assembly;

            builder.Register(c => new SerilogLogger(c.Resolve<Serilog.ILogger>()))
                .As<ILogger>()
                .SingleInstance();

            var handlerTypesProvider = new AssemblyScanningTypeProvider(workerAssembly, messageContractsAssembly);

            builder.RegisterNimbus(handlerTypesProvider);
            builder.Register(componentContext => new BusBuilder()
                    .Configure()
                    .WithConnectionString("Endpoint=sb://opportunity-ram.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=bDzLHz6aN29LfuppmlZQfvxyE0xKRXEb0lJi4wk/0Do=")
                    .WithNames("Opportunity", "opportunitybus")
                    .WithTypesFrom(handlerTypesProvider)
                    .WithAutofacDefaults(componentContext)
                    .WithDefaultTimeout(TimeSpan.FromMinutes(3))
                    .Build())
                .As<IBus>()
                .AutoActivate()
                .OnActivated(c => c.Instance.Start().Wait())
                .SingleInstance();
        }
    }
}
