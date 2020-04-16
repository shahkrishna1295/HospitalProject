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
    public class DonationController : Controller
    {
        // GET: Donation
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
        public ActionResult Create([Bind(Include = "DonatorName,DonatorEmail,DonationDate,DonatorPhone,DonationAmount")] DonationModel donation)
        {
            if (ModelState.IsValid)
            {
                db.Donation.Add(donation);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(donation);
        }

        public ActionResult List(string namesearchkey, int pagenum = 0)
        {
            //How could we modify this to include a search bar?
            List<DonationModel> donationmodel = db
                .Donation
                .Where(g => (namesearchkey != null) ? g.DonatorName.Contains(namesearchkey) : true)
                .ToList();

            //start of pagination algorithm (LINQ techniques)
            int perpage = 3;
            int petcount = donationmodel.Count();
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
                donationmodel = db.Donation 
                    .Where(g => (namesearchkey != null) ? g.DonatorName.Contains(namesearchkey) : true)
                    .OrderBy(g => g.DonationDate)
                    .Skip(start)
                    .Take(perpage)
                    .ToList();
            }
            //end of pagination algorithm (LINQ techniques)

            return View(donationmodel);

        }

        public ActionResult Update(int id)
        {
            DonationModel OpenedDonation = db.Donation.Find(id);

            return View(OpenedDonation);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationModel donation = db.Donation.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "DonatorName,DonatorEmail,DonationDate,DonatorPhone,DonationAmount")] DonationModel donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donation);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationModel donation = db.Donation.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            return View(donation);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DonationModel Donation = db.Donation.Find(id);
            // Equivalent to SQL delete statement:
            db.Donation.Remove(Donation);
            db.SaveChanges();

            return RedirectToAction("List");
        }



    }
}