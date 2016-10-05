using SAS.BLL.DTO;
using SAS.WEB.Models;

namespace SAS.WEB.Mapping
{
    public static class MapperBag
    {
        public static MapperWrapper<SkillGradeDTO, SkillGradeModel> SkillGradeMapper
        {
            get { return new MapperWrapper<SkillGradeDTO, SkillGradeModel>(); }
        }

        public static MapperWrapper<SkillRequirementDTO, SkillGradeModel> SkillRequirementMapper
        {
            get { return new MapperWrapper<SkillRequirementDTO, SkillGradeModel>(); }
        }

        public static MapperWrapper<UserDTO, UserModel> UserMapper
        {
            get { return new MapperWrapper<UserDTO, UserModel>(); }
        }

        public static MapperWrapper<UserProfileDTO, UserProfileModel> UserProfileMapper
        {
            get { return new MapperWrapper<UserProfileDTO, UserProfileModel>(); }
        }

        public static MapperWrapper<CategoryDTO, CategoryViewModel> CategoryViewMapper
        {
            get { return new MapperWrapper<CategoryDTO, CategoryViewModel>(); }
        }

        public static MapperWrapper<CategoryDTO, CategoryModel> CategoryMapper
        {
            get { return new MapperWrapper<CategoryDTO, CategoryModel>(); }
        }

        public static MapperWrapper<SkillDTO, SkillModel> SkillMapper
        {
            get { return new MapperWrapper<SkillDTO, SkillModel>(); }
        }

        public static MapperWrapper<SkillSetDTO, SkillSetModel> SkillSetMapper
        {
            get { return new MapperWrapper<SkillSetDTO, SkillSetModel>(); }
        }

        public static MapperWrapper<RequestDTO, RequestModel> RequestMapper
        {
            get { return new MapperWrapper<RequestDTO, RequestModel>(); }
        }

        public static MapperWrapper<RequestDTO, RequestViewModel> RequestViewMapper
        {
            get { return new MapperWrapper<RequestDTO, RequestViewModel>(); }
        }
    }
}