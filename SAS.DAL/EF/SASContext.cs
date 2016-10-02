using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using SAS.DAL.Entities;

namespace SAS.DAL.EF
{
    public class SASContext : IdentityDbContext<User>
    {
        public SASContext() : base("SASconn")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillGrade> SkillGrades { get; set; }
        public DbSet<SkillSet> SkillSets { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SkillRequirement> SkillRequierements { get; set; }
    }
}

