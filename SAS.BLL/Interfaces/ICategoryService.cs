using System.Collections.Generic;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    /// <summary>
    /// Contains methods for getting, adding and deleting categories. 
    /// </summary>
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetAll();
        OperationDetails Create(CategoryDTO category);
    }
}