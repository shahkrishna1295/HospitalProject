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

namespace HospitalProject.Controllers
{
    public class JobController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Job
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "JobTitle,JobDepartmentName,JobPostedDate,JobDescription,JobRequirements")] JobModel job)
        {
            if (ModelState.IsValid)
            {
                db.Job.Add(job);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(job);
        }

        public ActionResult List(string jobsearchkey,int pagenum = 0)
        {
            //How could we modify this to include a search bar?
            List<JobModel> jobmodel = db
                .Job
                .Where(g => (jobsearchkey != null) ? g.JobTitle.Contains(jobsearchkey) : true)
                .ToList();

            //start of pagination algorithm (LINQ techniques)
            int perpage = 3;
            int petcount = jobmodel.Count();
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
                jobmodel = db.Job
                    .Where(g => (jobsearchkey != null) ? g.JobTitle.Contains(jobsearchkey) : true)
                    .OrderBy(g => g.JobPostedDate)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
            }
            //end of pagination algorithm (LINQ techniques)

            return View(jobmodel);

        }

        public ActionResult Update(int id)
        {
            JobModel OpenedJob = db.Job.Find(id);

            return View(OpenedJob);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobModel Job = db.Job.Find(id);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        [HttpPost]
        public ActionResult Update(int id, string JobTitle, string JobDescription, DateTime JobPostedDate , string JobDepartmentName, string JobRequirements)
        {
            JobModel Job = db.Job.Find(id);
            Job.JobTitle = JobTitle;
            Job.JobDescription = JobDescription;
            Job.JobPostedDate = JobPostedDate;
            Job.JobDepartmentName = JobDepartmentName;
            Job.JobRequirements = JobRequirements;

            db.SaveChanges();

            return RedirectToAction("List");

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobModel Job = db.Job.Find(id);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            JobModel Job = db.Job.Find(id);
            // Equivalent to SQL delete statement:
            db.Job.Remove(Job);
            db.SaveChanges();

            return RedirectToAction("List");
        }



    }
}