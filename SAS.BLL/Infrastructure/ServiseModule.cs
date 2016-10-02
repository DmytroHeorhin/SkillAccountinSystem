using Ninject.Modules;
using SAS.DAL.Repositories;
using SAS.DAL.Interfaces;

namespace SAS.BLL.Infrastructure
{
    public class ServiseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
