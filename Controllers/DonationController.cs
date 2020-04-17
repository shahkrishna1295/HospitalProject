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
        // TO DO: Add payment gateway
        // Send receipt when payment is done
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
                //creating a new donation
                Debug.WriteLine("adding the donation" + donation);
                db.Donation.Add(donation);
                db.SaveChanges();
                return RedirectToAction("List");
            }

            return View(donation);
        }

        public ActionResult List(string namesearchkey, int pagenum = 0)
        {
            //search bar
            Debug.WriteLine("searching for" + namesearchkey);
            List<DonationModel> donationmodel = db
                .Donation
                .Where(g => (namesearchkey != null) ? g.DonatorName.Contains(namesearchkey) : true)
                .ToList();

            //pagination ref: PetGrooming
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
            //returning the donation model

            return View(donationmodel);

        }

        public ActionResult Update(int id)
        {
            //finding the opened donation
            DonationModel OpenedDonation = db.Donation.Find(id);
            Debug.WriteLine("updating the donation with id" + id);
            //returnig the view
            return View(OpenedDonation);
        }

        public ActionResult Details(int? id)
        {
            //error handelling
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finding the donation
            DonationModel donation = db.Donation.Find(id);
            Debug.WriteLine("donation is" + donation);
            if (donation == null)
            {
                return HttpNotFound();
            }
            //returning the view of donation
            return View(donation);
        }


        [HttpPost]
        //update of donation
        public ActionResult Update([Bind(Include = "DonatorName,DonatorEmail,DonationDate,DonatorPhone,DonationAmount")] DonationModel donation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donation).State = EntityState.Modified;
                //saving the changes
                db.SaveChanges();
                return RedirectToAction("List");
            }
            //returning the view
            Debug.WriteLine("donation is" + donation);
            return View(donation);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //finding the donation
            Debug.WriteLine("deleting job with id" + id);
            DonationModel donation = db.Donation.Find(id);
            if (donation == null)
            {
                return HttpNotFound();
            }
            //returning the view
            return View(donation);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //finding the donation
            DonationModel Donation = db.Donation.Find(id);
            Debug.WriteLine("deleting the job with id" + id);
            // removing the donation
            db.Donation.Remove(Donation);
            db.SaveChanges();
            //returning the list view
            return RedirectToAction("List");
        }



    }
}