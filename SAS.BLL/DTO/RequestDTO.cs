using System;
using System.Collections.Generic;

namespace SAS.BLL.DTO
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }   
        public string UserName { get; set; } 
        public DateTime DateTime { get; set; }  
        public List<SkillRequirementDTO> SkillRequirementDTOs { get; set; }
    }
}
