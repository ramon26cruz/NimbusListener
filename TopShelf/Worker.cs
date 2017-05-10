using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace TopShelf
{
    public class Worker
    {
        private IContainer _container;

        public void Start()
        {
            if (_container != null) return;
            _container = IoC.Configure();

        }
        public void Stop()
        {
            var container = _container;
            if (container == null) return;
            _container = null;
            container.Dispose();
        }
    }

    public static class IoC
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(IoC).Assembly);
            return builder.Build();
        }
        
    }
}
