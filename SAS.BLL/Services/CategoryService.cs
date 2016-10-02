using SAS.BLL.DTO;
using SAS.BLL.Interfaces;
using SAS.BLL.Mapping;
using SAS.DAL.Interfaces;
using System.Collections.Generic;
using System;
using SAS.BLL.Infrastructure;
using System.Linq;

namespace SAS.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
      
        public IEnumerable<CategoryDTO> GetAll()
        {
            try
            {
                return MapperBag.CategoryMapper.Map(unitOfWork.CategoryRepository.GetAll());
            }
            catch
            {
                return null;
            }
        }

        public OperationDetails Create(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return new OperationDetails(false, "Empty category has been provided", "");
            if (categoryDTO.Name == null || categoryDTO.Name == string.Empty)
                return new OperationDetails(false, "Category name required", "Name");
            try
            {                
                var cat = MapperBag.CategoryMapper.Map(categoryDTO);
                if (unitOfWork.CategoryRepository.SearchFor(c => c.Name == cat.Name).FirstOrDefault() != null)
                    return new OperationDetails(false, "Category with the given name already exists", "Name");
                unitOfWork.CategoryRepository.Add(cat);
                unitOfWork.Save();
                return new OperationDetails(true, "Category was successfully created", "");
            }
            catch(Exception ex)
            {
                return new OperationDetails(false, ex.Message, "");
            }
        }
    }
}
