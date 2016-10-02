using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAS.WEB.Models
{
    public class SkillModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter skill name")]
        [Display(Name = "Skill name")]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public SkillGradeModel SkillGrade { get; set; }
    }
}