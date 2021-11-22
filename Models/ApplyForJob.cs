using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace workjop.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string message { get; set; }
        public DateTime applyData { get; set; }

        //relationship

        public int jobId { get; set; }  
        public string userId { get; set; }
        //relationship(job)(usrs)=>m.m(many)to(many);
        public virtual Job Job { get; set; }

        public virtual ApplicationUser User { get; set; }


    }
}