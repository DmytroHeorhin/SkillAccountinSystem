using SAS.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAS.BLL.Infrastructure
{
    public static class SkillSetFilter
    {
        public static bool SkillSetFulfilsRequest(IEnumerable<SkillGradeDTO> SkillSet, IEnumerable<SkillRequirementDTO> Request)
        {           
            foreach(SkillRequirementDTO req in Request)
            {
                SkillGradeDTO skillGrade = SkillSet.Where(sg => sg.SkillId == req.SkillId).FirstOrDefault();
                if (skillGrade == null || skillGrade.Grade < req.Grade)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
