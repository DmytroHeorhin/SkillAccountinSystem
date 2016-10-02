using SAS.DAL.Entities;
using SAS.DAL.EF;

namespace SAS.DAL.Repositories
{
    public class RequestRepository : BaseRepository<Request>
    {
        public RequestRepository(SASContext dbContext) : base(dbContext)
        {
        }
    }
}
