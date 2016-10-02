using SAS.DAL.EF;
using SAS.DAL.Entities;

namespace SAS.DAL.Repositories
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
