using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAS.WEB.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ContainsPositiveSkillGrades { get; set; }
        public IEnumerable<SkillModel> Skills { get; set; }
    }
}