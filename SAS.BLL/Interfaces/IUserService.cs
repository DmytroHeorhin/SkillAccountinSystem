using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    /// <summary>
    /// Contains methods for managing users. 
    /// </summary>
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        IEnumerable<UserDTO> GetAll();
        IEnumerable<UserDTO> GetByRequest(IEnumerable<SkillRequirementDTO> request);
    }
}

