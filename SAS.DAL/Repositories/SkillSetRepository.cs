using SAS.DAL.EF;
using SAS.DAL.Entities;

namespace SAS.DAL.Repositories
{
    public class SkillSetRepository : BaseRepository<SkillSet>
    {
        public SkillSetRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
