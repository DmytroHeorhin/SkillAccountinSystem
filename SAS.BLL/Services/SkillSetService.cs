using SAS.BLL.DTO;
using SAS.BLL.Infrastructure;
using SAS.BLL.Interfaces;
using SAS.BLL.Mapping;
using SAS.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAS.BLL.Services
{
    public class SkillSetService : ISkillSetService
    {
        IUnitOfWork unitOfWork;
        ComplexMapper mapper;

        public SkillSetService(IUnitOfWork UnitOfWork)
        {
            unitOfWork = UnitOfWork;
            mapper = new ComplexMapper(unitOfWork);
        }

        public async Task<OperationDetails> UpdateSkillSet(SkillSetDTO skillSet)
        {
            try
            {
                if (skillSet == null)
                    return new OperationDetails(false, "Empty skill set has been provided", "");

                string userName = skillSet.UserName;
                if (userName == null || userName == string.Empty)
                    return new OperationDetails(false, "Empty user name has been provided", "UserName");
                var skillGrades = skillSet.SkillGradeDTOs;
                if (skillGrades == null)
                    throw new ValidationException("Empty skill grades collection has been provided", "SkillGrades");

                var user = await unitOfWork.UserManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new OperationDetails(false, "User does not exist", "UserName");
                }
                if (user.SkillSet != null)
                {
                    unitOfWork.SkillSetRepository.Delete(user.SkillSet.Id);
                    user.SkillSet = null;
                    await unitOfWork.UserManager.UpdateAsync(user);
                }               

                unitOfWork.SkillSetRepository.Add(mapper.Map(skillSet));
                unitOfWork.Save();

                UserDTO userDTO = MapperBag.UserMapper.Map(await unitOfWork.UserManager.FindByNameAsync(userName));
                foreach (SkillGradeDTO sg in skillGrades)
                {
                    sg.SkillSetId = userDTO.SkillSetId;
                    unitOfWork.SkillGradeRepository.Add(mapper.Map(sg));
                }
                await unitOfWork.SaveAsync();
                return new OperationDetails(true, "Skill set successfully updated", "");
            }
            catch (Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }

        public async Task<IEnumerable<SkillGradeDTO>> Find(string userName)
        {
            if (userName == null || userName == string.Empty)
                return null;
            try
            {               
                UserDTO userDTO = MapperBag.UserMapper.Map(await unitOfWork.UserManager.FindByNameAsync(userName));
                if (userDTO == null)
                {
                    return null;
                }
                var skillSet = unitOfWork.SkillSetRepository.Get(userDTO.SkillSetId);
                return MapperBag.SkillGradeMapper.Map(skillSet.SkillGrades);
            }
            catch
            {
                return null;
            }
        }
    }
}
