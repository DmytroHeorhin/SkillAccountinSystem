using System.Collections.Generic;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    /// <summary>
    /// Contains methods for getting, adding and deleting skills. 
    /// </summary>
    public interface ISkillService
    {
        IEnumerable<SkillDTO> GetSkillsOfCategory(int categoryId);
        OperationDetails Create(SkillDTO skillDTO);
    }
}