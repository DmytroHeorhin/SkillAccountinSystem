using SAS.BLL.DTO;
using SAS.DAL.Entities;

namespace SAS.BLL.Mapping
{
    public static class MapperBag
    {
        public static MapperWrapper<Category, CategoryDTO> CategoryMapper
        {
            get { return new MapperWrapper<Category, CategoryDTO>(); }
        }

        public static MapperWrapper<Skill, SkillDTO> SkillMapper
        {
            get { return new MapperWrapper<Skill, SkillDTO>(); }
        }

        public static MapperWrapper<SkillSet, SkillSetDTO> SkillSetMapper
        {
            get { return new MapperWrapper<SkillSet, SkillSetDTO>(); }
        }

        public static MapperWrapper<SkillGrade, SkillGradeDTO> SkillGradeMapper
        {
            get { return new MapperWrapper<SkillGrade, SkillGradeDTO>(); }
        }

        public static MapperWrapper<SkillRequirement, SkillRequirementDTO> SkillRequirementMapper
        {
            get { return new MapperWrapper<SkillRequirement, SkillRequirementDTO>(); }
        }

        public static MapperWrapper<Request, RequestDTO> RequestMapper
        {
            get { return new MapperWrapper<Request, RequestDTO>(); }
        }

        public static MapperWrapper<User, UserDTO> UserMapper
        {
            get { return new MapperWrapper<User, UserDTO>(); }
        }

        public static MapperWrapper<UserProfile, UserProfileDTO> UserProfileMapper
        {
            get { return new MapperWrapper<UserProfile, UserProfileDTO>(); }
        }
    }
}
