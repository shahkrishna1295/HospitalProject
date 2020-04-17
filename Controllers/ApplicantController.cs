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
        //TO DO: add asp.net user to applicant
        //add email notification when they get selected
        //application status for applied jobs
        //suggest job as per the applicant profile by matching it to job description
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
            //if the model state is valid then we can add the applicant
            if (ModelState.IsValid)
            {
                //adding applicant
                db.Applicant.Add(applicant);
                Debug.WriteLine("adding the applicant " + applicant);
                //saving changes
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(applicant);
        }

        public ActionResult List(string applicantsearchkey, int pagenum = 0)
        {
            //Search bar
            Debug.WriteLine("searching for " + applicantsearchkey);
            List<ApplicantModel> applicantmodel = db
                .Applicant
                .Where(g => (applicantsearchkey != null) ? g.ApplicantFirstName.Contains(applicantsearchkey) : true)
                .ToList();

            //pagination : ref: PetGrooming
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
            //returning the view

            return View(applicantmodel);

        }

        public ActionResult Update(int id)
        {
            //updating the applicant
            ApplicantModel OpenedApplicant = db.Applicant.Find(id);
            Debug.WriteLine("finding applicant with id " + id);
            //returning the view
            return View(OpenedApplicant);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //if the id is none, give error
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finding the applicant
            ApplicantModel SelectedApplicant = db.Applicant.Find(id);
            Debug.WriteLine("selected applicant is" + SelectedApplicant);
            //m2m query
            string aside_query = "select * from JobModels inner join JobModelApplicantModels on JobModels.JobID = JobModelApplicantModels.JobModel_JobID where JobModelApplicantModels.ApplicantModel_ApplicantID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            //getting the jobs
            List<JobModel> Jobs = db.Job.SqlQuery(aside_query, fk_parameter).ToList();

            //getting all the jobs
            List<JobModel> all_jobs = db.Job
                .ToList();
            if (SelectedApplicant == null)
            {
                return HttpNotFound();
            }
            ShowApplicant viewmodel = new ShowApplicant();
            //assigning the values
            viewmodel.applicant = SelectedApplicant;
            viewmodel.jobs = Jobs;
            viewmodel.all_jobs = all_jobs;
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult AttachJob(string id, int JobID)
        {
            Debug.WriteLine("applicant id is" + id + " and jobid is " + JobID);

            //attaching the job to applicant using bridging table
            string check_query = "select * from JobModels inner join JobModelApplicantModels on JobModelApplicantModels.JobModel_JobID = JobModels.JobID where JobModel_JobID=@JobID and ApplicantModel_ApplicantID=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@JobID", JobID);
            //checking if the job is already attached or not
            List<JobModel> jobs = db.Job.SqlQuery(check_query, check_params).ToList();
            //if not we can attach
            if (jobs.Count <= 0)
            {
                Debug.WriteLine("insidee", jobs.Count);

                string query = "insert into JobModelApplicantModels (JobModel_JobID, ApplicantModel_ApplicantID) values (@JobID, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@JobID", JobID);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            //redirecting to details
            return RedirectToAction("Details/" + id);

        }

        [HttpGet]
        public ActionResult DetachJob(string id, int JobID)
        {
            Debug.WriteLine("detaching the job with id" + JobID);
            //detaching the job
            string query = "delete from JobModelApplicantModels where JobModel_JobID=@JobID and ApplicantModel_ApplicantID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@JobID", JobID);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            //returning to details
            return RedirectToAction("Details/" + id);
        }

        [HttpPost]
        public ActionResult Update(int id, string ApplicantFirstName, string ApplicantLastName, string ApplicantAddress, string ApplicantEmail, int ApplicantPhone, string ApplicantEducationSummary, string ApplicantWorkExperience, string ApplicantSkills)
        {
            Debug.WriteLine("updating applicant with id" + id);
            //updating the applicant
            ApplicantModel Applicant = db.Applicant.Find(id);
            Applicant.ApplicantFirstName = ApplicantFirstName;
            Applicant.ApplicantLastName = ApplicantLastName;
            Applicant.ApplicantAddress = ApplicantAddress;
            Applicant.ApplicantEmail = ApplicantEmail;
            Applicant.ApplicantPhone = ApplicantPhone;
            Applicant.ApplicantEducationSummary = ApplicantEducationSummary;
            Applicant.ApplicantWorkExperience = ApplicantWorkExperience;
            Applicant.ApplicantSkills = ApplicantSkills;
            //saving to database
            db.SaveChanges();
            //returning to list
            return RedirectToAction("List");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finding the applicant
            ApplicantModel Applicant = db.Applicant.Find(id);
            Debug.WriteLine("applicant" + Applicant);
            if (Applicant == null)
            {
                return HttpNotFound();
            }
            //returning the view
            return View(Applicant);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //deleting the applicant
            ApplicantModel Applicant = db.Applicant.Find(id);
            // query
            db.Applicant.Remove(Applicant);
            Debug.WriteLine("removing applicant" + Applicant);
            //executing the query
            db.SaveChanges();
            //redirecting to list
            return RedirectToAction("List");
        }

    }
}