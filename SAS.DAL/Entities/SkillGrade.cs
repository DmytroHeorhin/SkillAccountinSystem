using System.ComponentModel.DataAnnotations;

namespace SAS.DAL.Entities
{
    public class SkillGrade : BaseEntity
    {
        [Required]
        public virtual Skill Skill { get; set; }

        public int Grade { get; set; }

        [Required]
        public virtual SkillSet SkillSet { get; set; }
    }
}
