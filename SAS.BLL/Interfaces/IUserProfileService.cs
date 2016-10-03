using System.Threading.Tasks;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    /// <summary>
    /// Contains methods for getting and adding/updating user profiles. 
    /// </summary>
    public interface IUserProfileService
    {      
        Task<OperationDetails> UpdateUserProfileAsync(UserProfileDTO userProfileDTO);
        Task<UserProfileDTO> FindByNameAsync(string userName);
    }
}