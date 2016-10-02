using SAS.BLL.Interfaces;
using SAS.WEB.Mapping;
using SAS.WEB.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        IServiceBag serviceBag;

        public UserProfileController(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
        }

        [HttpGet]
        public async Task<ActionResult> UserProfile()
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var model = MapperBag.UserProfileMapper.Map(await serviceBag.UserProfileService.FindByNameAsync(userName));
            if (model == null)
                return Redirect("/UserProfile/EditProfile");
            model.UserName = userName; // do I need this??
            return View(model);
        }
       
        [HttpGet]
        public async Task<ActionResult> ProfileOfUser(string userName)
        {
            var model = MapperBag.UserProfileMapper.Map(await serviceBag.UserProfileService.FindByNameAsync(userName));
            if (model == null)
                return new HttpNotFoundResult();  
            model.UserName = userName; // do I need this??
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