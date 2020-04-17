using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//add
using System.Data;
using System.Net;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using HospitalProject.Data;
using HospitalProject.Models;
using HospitalProject.Models.ViewModels;
using System.Diagnostics;


namespace HospitalProject.Controllers
{
    public class ContactStatusController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: ContactStatus/List
        public ActionResult List(string statussearchkey)
        {
            Debug.WriteLine("The parameter is " + statussearchkey);
            //SQL query 
            string query = "SELECT * FROM status";
            if (statussearchkey != "")
            {
                query = query + " WHERE Name LIKE '%" + statussearchkey + "%'";
            }
            //needed data
            List<ContactStatus> contactstatuses = db.ContactStatuses.SqlQuery(query).ToList();
            return View(contactstatuses);
        }

        //add new status name
        public ActionResult Add()
        {
            //don't need previous info for adding a status
            return View();
        }
        [HttpPost]
        public ActionResult Add(string StatusName)
        {
            //SQL query
            string query = "INSERT INTO status (Name) VALUES (@StatusName)";
            var parameter = new SqlParameter("@StatusName", StatusName);
            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }

        //show 

    }
}