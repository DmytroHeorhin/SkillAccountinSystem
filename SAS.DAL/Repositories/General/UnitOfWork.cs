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

        public UnitOfWork()
        {
            _dbContext = new SASContext();  
        }

        #region Properties
        public UserManager<User> UserManager
        {
            get
            {
                return new UserManager<User>(new UserStore<User>(_dbContext));
            }
        }

        public RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext));
            }
        }

        public IRepository<UserProfile> UserProfileRepository
        {
            get
            {
                return new UserProfileRepository(_dbContext);
            }
        }

        public IRepository<Category> CategoryRepository
        {
            get
            {
                return new CategoryRepository(_dbContext);
            }
        }

        public IRepository<Skill> SkillRepository
        {
            get
            {
                return new SkillRepository(_dbContext);
            }
        }

        public IRepository<SkillGrade> SkillGradeRepository
        {
            get
            {
                return new SkillGradeRepository(_dbContext);
            }
        }

        public IRepository<SkillSet> SkillSetRepository
        {
            get
            {
                return new SkillSetRepository(_dbContext);
            }
        }

        public IRepository<SkillRequirement> SkillRequirementRepository
        {
            get
            {
                return new SkillRequirementRepository(_dbContext);
            }
        }

        public IRepository<Request> RequestRepository
        {
            get
            {
                return new RequestRepository(_dbContext);
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
