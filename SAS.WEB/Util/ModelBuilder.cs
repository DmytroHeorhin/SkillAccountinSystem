using PagedList;
using SAS.BLL.Interfaces;
using SAS.WEB.Mapping;
using SAS.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS.WEB.Util
{
    public class ModelBuilder
    {
        IServiceBag serviceBag;

        public ModelBuilder(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
        }

        public SkillSetViewModel ObtainSkillSetVievModel()
        {        
            return ConstructSkillSetVievModel(new List<SkillGradeModel>());
        }

        public async Task<SkillSetViewModel> ObtainSkillSetVievModel(string userName)
        {
            var skillGrades = MapperBag.SkillGradeMapper.Map(await serviceBag.SkillSetService.Find(userName));
            if (skillGrades == null)
                return null;
            return ConstructSkillSetVievModel(skillGrades);
        }

        public async Task<RequestViewModel> ObtainRequestViewModel(string userName, string requestName, int pageNumber)
        {
            var request = await serviceBag.RequestService.Find(userName, requestName);
            if (request == null)
                return null;
            RequestViewModel model = MapperBag.RequestViewMapper.Map(request);
            model.Request = ConstructSkillSetVievModel(MapperBag.SkillRequirementMapper.Map(request.SkillRequirementDTOs));
            model.Users = MapperBag.UserMapper.Map(serviceBag.UserService.GetByRequest(request.SkillRequirementDTOs)).ToPagedList(pageNumber, 10);
            return model;
        }

        private SkillSetViewModel ConstructSkillSetVievModel(IEnumerable<SkillGradeModel> grades)
        {
            SkillSetViewModel model = new SkillSetViewModel();          
            model.Categories = MapperBag.CategoryViewMapper.Map(serviceBag.CategoryService.GetAll());
            foreach (CategoryViewModel category in model.Categories)
            {
                category.Skills = MapperBag.SkillMapper.Map(serviceBag.SkillService.GetSkillsOfCategory(category.Id));
                if (grades.Count() != 0)
                {
                    foreach (SkillModel skill in category.Skills)
                    {
                        skill.SkillGrade = grades.Where(sg => sg.SkillId == skill.Id).FirstOrDefault();
                        if (skill.SkillGrade == null)
                        {
                            skill.SkillGrade = new SkillGradeModel();
                        }
                        if (!category.ContainsPositiveSkillGrades && skill.SkillGrade.Grade > 0)
                        {
                            category.ContainsPositiveSkillGrades = true;
                        }
                    }
                }  
                else
                {
                    foreach (SkillModel skill in category.Skills)
                    {
                        skill.SkillGrade = new SkillGradeModel();
                    }
                }     
            }
            return model;
        }
    }
}