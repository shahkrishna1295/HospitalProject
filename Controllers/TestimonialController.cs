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
    public class TestimonialController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Testimonial
        public ActionResult List(string searchkey)
        {
            string query = "Select * from TestimonialModel";
            
            if (searchkey != "")
            {

                query = query + " where testimonialtitle like '%" + searchkey + "%' or testimonialmessage like '%" + searchkey + "%'";
                Console.WriteLine("The query is" + query);
            }
            
            List<TestimonialModel> testimonials = db.Testimonial.SqlQuery(query).ToList();
            return View(testimonials);
        }
        // GET: Testimonial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testimonial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestimonialTitle,TestimonialMessage,TestimonialAttachment,NameOfPatient")] TestimonialModel testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Testimonial.Add(testimonial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testimonial);
        }

        // GET: Testimonial/Edit/id
        public ActionResult Update(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestimonialModel testimonial = db.Testimonial.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonial/Edit/id
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "TestimonialTitle,TestimonialMessage,TestimonialAttachment,NameOfPatient")] TestimonialModel testimonial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testimonial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testimonial);
        }

        // GET: Emergencies/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestimonialModel testimonial = db.Testimonial.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Emergencies/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestimonialModel testimonial = db.Testimonial.Find(id);
            db.Testimonial.Remove(testimonial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}