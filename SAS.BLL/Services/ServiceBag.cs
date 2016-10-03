using System;
using SAS.BLL.Interfaces;
using SAS.DAL.Interfaces;

namespace SAS.BLL.Services
{
    /// <summary>
    /// Contains properties for getting instances of services. 
    /// </summary>
    public class ServiceBag : IServiceBag
    {
        IUnitOfWork unitOfWork;

        public ServiceBag(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
        }

        #region Properties
        public ICategoryService CategoryService
        {
            get { return new CategoryService(unitOfWork); }
        }

        public IRequestService RequestService
        {
            get { return new RequestService(unitOfWork); }
        }

        public IUserService UserService
        {
            get { return new UserService(unitOfWork); }
        }

        public IUserProfileService UserProfileService
        {
            get { return new UserProfileService(unitOfWork); }
        }

        public ISkillSetService SkillSetService
        {
            get { return new SkillSetService(unitOfWork); }
        }

        public ISkillService SkillService
        {
            get { return new SkillService(unitOfWork); }
        }
        #endregion

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
