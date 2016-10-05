using SAS.BLL.Interfaces;
using SAS.WEB.Mapping;
using SAS.WEB.Models;
using SAS.WEB.Util;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        IServiceBag serviceBag;
        ModelBuilder modelBuilder;

        public UserProfileController(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
            modelBuilder = new ModelBuilder(serviceBag);
        }
      
        [HttpGet]
        public async Task<ActionResult> UserProfile()
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var model = MapperBag.UserProfileMapper.Map(await serviceBag.UserProfileService.FindByNameAsync(userName));
            if (model == null)
                return Redirect("/UserProfile/EditProfile");
            return View(model);
        }

        [HttpGet]
        public ActionResult _UserProfile(UserProfileModel model)
        {
            return View(model);
        }
       
        [HttpGet]
        public async Task<ActionResult> ProfileOfUser(string userName)
        {
            var profile = MapperBag.UserProfileMapper.Map(await serviceBag.UserProfileService.FindByNameAsync(userName));
            if (profile == null)
                return new HttpNotFoundResult(); 
            var skillSet = await modelBuilder.ObtainSkillSetVievModel(userName);
            if (skillSet == null)
                return new HttpNotFoundResult();
            ProfileAndSkillsModel model = new ProfileAndSkillsModel() { Profile = profile, SkillSet = skillSet };
            return View(model);
        } 

        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var model = MapperBag.UserProfileMapper.Map(await serviceBag.UserProfileService.FindByNameAsync(userName));
            if (model == null)
                model = new UserProfileModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(UserProfileModel model)
        {
            model.UserName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var profile = MapperBag.UserProfileMapper.Map(model);
            var operationDetails = await serviceBag.UserProfileService.UpdateUserProfileAsync(profile);
            if (!operationDetails.Succeeded)
            {
                ViewBag.Massage = operationDetails.Message;
                return View("temp");
            }
            if (operationDetails.Succeeded)
                return Redirect("/UserProfile/UserProfile");
            else
            {
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }                 
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            serviceBag.Dispose();
            base.Dispose(disposing);
        }
    }
}