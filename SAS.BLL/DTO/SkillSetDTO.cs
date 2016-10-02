using System.Collections.Generic;

namespace SAS.BLL.DTO
{
    public class SkillSetDTO 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<SkillGradeDTO> SkillGradeDTOs { get; set; }
    }
}

