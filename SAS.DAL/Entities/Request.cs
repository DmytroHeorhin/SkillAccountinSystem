using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.DAL.Entities
{
    public class Request : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime DateTime { get; set; }

        public virtual List<SkillRequirement> SkillRequirements { get; set; }

        public Request()
        {
            SkillRequirements = new List<SkillRequirement>();
        }
    }
}
