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
    public class RequestController : Controller
    {
        IServiceBag serviceBag;
        ModelBuilder modelBuilder;
        
        public RequestController(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
            modelBuilder = new ModelBuilder(ServiceBag);           
        }

        [HttpGet]
        public ActionResult CreateRequest()
        {
            var model = new RequestViewModel();
            model.Request = modelBuilder.ObtainSkillSetVievModel();
            if (model == null)
                return new HttpNotFoundResult();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRequest(RequestViewModel model, int[] skills, int[] grades)
        {
            if (ModelState.IsValid)
            {
                string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
                var skillRequirements = PositiveSkillGradesRetriver.Retrive(skills, grades);
                var requestDTO = ComplexMapper.Map(new RequestModel() { UserName = userName, Name = model.Name, SkillRequirements = skillRequirements.ToList() });
                var operationDetails = await serviceBag.RequestService.Create(requestDTO);
              
                if (operationDetails.Succeeded)
                {
                    return Redirect("/Request/RequestByName/?requestName=" + operationDetails.Property);
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            model.Request = await modelBuilder.ObtainSkillSetVievModel(HttpContext.GetOwinContext().Authentication.User.Identity.Name);
            // to do: provide saving of grades in case of error
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Requests()
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var model = MapperBag.RequestViewMapper.Map(await serviceBag.RequestService.Find(userName));
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> RequestByName(string requestName, int? page)
        {
            if (!(requestName == null))
            {
                string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
                var model = await modelBuilder.ObtainRequestViewModel(userName, requestName, (page ?? 1));
                if (!(model == null))
                    return View(model);
            }
            return new HttpNotFoundResult();           
        }

        [HttpGet]
        public async Task<ActionResult> DeleteRequestByName(string requestName)
        {
            if (!(requestName == null))
            {
                string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
                var model = await modelBuilder.ObtainRequestViewModel(userName, requestName, 1);
                if (!(model == null))
                    return View(model);
            }
            return new HttpNotFoundResult();
        }

        [HttpPost, ActionName("DeleteRequestByName")]
        public async Task<ActionResult> DeleteRequestByNameConfirmed(string requestName)
        {
            if (!(requestName == null))
            {
                string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
                var requestDTO = ComplexMapper.Map(new RequestModel() { UserName = userName, Name = requestName });
                var operationDetails = await serviceBag.RequestService.Delete(requestDTO);
                if (operationDetails.Succeeded)
                    return Redirect("/Request/Requests");
                else
                    ViewBag.Massage = operationDetails.Message;
            }
            return View("Unsuccessful");      
        }

        [HttpGet]
        public ActionResult DeleteAllRequests()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteAllRequests")]
        public async Task<ActionResult> DeleteAllRequestsConfirmed()
        {
            string userName = HttpContext.GetOwinContext().Authentication.User.Identity.Name;           
            var operationDetails = await serviceBag.RequestService.DeleteAll(userName);
            if (operationDetails.Succeeded)
                return Redirect("/Request/Requests");
            ViewBag.Massage = operationDetails.Message;
            return View("Unsuccessful");
        }

        protected override void Dispose(bool disposing)
        {
            serviceBag.Dispose();
            base.Dispose(disposing);
        }
    }
}