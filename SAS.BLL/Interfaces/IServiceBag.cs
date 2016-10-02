using System;

namespace SAS.BLL.Interfaces
{
    public interface IServiceBag : IDisposable
    {
        ICategoryService CategoryService { get; }
        IRequestService RequestService { get; }
        ISkillService SkillService { get; }
        ISkillSetService SkillSetService { get; }
        IUserService UserService { get; }
        IUserProfileService UserProfileService { get; }
    }
}