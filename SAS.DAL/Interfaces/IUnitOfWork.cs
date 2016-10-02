using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SAS.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SAS.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        IRepository<UserProfile> UserProfileRepository { get; }
        IRepository<Category> CategoryRepository { get; }
        IRepository<Skill> SkillRepository { get; }
        IRepository<SkillGrade> SkillGradeRepository { get; }
        IRepository<SkillSet> SkillSetRepository { get; }
        IRepository<SkillRequirement> SkillRequirementRepository { get; }
        IRepository<Request> RequestRepository { get; }

        Task SaveAsync();
        void Save();
    }
}
