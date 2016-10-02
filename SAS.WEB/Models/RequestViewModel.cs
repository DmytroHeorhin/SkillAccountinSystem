using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SAS.WEB.Models
{
    public class RequestViewModel
    {
        string defaultName = "new request";

        public int Id { get; set; }

        [Display(Name = "Request name")]
        [Required(ErrorMessage = "Name field can not be empty")]
        public string Name
        {
            get { return defaultName; }
            set { defaultName = value; } 
        }

        [Display(Name = "Time")]
        public DateTime DateTime { get; set; }

        public SkillSetViewModel Request { get; set; }
        public IPagedList<UserViewModel> Users { get; set; }
    }
}