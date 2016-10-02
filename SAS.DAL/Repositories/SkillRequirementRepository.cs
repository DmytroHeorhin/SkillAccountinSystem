using SAS.DAL.Entities;
using SAS.DAL.EF;

namespace SAS.DAL.Repositories
{
    public class SkillRequirementRepository : BaseRepository<SkillRequirement>
    {
        public SkillRequirementRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
