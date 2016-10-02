using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;
using SAS.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using SAS.BLL.Interfaces;
using SAS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNet.Identity.EntityFramework;
using SAS.BLL.Mapping;

namespace SAS.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            if(userDto.Email == null || userDto.Email == string.Empty)
            {
                return new OperationDetails(false, "User with no email has been provided", "Email");
            }
            if (userDto.Password == null || userDto.Password == string.Empty)
            {
                return new OperationDetails(false, "User with no password has been provided", "Password");
            }
            if (userDto.Role == null || userDto.Role == string.Empty)
            {
                return new OperationDetails(false, "User with no role has been provided", "Role");
            }
            try
            {
                IdentityRole role = unitOfWork.RoleManager.FindByName(userDto.Role);
                if (role == null)
                {
                    return new OperationDetails(false, "User with invalid role has been provided", "Role");
                }
                if (await unitOfWork.UserManager.FindByNameAsync(userDto.UserName) != null)
                {
                    return new OperationDetails(false, "Login is already in use", "UserName");
                }

                User user = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
                if (user == null)
                {
                    user = new User { Email = userDto.Email, UserName = userDto.UserName };
                    var result = await unitOfWork.UserManager.CreateAsync(user, userDto.Password);
                    if (result.Errors.Count() > 0)
                        return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                    await unitOfWork.UserManager.AddToRoleAsync(user.Id, role.Name);
                    await unitOfWork.SaveAsync();
                    return new OperationDetails(true, "Registration completed successfully", "");
                }
                else
                {
                    return new OperationDetails(false, "Email address already in use", "Email");
                }
            }
            catch(Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;

            try
            {
                User userByEmail = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
                User user = await unitOfWork.UserManager.FindAsync(userByEmail.UserName, userDto.Password);
                if (user != null)
                    claim = await unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch { }
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await unitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new IdentityRole { Name = roleName };
                    await unitOfWork.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            try
            {
                return MapperBag.UserMapper.Map(unitOfWork.UserManager.Users.ToList());
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<UserDTO> GetByRequest(IEnumerable<SkillRequirementDTO> request)
        {
            if(request == null)
            {
                return null;
            }
            List<UserDTO> result = new List<UserDTO>();
            try
            {
                var users = unitOfWork.UserManager.Users.ToList();
                foreach (User user in users)
                {
                    if (user.SkillSet == null)
                        continue;
                    if (SkillSetFilter.SkillSetFulfilsRequest(MapperBag.SkillGradeMapper.Map(user.SkillSet.SkillGrades), request))
                    {
                        result.Add(MapperBag.UserMapper.Map(user));
                    }
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}

