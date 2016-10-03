using SAS.BLL.DTO;
using SAS.BLL.Interfaces;
using SAS.BLL.Mapping;
using SAS.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;
using SAS.BLL.Infrastructure;

namespace SAS.BLL.Services
{
    /// <summary>
    /// Contains methods for getting, adding and deleting skills. 
    /// </summary>
    public class SkillService : ISkillService
    {
        IUnitOfWork unitOfWork;
        ComplexMapper mapper;

        public SkillService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            mapper = new ComplexMapper(unitOfWork);
        }
     
        public IEnumerable<SkillDTO> GetSkillsOfCategory(int categoryId)
        {
            try
            { 
                return MapperBag.SkillMapper.Map(unitOfWork.SkillRepository.SearchFor(s => s.Category.Id == categoryId).ToList());
            } 
            catch
            {
                return null;
            }
        }

        public OperationDetails Create(SkillDTO skillDTO)
        {
            if (skillDTO == null)
                return new OperationDetails(false, "Empty category has been provided", "");
            if (skillDTO.Name == null || skillDTO.Name == string.Empty)
                return new OperationDetails(false, "Skill name required", "Name");
            if (skillDTO.CategoryId == 0)
                return new OperationDetails(false, "Category required", "CategoryId");
            try
            {
                var skill = mapper.Map(skillDTO);
                if (unitOfWork.SkillRepository.SearchFor(s => s.Name == skill.Name).FirstOrDefault() != null)
                    return new OperationDetails(false, "Skill with the given name already exists", "Name");               
                unitOfWork.SkillRepository.Add(skill);
                unitOfWork.Save();
                return new OperationDetails(true, "Skill was successfully created", "");
            }
            catch(Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }
    }
}
