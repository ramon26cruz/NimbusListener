using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Serilog.Enrichers;
using SerilogWeb.Classic.Enrichers;
using TopShelf.ConfigurationSettings;

namespace TopShelf.AutofacModules
{
    public class LoggerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.Register(c => new LoggerConfiguration()
                    .Enrich.WithProperty("ApplicationName", "OpportunityPipelineBSTIntegration")
                    .Enrich.WithProperty("ApplicationVersion", ThisAssembly.GetName().Version)
                    .Enrich.WithMachineName()
                    .Enrich.WithEnvironmentUserName()
                    .Enrich.With<MachineNameEnricher>()
                    .Enrich.With<HttpRequestClientHostIPEnricher>()
                    .Enrich.With<HttpRequestIdEnricher>()
                    .Enrich.With<HttpRequestNumberEnricher>()
                    .Enrich.With<HttpRequestRawUrlEnricher>()
                    .Enrich.With<HttpRequestTraceIdEnricher>()
                    .Enrich.With<HttpRequestTypeEnricher>()
                    .Enrich.With<HttpRequestUrlReferrerEnricher>()
                    .Enrich.With<HttpRequestUserAgentEnricher>()
                    .Enrich.With<UserNameEnricher>()
                    .MinimumLevel.Debug()
                    // .WriteTo.ColoredConsole()
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger())
                .As<ILogger>()
                .AutoActivate()
                .OnActivated(args => Log.Logger = args.Instance)
                .SingleInstance();
        }
    }
}
