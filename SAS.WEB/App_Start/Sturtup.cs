using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Microsoft.AspNet.Identity;
using SAS.BLL.Interfaces;
using SAS.WEB.Util;
using Ninject;
using SAS.BLL.Infrastructure;

[assembly: OwinStartup(typeof(UserStore.App_Start.Startup))]

namespace UserStore.App_Start
{
    public class Startup
    {      
        NinjectDependencyResolver resolver = new NinjectDependencyResolver(new StandardKernel(new ServiseModule()));
              
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            var serviceBag = (IServiceBag)resolver.GetService(typeof(IServiceBag));
            return serviceBag.UserService;
        }
    }
}

