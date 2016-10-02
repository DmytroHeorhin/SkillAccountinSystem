using SAS.BLL.Interfaces;
using SAS.WEB.Mapping;
using SAS.WEB.Models;
using SAS.WEB.Util;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Controllers
{
    [Authorize]
    public class SkillSetController : Controller
    {
        IServiceBag serviceBag;
        ModelBuilder modelBuilder;
        
        public SkillSetController(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
            modelBuilder = new ModelBuilder(ServiceBag);
        }

        [HttpGet]
        public async Task<ActionResult> EditSkillSet()
        {
            var model = await modelBuilder.ObtainSkillSetVievModel(HttpContext.GetOwinContext().Authentication.User.Identity.Name);
            if (model == null)
                return new HttpNotFoundResult();
            return View(model);
        }      

        [HttpPost]
        public async Task<ActionResult> EditSkillSet(int[] skills, int[] grades)
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var skillGrades = PositiveSkillGradesRetriver.Retrive(skills, grades);
            var skillSet = ComplexMapper.Map(new SkillSetModel() { UserName = userName, SkillGrades = skillGrades });
            var opDet = await serviceBag.SkillSetService.UpdateSkillSet(skillSet);
            if (!opDet.Succeeded)
            {
                ViewBag.Massage = opDet.Message;
                return View("Unsuccessful");
            }    
            return Redirect("/SkillSet/SkillSet");
        }      

        [HttpGet]
        public async Task<ActionResult> SkillSet()
        {
            var model = await modelBuilder.ObtainSkillSetVievModel(HttpContext.GetOwinContext().Authentication.User.Identity.Name);
            if(model == null)
                return new HttpNotFoundResult();
            if(model.Categories.Where(c => c.ContainsPositiveSkillGrades).Count() == 0)
                return Redirect("/SkillSet/EditSkillSet");
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            serviceBag.Dispose();
            base.Dispose(disposing);
        }
    }
}