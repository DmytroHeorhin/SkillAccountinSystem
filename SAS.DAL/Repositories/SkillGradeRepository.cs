using SAS.DAL.EF;
using SAS.DAL.Entities;

namespace SAS.DAL.Repositories
{
    public class SkillGradeRepository : BaseRepository<SkillGrade>
    {
        public SkillGradeRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
