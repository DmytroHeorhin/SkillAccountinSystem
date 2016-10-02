using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAS.DAL.Entities
{
    public class Category : BaseEntity
    {
        [MaxLength(30)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        public virtual List<Skill> Skills { get; set; }
    }
}