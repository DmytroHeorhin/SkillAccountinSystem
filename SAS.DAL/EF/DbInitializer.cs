using System.Data.Entity;
using SAS.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using RandomNameGenerator;
using System.Linq;

namespace SAS.DAL.EF
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<SASContext>
    {
        protected override void Seed(SASContext db)
        {
            Category PL = db.Categories.Add(new Category { Name = "Programming languages" });
            Category OS = db.Categories.Add(new Category { Name = "Operating systems" });
            Category Db = db.Categories.Add(new Category { Name = "Databases" });
            Category P = db.Categories.Add(new Category { Name = "Platforms" });

            Skill ABAP = db.Skills.Add(new Skill() { Name = "ABAP", Category = PL });
            Skill CS = db.Skills.Add(new Skill { Name = "C#", Category = PL });
            Skill Cpp = db.Skills.Add(new Skill { Name = "C++", Category = PL });
            Skill Cobol = db.Skills.Add(new Skill { Name = "COBOL", Category = PL });
            db.Skills.Add(new Skill { Name = "Delphi", Category = PL });
            db.Skills.Add(new Skill { Name = "Flash", Category = PL });
            db.Skills.Add(new Skill { Name = "Flex", Category = PL });
            db.Skills.Add(new Skill { Name = "Java", Category = PL });
            db.Skills.Add(new Skill { Name = "JavaScript/HTML/CSS", Category = PL });
            db.Skills.Add(new Skill { Name = "Perl", Category = PL });
            db.Skills.Add(new Skill { Name = "PHP", Category = PL });
            db.Skills.Add(new Skill { Name = "Python", Category = PL });
            db.Skills.Add(new Skill { Name = "Ruby", Category = PL });
            db.Skills.Add(new Skill { Name = "SQL", Category = PL });
            db.Skills.Add(new Skill { Name = "VB", Category = PL });
            db.Skills.Add(new Skill { Name = "XML/XSL/XSLT", Category = PL });
            db.Skills.Add(new Skill { Name = "Android", Category = OS });
            db.Skills.Add(new Skill { Name = "BlackBerry", Category = OS });
            db.Skills.Add(new Skill { Name = "iOS", Category = OS });
            db.Skills.Add(new Skill { Name = "Symbian", Category = OS });
            db.Skills.Add(new Skill { Name = "Unix/Linux/Solaris", Category = OS });
            db.Skills.Add(new Skill { Name = "Windows", Category = OS });
            db.Skills.Add(new Skill { Name = "WindowsPhone", Category = OS });
            db.Skills.Add(new Skill { Name = "AMAZON", Category = P });
            db.Skills.Add(new Skill { Name = "ATG", Category = P });
            db.Skills.Add(new Skill { Name = "AZURE", Category = P });
            db.Skills.Add(new Skill { Name = "COGNOS", Category = P });
            db.Skills.Add(new Skill { Name = "DAY", Category = P });
            db.Skills.Add(new Skill { Name = "DOCUMENTUM", Category = P });
            db.Skills.Add(new Skill { Name = "SAP", Category = P });
            db.Skills.Add(new Skill { Name = "SFcom/FORCE", Category = P });
            db.Skills.Add(new Skill { Name = "SHAREPOINT", Category = P });
            db.Skills.Add(new Skill { Name = "Tibico", Category = P });
            db.Skills.Add(new Skill { Name = "WS Commerce", Category = P });
            Skill MSSQL = db.Skills.Add(new Skill { Name = "Microsoft SQL Server", Category = Db });
            db.Skills.Add(new Skill { Name = "Oracle", Category = Db });
            db.Skills.Add(new Skill { Name = "PostgreSQL", Category = Db });
            db.Skills.Add(new Skill { Name = "MySQL", Category = Db });
            db.Skills.Add(new Skill { Name = "NoSQL", Category = Db });
            db.Skills.Add(new Skill { Name = "ETL/OLAP/BI/Data", Category = Db });         
            Skill Mining = db.Skills.Add(new Skill { Name = "Mining", Category = Db });

            var userManager = new UserManager<User>(new UserStore<User>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var role1 = new IdentityRole { Name = "administrator" };
            var role2 = new IdentityRole { Name = "manager" };
            var role3 = new IdentityRole { Name = "user" };
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            string passw = "123Qwerty";
            var admUser = new User() { Email = "admin@sas.com", UserName = "Administrator" };            
            var manUser = new User() { Email = "manag@sas.com", UserName = "Manager" };
            var usUser = new User() { Email = "user@sas.com", UserName = "User" };
            var result1 = userManager.Create(admUser, passw);
            var result2 = userManager.Create(manUser, passw);
            var result3 = userManager.Create(usUser, passw);
            if (result1.Succeeded)
            {
                userManager.AddToRole(admUser.Id, role1.Name);
                userManager.AddToRole(admUser.Id, role2.Name);
                userManager.AddToRole(admUser.Id, role3.Name);
            }
            if (result2.Succeeded)
            {
                userManager.AddToRole(manUser.Id, role2.Name);
                userManager.AddToRole(manUser.Id, role3.Name);
            }
            if (result3.Succeeded)
            {
                userManager.AddToRole(usUser.Id, role3.Name);
            }

            SkillSet set = db.SkillSets.Add(new SkillSet() { User = usUser });
            usUser.SkillSet = set;
            SkillGrade grade1 = db.SkillGrades.Add(new SkillGrade() { SkillSet = set, Grade = 2, Skill = CS });
            SkillGrade grade2 = db.SkillGrades.Add(new SkillGrade() { SkillSet = set, Grade = 1, Skill = MSSQL });

            Random rand = new Random();           
            for(int i = 1; i < 50; i++)
            {
                string firstName = NameGenerator.GenerateFirstName((i % 2) == 0 ? Gender.Male : Gender.Female);
                string lastName = NameGenerator.GenerateLastName();
                var user = new User() { Email = firstName + "." +lastName + i.ToString() + "@sas.com", UserName = firstName + "_" + lastName + "_" + i.ToString()};
                var result = userManager.Create(user, passw);
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, role3.Name);                          
                }
                var profile = new UserProfile() { FirstName = firstName, LastName = lastName, Email = "e@ma.il", MobilePhone = "1234567890", User = user, ContactPhone = (i % 2) == 0 ? "WorkPhone" : "MobilePhone" };
                db.UserProfiles.Add(profile);
                var skillSet = db.SkillSets.Add(new SkillSet() { User = user });
                for (int j = 1; j < 10; j++)
                {
                    var skill = db.Skills.Where(s => s.Id == (i * j > 41 ? (i * j) % 41 : i * j)).First();
                    db.SkillGrades.Add(new SkillGrade() { SkillSet = skillSet, Grade = rand.Next(1, 4), Skill = skill });
                }
            }          
            db.SaveChanges();
        }
    }
}
