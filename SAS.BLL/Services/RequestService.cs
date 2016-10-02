using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;
using SAS.BLL.Interfaces;
using SAS.BLL.Mapping;
using SAS.DAL.Entities;
using SAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAS.BLL.Services
{
    public class RequestService : IRequestService
    {
        IUnitOfWork unitOfWork;
        ComplexMapper mapper;

        public RequestService(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
            mapper = new ComplexMapper(unitOfWork);
        }

        public async Task<OperationDetails> Create(RequestDTO requestDTO)
        {
            //try
           // {
                if (requestDTO == null)
                {
                    return new OperationDetails(false, "Empty request has been provided", "");
                }
                string userName = requestDTO.UserName;
                if (userName == null || userName == string.Empty)
                {
                    return new OperationDetails(false, "Empty user name has been provided", "UserName");
                }
                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new OperationDetails(false, "User does not exist", "UserName");
                }
                string requestName = requestDTO.Name;
                if (requestName == null || requestName == string.Empty)
                {
                    return new OperationDetails(false, "Empty request name has been provided", "Name");
                }
                if (requestDTO.DateTime == null)
                    requestDTO.DateTime = DateTime.Now;

                var requests = user.Requests;
                while (requests.Where(r => r.Name == requestName).FirstOrDefault() != null)
                {
                    requestName = ModifyClaimedName(requestName);
                }
                requestDTO.Name = requestName;

                var request = mapper.Map(requestDTO);
                unitOfWork.RequestRepository.Add(request);
                await unitOfWork.SaveAsync();

                request = unitOfWork.RequestRepository.SearchFor(r => r.User.UserName == userName).Where(r => r.Name == requestName).First();
                foreach (SkillRequirementDTO srDTO in requestDTO.SkillRequirementDTOs)
                {
                    srDTO.RequestId = request.Id;
                    var sr = mapper.Map(srDTO); ;
                    unitOfWork.SkillRequirementRepository.Add(sr);
                }
                await unitOfWork.SaveAsync();
                return new OperationDetails(true, "Request was successfully created", requestName);
           // }
           // catch(Exception ex)
           // {
            //    return new OperationDetails(false, ex.Message, "");
           // }
        }

        public async Task<IEnumerable<RequestDTO>> Find(string userName)
        {
            if (userName == null || userName == string.Empty)
                return null;
            try
            {
                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return null;
                }
                return MapperBag.RequestMapper.Map(user.Requests.OrderByDescending(r => r.DateTime));
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<RequestDTO> Find(string userName, string requestName)
        {
            if (userName == null || userName == string.Empty)
                return null;
            if (requestName == null || requestName == string.Empty)
                return null;
            try
            {
                User user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return null;
                }
                var request = user.Requests.Where(r => r.Name == requestName).FirstOrDefault();
                if (request == null)
                {
                    return null;
                }
                request.DateTime = DateTime.Now;
                unitOfWork.RequestRepository.Update(request);
                await unitOfWork.SaveAsync();

                var result = MapperBag.RequestMapper.Map(request);
                result.SkillRequirementDTOs = MapperBag.SkillRequirementMapper.Map(request.SkillRequirements).ToList();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<OperationDetails> Delete(RequestDTO requestDTO)
        {
            try
            {
                if (requestDTO == null)
                {
                    return new OperationDetails(false, "Empty request has been provided", "");
                }
                string userName = requestDTO.UserName;
                if (userName == null || userName == string.Empty)
                {
                    return new OperationDetails(false, "Empty user name has been provided", "UserName");
                }
                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new OperationDetails(false, "User does not exist", "UserName");
                }
                string requestName = requestDTO.Name;
                if (requestName == null || requestName == string.Empty)
                {
                    return new OperationDetails(false, "Empty request name has been provided", "Name");
                }
                var request = unitOfWork.RequestRepository.SearchFor(r => r.User.UserName == userName).Where(r => r.Name == requestName).First();
                unitOfWork.RequestRepository.Delete(request.Id);               
                await unitOfWork.SaveAsync();
                return new OperationDetails(true, "Request was successfully deleted", requestName);
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public async Task<OperationDetails> DeleteAll(string userName)
        {
            try
            {               
                if (userName == null || userName == string.Empty)
                {
                    return new OperationDetails(false, "Empty user name has been provided", "");
                }
                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new OperationDetails(false, "User does not exist", "");
                }
                var requests = unitOfWork.RequestRepository.SearchFor(r => r.User.UserName == userName);
                foreach (var request in requests)
                {
                    unitOfWork.RequestRepository.Delete(request.Id);
                }
                await unitOfWork.SaveAsync();
                return new OperationDetails(true, "All requests were successfully deleted", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        static string ModifyClaimedName(string name)
        {
            int newIndex = 1;
            Regex hasSuffix = new Regex("\\([0-9]+\\)$");
            if (hasSuffix.IsMatch(name))
            {
                int i = 0;
                Regex suffixValue = new Regex("\\(" + i.ToString() + "\\)$");
                while (!suffixValue.IsMatch(name))
                {
                    i++;
                    suffixValue = new Regex("\\(" + i.ToString() + "\\)$");
                }
                char[] charsToTrimEnd = "1234567890()".ToCharArray();
                name = name.TrimEnd(charsToTrimEnd);
                newIndex = ++i;
            }
            string newSuffix = "(" + newIndex.ToString() + ")";
            name = name + newSuffix;
            return name;
        }
    }
}
