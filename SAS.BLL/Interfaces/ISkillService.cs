using System.Collections.Generic;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    public interface ISkillService
    {
        IEnumerable<SkillDTO> GetSkillsOfCategory(int categoryId);
        OperationDetails Create(SkillDTO skillDTO);
    }
}