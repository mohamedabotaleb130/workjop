using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using workjop.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var list = db.Categories.ToList();

            return View(list);
        }


        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Apply(string Messags)

        {
            var UserId = User.Identity.GetUserId();

            var JobId = (int)Session["jobId"];
          
        
           var Check = db.ApplyForJobs.Where(a => a.jobId == JobId && a.userId == UserId).ToList();

            if (Check.Count < 1)
            {

                var job = new ApplyForJob();
                job.userId = UserId;
                job.jobId = JobId;
                job.message = Messags;
                job.applyData = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت الاضافة بنجاح !";

            }
            else
            {
                ViewBag.Result = "المعذرة ,لقد سبق وتقدمت الي نفس الوظيفة";


            }
               return View();



}
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }


            return View(job);
        }


        // POST: ApplyForJobs/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {

            if (ModelState.IsValid)
            {

                job.applyData = DateTime.Now;

                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobByUser");

            }
            return View(job);
        }

        [Authorize]
        public ActionResult GetJobByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.userId == UserId);
            return View(Jobs.ToList());
        }


        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);

        }




        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }


            return View(job);

        }

        // POST: ApplyForJobs/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            var myjob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(myjob);
            db.SaveChanges();

            return RedirectToAction("GetJobByUser");



        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs =
           from app in db.ApplyForJobs
           join job in db.Jobs
           on app.jobId equals job.Id
           where job.User.Id == UserID
           select app;


            var grouped = from j in Jobs
                          group j by j.Job.JopTitle
                        into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr

                          };

            return View(grouped.ToList());
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
       
           

            public ActionResult Contact(ContactModel contact)
            {
                var mail = new MailMessage();
                var loginInfo = new NetworkCredential("emiel", "password");

            mail.From = new MailAddress(contact.Email);
                mail.To.Add(new MailAddress("mohamedabotaleb130@gmail.com"));
                mail.Subject = contact.Subject;
                mail.IsBodyHtml = true;
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                               "بريد المرسل: " + contact.Email + "<br>" +
                               "عنوان الرسالة: " + contact.Subject + "<br>" +
                               "نص الرسالة: <b>" + contact.Message + "</b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
             smtpClient.Credentials = loginInfo;
                smtpClient.Send(mail);
                return RedirectToAction("Index");
            }



            //    var mail = new MailMessage();
            //    var loginInfo = new NetworkCredential("emial.com", "passs");
            //    mail.From = new MailAddress(contact.Email);
            //    mail.To.Add(new MailAddress("mohamedabotaleb130@gmail.com"));
            //    mail.Subject = contact.Subject;
            //    mail.IsBodyHtml = true;
            //    string body = "اسم المرسل: " + contact.Name + "<br>" +
            //                    "بريد المرسل: " + contact.Email + "<br>" +
            //                    "عنوان الرسالة: " + contact.Subject + "<br>" +
            //                    "نص الرسالة: <b>" + contact.Message + "</b>";
            //    mail.Body = body;
            //var smtpClient = new SmtpClient("smtp.gmail.com", 587);


            //    smtpClient.EnableSsl = true;
            //smtpClient.UseDefaultCredentials = true;

            //    smtpClient.Credentials = loginInfo;
            //    smtpClient.Send(mail);
            //    return RedirectToAction("Index");
        
        

        public ActionResult Search()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Search( string searchName)
        {

            var result = db.Jobs.Where(a => a.JopTitle.Contains(searchName) ||
              a.JobContent.Contains(searchName) ||
              a.Category.CategoryName.Contains(searchName) ||
              a.Category.CategoryDescription.Contains(searchName)).ToList();


            return View(result);

        }
    }
}