using System.Collections.Generic;
using System.Threading.Tasks;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    public interface ISkillSetService 
    {      
        Task<OperationDetails> UpdateSkillSet(SkillSetDTO skillSet);
        Task<IEnumerable<SkillGradeDTO>> Find(string userName);
    }
}