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
    public class DoctorController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<Doctor> doctors = db.Doctor.ToList();
            return View(doctors);
        }

        public ActionResult Add()
        {
            List<Speciality> specialities = db.Speciality.ToList();
            
            return View(specialities);
        }

        [HttpPost]

        public ActionResult Add(String DocFirstname, String DocLastname, String DocEmailAddr, string DocLocation, String[] speciality)
        {
            //adding a new doctor 
            Doctor NewDoctor = new Doctor
            {
                DocFname = DocFirstname,
                DocLname = DocLastname,
                DocEmail = DocEmailAddr,
                DocHLoc = DocLocation
            };
            db.Doctor.Add(NewDoctor);
            var count = db.SaveChanges();
            //attaching specialities of the doctor
            for (var i = 0; i < speciality.Length; i++)
            {
                string query = "insert into SpecialityDoctors(Speciality_SpecialityID,Doctor_DocID) values (@SpecialityID,@DocID)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@SpecialityID", speciality[i]);
                sqlparams[1] = new SqlParameter("@DocID", NewDoctor.DocID);
                db.Database.ExecuteSqlCommand(query, sqlparams);
            }
            return Redirect("/Doctor/List");
        }
        public ActionResult ConfirmDelete(int id)
        {
            Doctor doc = db.Doctor.Find(id);
            return View(doc);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string specDocQuery = "delete from specialityDoctors where Doctor_DocID=@id";
            db.Database.ExecuteSqlCommand(specDocQuery, new SqlParameter("@id", id));


            string query = "delete from Doctors where DocID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return Redirect("/Doctor/List");
        }
    }
}