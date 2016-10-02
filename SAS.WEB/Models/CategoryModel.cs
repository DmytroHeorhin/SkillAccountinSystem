using System.ComponentModel.DataAnnotations;

namespace SAS.WEB.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter category name")]
        [Display(Name = "Category name")]
        public string Name { get; set; }
    }
}