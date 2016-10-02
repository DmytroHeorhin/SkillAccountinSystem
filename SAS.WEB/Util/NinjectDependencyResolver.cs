using System.Web.Mvc;
using Ninject;
using System.Collections.Generic;
using System;
using SAS.BLL.Interfaces;
using SAS.BLL.Services;

namespace SAS.WEB.Util
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IServiceBag>().To<ServiceBag>();
        }
    }
}

