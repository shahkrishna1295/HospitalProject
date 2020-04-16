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
        // GET: all Testimonial list for staff
        public ActionResult List(string searchkey, int pagenum = 0)
        {
            string query = "Select * from TestimonialModel";
            
            if (searchkey != "")
            {

                query = query + " where testimonialtitle like '%" + searchkey + "%' or testimonialmessage like '%" + searchkey + "%'";
                Console.WriteLine("The query is" + query);
            }

            List<TestimonialModel> testimonials = db.Testimonial.SqlQuery(query).ToList();

            //Pagination code starts here
            int perpage = 5; //setting boundary as 5 testimonial per page
            int testimonialcount = testimonials.Count(); //total testimonal
            int maxpage = (int)Math.Ceiling((decimal)testimonialcount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";

            //starts viewing data
            if (maxpage > 0)
            {
                //fetching the content of the page
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);

                List<SqlParameter> newparams = new List<SqlParameter>();

                //checking if user have search of any particular result 
                if (searchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
                    ViewData["testimonialsearchkey"] = searchkey;
                }

                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));

                //query to get data row by row and stops at boundary here its 5
                string pagedquery = query + " order by testimonialid offset @start rows fetch first @perpage rows only ";


                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);

                //passing query for execution
                testimonials = db.Testimonial.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //pagination ends here

           
            return View(testimonials);
        }

        //GET : all testimonial for visitor view where status = true as true is published testimonial
        public ActionResult VisitorView(string searchkey, int pagenum = 0)
        {
            string query = "Select * from TestimonialModel";

            if (searchkey != "")
            {

                query = query + " where (testimonialtitle like '%" + searchkey + "%' or testimonialmessage like '%" + searchkey + "%') and status = 1";
                Console.WriteLine("The query is" + query);
            }

            List<TestimonialModel> testimonials = db.Testimonial.SqlQuery(query).ToList();

            //Pagination code starts here
            int perpage = 5; //setting boundary as 5 testimonial per page
            int testimonialcount = testimonials.Count(); //total testimonal
            int maxpage = (int)Math.Ceiling((decimal)testimonialcount / perpage) - 1;
            if (maxpage < 0) maxpage = 0;
            if (pagenum < 0) pagenum = 0;
            if (pagenum > maxpage) pagenum = maxpage;
            int start = (int)(perpage * pagenum);
            ViewData["pagenum"] = pagenum;
            ViewData["pagesummary"] = "";

            //starts viewing data
            if (maxpage > 0)
            {
                //fetching the content of the page
                ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);

                List<SqlParameter> newparams = new List<SqlParameter>();

                //checking if user have search of any particular result 
                if (searchkey != "")
                {
                    newparams.Add(new SqlParameter("@searchkey", "%" + searchkey + "%"));
                    ViewData["testimonialsearchkey"] = searchkey;
                }

                newparams.Add(new SqlParameter("@start", start));
                newparams.Add(new SqlParameter("@perpage", perpage));

                //query to get data row by row and stops at boundary here its 5
                string pagedquery = query + " order by testimonialid offset @start rows fetch first @perpage rows only ";


                Debug.WriteLine(pagedquery);
                Debug.WriteLine("offset " + start);
                Debug.WriteLine("fetch first " + perpage);

                //passing query for execution
                testimonials = db.Testimonial.SqlQuery(pagedquery, newparams.ToArray()).ToList();
            }
            //pagination ends here


            return View(testimonials);
        }
        // GET: Testimonial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testimonial/Create
        //adding a new testimonial in the database using parameterized method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string TestimonialTitle, string NameOfPatient, string TestimonialMessage)
        {
            Boolean status = false;
            string query = "insert into TestimonialModel (TestimonialTitle, NameOfPatient, TestimonialMessage, Status) values (@TestimonialTitle,@NameOfPatient,@TestimonialMessage, @Status)";
           
            //query before the parameters
            Debug.WriteLine(query);
            Console.WriteLine(query);

            //insert query parameters
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@TestimonialTitle", TestimonialTitle);
            sqlparams[1] = new SqlParameter("@NameOfPatient", NameOfPatient);
            sqlparams[2] = new SqlParameter("@TestimonialMessage", TestimonialMessage);

            //setting status as 0 = unpulish by defult
            sqlparams[3] = new SqlParameter("@Status", status);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        // GET: Testimonial/Update/id
        public ActionResult Update(int? id)
        {
            //getting the selected testimonial to update
            TestimonialModel testimonial = db.Testimonial.SqlQuery("select * from testimonialmodel where testimonialid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return View(testimonial);
        }

        // POST: Testimonial/Update/id
        //updating the testimonial using entity framework
        //it is not reasonable to do update the testimonial but my concept is if patient has submitted a testimonial with spelling mistake then staff can change it.
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

        // GET: Testimonial/Delete/id
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finding testimonial by id
            TestimonialModel testimonial = db.Testimonial.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonial/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //getting the testimonial details by id
            TestimonialModel testimonial = db.Testimonial.Find(id);

            //simply removing the testimonial from database
            //here REMOVE(testimonial) works as delete query
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