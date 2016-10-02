using SAS.WEB.Models;
using System.Collections.Generic;

namespace SAS.WEB.Util
{
    public static class PositiveSkillGradesRetriver
    {
        public static IEnumerable<SkillGradeModel> Retrive(int[] skills, int[] grades)
        {
            if (grades.Length == 0 || skills.Length != grades.Length)
                return null;
            List<SkillGradeModel> result = new List<SkillGradeModel>();
            for (int i = 0; i < skills.Length; i++)
            {
                if (grades[i] != 0)
                {
                    result.Add(new SkillGradeModel() { SkillId = skills[i], Grade = grades[i] });
                }
            }
            return result;
        }
    }
}