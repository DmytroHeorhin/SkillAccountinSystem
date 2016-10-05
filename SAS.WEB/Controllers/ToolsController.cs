using PagedList;
using SAS.BLL.Interfaces;
using SAS.BLL.Services;
using SAS.WEB.Mapping;
using SAS.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Controllers
{
    [Authorize(Roles = "manager, administrator")]
    public class ToolsController : Controller
    {
        IServiceBag serviceBag;

        public ToolsController(IServiceBag ServiceBag)
        {
            serviceBag = ServiceBag;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var cat = MapperBag.CategoryMapper.Map(model);
                var operationDetails = serviceBag.CategoryService.Create(cat);
                if (operationDetails.Succeeded)
                {
                    ViewBag.Item = "Category";
                    ViewBag.State = "created";
                    return View("Successful");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public ActionResult CreateSkill()
        {
            ViewBag.Categories = MapperBag.CategoryMapper.Map(serviceBag.CategoryService.GetAll());
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult CreateSkill(SkillModel model)
        {
            if (ModelState.IsValid)
            {
                var skill = MapperBag.SkillMapper.Map(model);
                var operationDetails = serviceBag.SkillService.Create(skill);
                if (operationDetails.Succeeded)
                {
                    ViewBag.Item = "Skill";
                    ViewBag.State = "created";
                    return View("Successful");
                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
                }
            }
            ViewBag.Categories = MapperBag.CategoryMapper.Map(serviceBag.CategoryService.GetAll());
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            serviceBag.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult AllUsers(int? page)
        {
            var users = MapperBag.UserMapper.Map(serviceBag.UserService.GetAll());
            var model = users.ToPagedList(page ?? 1, 10);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult EditUser(string userName)
        {
            var model = MapperBag.UserMapper.Map(serviceBag.UserService.Find(userName));

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public ActionResult EditUser(UserModel user)
        {
            if (user == null)
                return new HttpNotFoundResult();
            if (ModelState.IsValid)
            {
                var operationDetails = serviceBag.UserService.UpdateRoles(MapperBag.UserMapper.Map(user));
                if(operationDetails.Succeeded)
                {
                    ViewBag.Item = "User";
                    ViewBag.State = "updated";
                    return View("Successful");
                }
                ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }                                  
            return View(user);
        }
    }
}