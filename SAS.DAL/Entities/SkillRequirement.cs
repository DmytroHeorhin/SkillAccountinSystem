using System.ComponentModel.DataAnnotations;

namespace SAS.DAL.Entities
{
    public class SkillRequirement : BaseEntity
    {
        [Required]
        public virtual Skill Skill { get; set; }

        public int Grade { get; set; }

        [Required]
        public virtual Request Request { get; set; }
    }
}
