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
    public class FaqController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        public ActionResult List()
        {
            List<Faq> Faqs = db.Faqs.ToList();
            return View(Faqs);
        }
        public ActionResult Add()
        {
            List<FaqCategory> FaqCat = db.FaqCategories.ToList();
            return View(FaqCat);
        }

        [HttpPost]
        public ActionResult Add(string faqQtn, string faqAns, int faqCatDD)
        {
            Faq NewFaq = new Faq()
            {
                FaqQtn = faqQtn,
                FaqAns = faqAns,
                FaqCatID = faqCatDD
            };

            db.Faqs.Add(NewFaq);
            return RedirectToAction("List");
        }
    }
}