using SAS.DAL.EF;
using SAS.DAL.Entities;

namespace SAS.DAL.Repositories
{
    public class SkillRepository : BaseRepository<Skill>
    {
        public SkillRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
