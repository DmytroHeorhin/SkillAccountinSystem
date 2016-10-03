using System.Collections.Generic;
using System.Threading.Tasks;
using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Interfaces
{
    /// <summary>
    /// Contains methods for getting, adding and deleting requests. 
    /// </summary>
    public interface IRequestService 
    {
        Task<OperationDetails> Create(RequestDTO requestDTO);
        Task<IEnumerable<RequestDTO>> Find(string userName);
        Task<RequestDTO> Find(string userName, string requestName);
        Task<OperationDetails> Delete(RequestDTO requestDTO);
        Task<OperationDetails> DeleteAll(string userName);
    }
}