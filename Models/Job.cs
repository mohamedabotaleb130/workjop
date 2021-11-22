using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace workjop.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Display(Name = "اسم  الوظيفة")]
        public string JopTitle { get; set; }
        [Display(Name = "وصف  الوظيفة")]
        [AllowHtml]
        public string JobContent { get; set; }
        [Display(Name = "صورة  الوظيفة")]

        public string JobImage { get; set; }
        [Display(Name = "نوع  الوظيفة")]


        public int CategoryID { get; set; }


        public string  UserID { get; set; }
        public virtual ApplicationUser User { get; set; }   //virtual :lazy loding 

        public virtual Category Category { get; set; }   //virtual :lazy loding 

    }
}