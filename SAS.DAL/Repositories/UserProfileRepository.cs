using SAS.DAL.Entities;
using SAS.DAL.EF;

namespace SAS.DAL.Repositories
{
    public class UserProfileRepository : BaseRepository<UserProfile>
    {
        public UserProfileRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
