using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace trade.application.injector
{
    public class DependencyInjector : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public DependencyInjector(IKernel kernel) { _kernel = kernel; }
        public IDependencyScope BeginScope() { return this; }
        public object GetService(Type serviceType) { return _kernel.TryGet(serviceType); }
        public IEnumerable<object> GetServices(Type serviceType) { return _kernel.GetAll(serviceType); }
        public void Dispose() { }
    }
}