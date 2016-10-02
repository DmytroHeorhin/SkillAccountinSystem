using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        IEnumerable<UserDTO> GetAll();
        IEnumerable<UserDTO> GetByRequest(IEnumerable<SkillRequirementDTO> request);
    }
}

