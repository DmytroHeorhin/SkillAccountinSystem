using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SAS.DAL.EF;
using SAS.DAL.Entities;
using SAS.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace SAS.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        SASContext _dbContext;
        UserManager<User> userManager;
        RoleManager<IdentityRole> roleManager;
        IRepository<UserProfile> userProfileRepository;
        IRepository<Category> categoryRepository;
        IRepository<Skill> skillRepository;
        IRepository<SkillGrade> skillGradeRepository;
        IRepository<SkillSet> skillSetRepository;
        IRepository<SkillRequirement> skillRequirementRepository;
        IRepository<Request> requestRepository;

        public UnitOfWork()
        {
            _dbContext = new SASContext();  
        }

        #region Properties
        public UserManager<User> UserManager
        {
            get
            {
                if(userManager == null)
                    userManager = new UserManager<User>(new UserStore<User>(_dbContext));
                return userManager;
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                if(roleManager == null)
                    roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
                return roleManager;
            }
        }

        public IRepository<UserProfile> UserProfileRepository
        {
            get
            {
                if(userProfileRepository == null)
                    userProfileRepository = new UserProfileRepository(_dbContext);
                return userProfileRepository;
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                if(categoryRepository == null)
                    categoryRepository = new CategoryRepository(_dbContext);
                return categoryRepository;
            }
        }

        public IRepository<Skill> SkillRepository
        {
            get
            {
                if(skillRepository == null)
                    skillRepository = new SkillRepository(_dbContext);
                return skillRepository;
            }
        }

        public IRepository<SkillGrade> SkillGradeRepository
        {
            get
            {
                if(skillGradeRepository == null)
                    skillGradeRepository = new SkillGradeRepository(_dbContext);
                return skillGradeRepository;
            }
        }

        public IRepository<SkillSet> SkillSetRepository
        {
            get
            {
                if(skillSetRepository == null)
                    skillSetRepository= new SkillSetRepository(_dbContext);
                return skillSetRepository;
            }
        }

        public IRepository<SkillRequirement> SkillRequirementRepository
        {
            get
            {
                if(skillRequirementRepository == null)
                    skillRequirementRepository = new SkillRequirementRepository(_dbContext);
                return skillRequirementRepository;
            }
        }

        public IRepository<Request> RequestRepository
        {
            get
            {
                if(requestRepository == null)
                    requestRepository = new RequestRepository(_dbContext);
                return requestRepository;
            }
        }     
        #endregion

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
