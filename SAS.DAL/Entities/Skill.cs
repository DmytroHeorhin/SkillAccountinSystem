using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.DAL.Entities
{
    public class Skill : BaseEntity
    {
        [MaxLength(30)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }

        public virtual List<SkillGrade> SkillGrades { get; set; }
    }
}
