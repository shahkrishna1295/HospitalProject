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
//using HospitalProject.Models.ViewModels;
using System.Diagnostics;
using System.Globalization; //for cultureinfo.invariantculture
//needed for await
using System.Threading.Tasks;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace HospitalProject.Controllers
{
    public class SpecialityController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Speciality
        public ActionResult List()
        {
            List<Speciality> specialities = db.Speciality.ToList();

            return View(specialities);
            
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(String newSpeciality)
        {
            Speciality NewSpeciality = new Speciality
            {
                SpecialityName = newSpeciality
            };

            db.Speciality.Add(NewSpeciality);
            db.SaveChanges();
            return Redirect("/Doctor/Add");
        }

        public ActionResult Update(int id)
        {
            Speciality s = db.Speciality.Find(id);
            return View(s);
        }
        [HttpPost]
        public ActionResult Update(int id, String UpdatedSpeciality)
        {
            Speciality s = db.Speciality.Find(id);
            s.SpecialityName = UpdatedSpeciality;
            db.SaveChanges();
            return Redirect("/Speciality/List");
        }

        public ActionResult ConfirmDelete(int id)
        {
            Speciality s = db.Speciality.Find(id);
            return View(s);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Speciality s = db.Speciality.Find(id);
            db.Speciality.Remove(s);
            db.SaveChanges();
            return Redirect("/Speciality/List");
        }

        
    }
}