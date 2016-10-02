using SAS.BLL.DTO;
using SAS.WEB.Models;
using System.Linq;

namespace SAS.WEB.Mapping
{
    public static class ComplexMapper
    {
        public static SkillSetDTO Map(SkillSetModel skillSet)
        {
            SkillSetDTO result = MapperBag.SkillSetMapper.Map(skillSet);
            result.SkillGradeDTOs = MapperBag.SkillGradeMapper.Map(skillSet.SkillGrades).ToList();
            return result;
        }

        public static RequestDTO Map(RequestModel requestModel)
        {
            RequestDTO result = MapperBag.RequestMapper.Map(requestModel);
            result.SkillRequirementDTOs = MapperBag.SkillRequirementMapper.Map(requestModel.SkillRequirements).ToList();
            return result;
        }
    }
}