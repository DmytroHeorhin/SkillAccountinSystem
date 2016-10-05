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
    /// <summary>
    /// Contains methods for managing users. 
    /// </summary>
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
                var role = unitOfWork.RoleManager.FindByName(userDto.Role);
                if (role == null)
                {
                    return new OperationDetails(false, "User with invalid role has been provided", "Role");
                }
                if (await unitOfWork.UserManager.FindByNameAsync(userDto.UserName) != null)
                {
                    return new OperationDetails(false, "Login is already in use", "UserName");
                }

                var user = await unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
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
                    return new OperationDetails(false, "Email address is already in use", "Email");
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

        public OperationDetails UpdateRoles(UserDTO userDto)
        {
            if (userDto == null)
                return new OperationDetails(false, "Empty user has been provided", "");
            string userName = userDto.UserName;
            if (userName == null || userDto.UserName == string.Empty)
                return new OperationDetails(false, "User name is required", "UserName");
            try
            {
                var user = Find(userName);
                if (user == null)
                    return new OperationDetails(false, "User does not exist", "UserName");
                foreach (string role in user.Roles)
                    if (userDto.Roles.Where(r => r == role).FirstOrDefault() == null)
                        unitOfWork.UserManager.RemoveFromRole(user.Id, role);
                foreach (string role in userDto.Roles)
                    if (user.Roles.Where(r => r == role).FirstOrDefault() == null)
                        unitOfWork.UserManager.AddToRole(user.Id, role);
                return new OperationDetails(true, "User roles have been successfully updated", "");
            }
            catch(Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public IEnumerable<UserDTO> GetAll()
        {
            try
            {
                var users = MapperBag.UserMapper.Map(unitOfWork.UserManager.Users.ToList());
                foreach(UserDTO user in users)
                {
                    user.Roles = unitOfWork.UserManager.GetRoles(user.Id);
                }
                return users;
            }
            catch
            {
                return null;
            }
        }

        public UserDTO Find(string userName)
        {
            try
            {
                var user = MapperBag.UserMapper.Map(unitOfWork.UserManager.FindByName(userName));
                if (user == null)
                    return null;
                user.Roles = unitOfWork.UserManager.GetRoles(user.Id);
                return user;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Gets collection of users which fulfills the given skill requirements.
        /// </summary>
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

