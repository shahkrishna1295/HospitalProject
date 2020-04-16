using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HospitalProject.Data;
using HospitalProject.Models;
using System.Diagnostics;
using System.IO;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HospitalProject.Models.ViewModels;

namespace HospitalProject.Controllers
{
    public class ApplicantController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Applicant
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ApplicantFirstName,ApplicantLastName,ApplicantAddress,ApplicantEmail,ApplicantPhone,ApplicantEducationSummary,ApplicantWorkExperience,ApplicantSkills")] ApplicantModel applicant)
        {
            if (ModelState.IsValid)
            {
                db.Applicant.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(applicant);
        }

        public ActionResult List(string applicantsearchkey, int pagenum = 0)
        {
            //How could we modify this to include a search bar?
            List<ApplicantModel> applicantmodel = db
                .Applicant
                .Where(g => (applicantsearchkey != null) ? g.ApplicantFirstName.Contains(applicantsearchkey) : true)
                .ToList();

            //start of pagination algorithm (LINQ techniques)
            int perpage = 3;
            int petcount = applicantmodel.Count();
            int maxpage = (int)Math.Ceiling((decimal)petcount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";
            if (maxpage > 0)
            {
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                applicantmodel = db.Applicant
                    .Where(g => (applicantsearchkey != null) ? g.ApplicantFirstName.Contains(applicantsearchkey) : true)
                    .OrderBy(g => g.ApplicantFirstName)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
            }
            //end of pagination algorithm (LINQ techniques)

            return View(applicantmodel);

        }

        public ActionResult Update(int id)
        {
            ApplicantModel OpenedApplicant = db.Applicant.Find(id);

            return View(OpenedApplicant);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicantModel SelectedApplicant = db.Applicant.Find(id);
            string aside_query = "select * from JobModels inner join JobModelApplicantModels on JobModels.JobID = JobModelApplicantModels.JobModel_JobID where JobModelApplicantModels.ApplicantModel_ApplicantID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<JobModel> Jobs = db.Job.SqlQuery(aside_query, fk_parameter).ToList();

            List<JobModel> all_jobs = db.Job
                .ToList();
            if (SelectedApplicant == null)
            {
                return HttpNotFound();
            }
            ShowApplicant viewmodel = new ShowApplicant();
            viewmodel.applicant = SelectedApplicant;
            viewmodel.jobs = Jobs;
            viewmodel.all_jobs = all_jobs;
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult AttachJob(string id, int JobID)
        {
            Debug.WriteLine("applicant id is" + id + " and jobid is " + JobID);

            string check_query = "select * from JobModels inner join JobModelApplicantModels on JobModelApplicantModels.JobModel_JobID = JobModels.JobID where JobModel_JobID=@JobID and ApplicantModel_ApplicantID=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@JobID", JobID);
            List<JobModel> jobs = db.Job.SqlQuery(check_query, check_params).ToList();
            if (jobs.Count <= 0)
            {
                Debug.WriteLine("insidee", jobs.Count);

                string query = "insert into JobModelApplicantModels (JobModel_JobID, ApplicantModel_ApplicantID) values (@JobID, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@JobID", JobID);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Details/" + id);

        }

        [HttpGet]
        public ActionResult DetachJob(string id, int JobID)
        {


            string query = "delete from JobModelApplicantModels where JobModel_JobID=@JobID and ApplicantModel_ApplicantID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@JobID", JobID);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Details/" + id);
        }

        [HttpPost]
        public ActionResult Update(int id, string ApplicantFirstName, string ApplicantLastName, string ApplicantAddress, string ApplicantEmail, int ApplicantPhone, string ApplicantEducationSummary, string ApplicantWorkExperience, string ApplicantSkills)
        {
            ApplicantModel Applicant = db.Applicant.Find(id);
            Applicant.ApplicantFirstName = ApplicantFirstName;
            Applicant.ApplicantLastName = ApplicantLastName;
            Applicant.ApplicantAddress = ApplicantAddress;
            Applicant.ApplicantEmail = ApplicantEmail;
            Applicant.ApplicantPhone = ApplicantPhone;
            Applicant.ApplicantEducationSummary = ApplicantEducationSummary;
            Applicant.ApplicantWorkExperience = ApplicantWorkExperience;
            Applicant.ApplicantSkills = ApplicantSkills;

            db.SaveChanges();

            return RedirectToAction("List");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantModel Applicant = db.Applicant.Find(id);
            if (Applicant == null)
            {
                return HttpNotFound();
            }
            return View(Applicant);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            ApplicantModel Applicant = db.Applicant.Find(id);
            // Equivalent to SQL delete statement:
            db.Applicant.Remove(Applicant);
            db.SaveChanges();

            return RedirectToAction("List");
        }

    }
}