using System.Collections.Generic;

namespace SAS.WEB.Models
{
    public class SkillSetModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<SkillGradeModel> SkillGrades { get; set; }
    }     
}