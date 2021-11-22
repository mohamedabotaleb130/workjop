using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace workjop.Models
{
    public class Category
    {

        public int ID { get; set; }
        [Required]
        [Display(Name="نوع الوظيفة")]
        public string  CategoryName { get; set; }

        [Required]
        [Display(Name = "وصف النوع ")]
        public string CategoryDescription { get; set; }                //Category(one)job(many)(1-m)

        public virtual ICollection<Job> Jobs { get; set; }//Category(one)job(many)
    }
}
