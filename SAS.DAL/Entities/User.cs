using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace SAS.DAL.Entities
{
    public class User : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        public virtual SkillSet SkillSet { get; set; }
        public virtual List<Request> Requests { get; set; }
    }
}

