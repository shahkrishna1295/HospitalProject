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
    public class FaqCategoryController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();
        // GET: FaqCategory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<FaqCategory> categories = db.FaqCategories.ToList();

            return View(categories);

        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(String newFaqCatName)
        {
            FaqCategory category = new FaqCategory()
            {
                FaqCatName = newFaqCatName
            };

            db.FaqCategories.Add(category);
            db.SaveChanges();
            return Redirect("/Faq/Add");
        }

        public ActionResult Update(int id)
        {
            FaqCategory category = db.FaqCategories.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Update(int id, String UpdatedFaqCatName)
        {
            FaqCategory category = db.FaqCategories.Find(id);
            category.FaqCatName = UpdatedFaqCatName;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(int id)
        {
            FaqCategory category = db.FaqCategories.Find(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            FaqCategory category = db.FaqCategories.Find(id);
            db.FaqCategories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}