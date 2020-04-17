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
    public class ServicesController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Services
        public ActionResult List(string servicesearchkey)
        {
            //access the search key

            //Debug.WriteLine("The search key is " + servicesearchkey);

            //getting all the services
            string query = "Select * from servicemodel";

            if (servicesearchkey != "")
            {
                //appending the query to include the search key
                query = query + " where servicetitle like '%" + servicesearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }

            List<ServiceModel> service = db.Service.SqlQuery(query).ToList();
            return View(service);

        }

        // GET: Service/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Testimonial/Create
        //adding a new testimonial in the database using parameterized method
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(string ServiceTitle, string ServiceCategory)
        //{
        //    Boolean status = false;
        //    string query = "insert into TestimonialModel (TestimonialTitle, NameOfPatient, TestimonialMessage, Status) values (@TestimonialTitle,@NameOfPatient,@TestimonialMessage, @Status)";

        //    //query before the parameters
        //    Debug.WriteLine(query);
        //    Console.WriteLine(query);

        //    //insert query parameters
        //    SqlParameter[] sqlparams = new SqlParameter[4];
        //    sqlparams[0] = new SqlParameter("@TestimonialTitle", TestimonialTitle);
        //    sqlparams[1] = new SqlParameter("@NameOfPatient", NameOfPatient);
        //    sqlparams[2] = new SqlParameter("@TestimonialMessage", TestimonialMessage);

        //    //setting status as 0 = unpulish by defult
        //    sqlparams[3] = new SqlParameter("@Status", status);

        //    db.Database.ExecuteSqlCommand(query, sqlparams);

        //    return RedirectToAction("List");
        //}

    }
}