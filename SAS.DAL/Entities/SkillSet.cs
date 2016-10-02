using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAS.DAL.Entities
{
    public class SkillSet : BaseEntity
    {
        [Required]
        public User User { get; set; }

        public virtual List<SkillGrade> SkillGrades { get; set; }

        public SkillSet()
        {
            SkillGrades = new List<SkillGrade>();
        }
    }
}
