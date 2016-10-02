using SAS.BLL.DTO;
using SAS.DAL.Entities;
using SAS.DAL.Interfaces;

namespace SAS.BLL.Mapping
{
    public class ComplexMapper
    {
        IUnitOfWork unit;       

        public ComplexMapper(IUnitOfWork UnitOfWork)
        {
            unit = UnitOfWork;           
        }

        public SkillSet Map(SkillSetDTO skillSetDTO)
        {
            var result = MapperBag.SkillSetMapper.Map(skillSetDTO);
            result.User = unit.UserManager.FindByNameAsync(skillSetDTO.UserName).Result;
            return result;
        }

        public SkillGrade Map(SkillGradeDTO skillGradeDTO)
        {
            var result = MapperBag.SkillGradeMapper.Map(skillGradeDTO);
            result.SkillSet = unit.SkillSetRepository.Get(skillGradeDTO.SkillSetId);
            result.Skill = unit.SkillRepository.Get(skillGradeDTO.SkillId);
            return result;
        }

        public Request Map(RequestDTO requestDTO)
        {
            var result = MapperBag.RequestMapper.Map(requestDTO);
            result.User = unit.UserManager.FindByNameAsync(requestDTO.UserName).Result;
            return result;
        }

        public SkillRequirement Map(SkillRequirementDTO skillRequirementDTO)
        {
            var result = MapperBag.SkillRequirementMapper.Map(skillRequirementDTO);
            result.Request = unit.RequestRepository.Get(skillRequirementDTO.RequestId);
            result.Skill = unit.SkillRepository.Get(skillRequirementDTO.SkillId);
            return result;
        }

        public Skill Map(SkillDTO skillDTO)
        {
            var result = MapperBag.SkillMapper.Map(skillDTO);
            result.Category = unit.CategoryRepository.Get(skillDTO.CategoryId);
            return result;
        }
    }
}
