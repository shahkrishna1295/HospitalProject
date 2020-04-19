using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//add
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using HospitalProject.Data;
using HospitalProject.Models;
using System.Diagnostics;
//needed for other sign in feature classes
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;
using HospitalProject.Models.ViewModels;

namespace HospitalProject.Controllers
{
    public class ContactFormController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        private HospitalProjectContext db = new HospitalProjectContext();
        public ContactFormController() { }


        // GET: ContactForm
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //contact searchkey
        public ActionResult List(string contactsearchkey, int pagenum = 0)
        {
            //empty searchkey declaration
            List<ContactForm> Contacts;
            if (contactsearchkey != "" && contactsearchkey != null)
            {
                //Linq
                Contacts = db.ContactForms.Where(contact =>
                contact.Subject.Contains(contactsearchkey) ||
                contact.ContactFName.Contains(contactsearchkey) ||
                contact.ContactLName.Contains(contactsearchkey) ||
                contact.ContactEmail.Contains(contactsearchkey) ||
                contact.ContactPhone.Contains(contactsearchkey) ||
                contact.ContactMessage.Contains(contactsearchkey)
                //contact.Status.Contains(contactsearchkey)
                ).ToList();

                //try pagination lab
                //display 5 record each page
                int perpage = 5;
                //total contact form records
                int contactcount = Contacts.Count();
                //caculate how many pages in total
                int maxpage = (int)Math.Ceiling((decimal)contactcount / perpage) - 1;
                if(maxpage < 0) maxpage = 0;
                if(pagenum < 0) pagenum = 0;
                if (pagenum > maxpage) pagenum = maxpage;
                int start = (int)(perpage * pagenum);
                ViewData["pagenum"] = pagenum;
                ViewData["pagesummary"] = "";
                if (maxpage > 0)
                {
                    //"+1" to fill pages in the array start from 0
                    ViewData["pagesummary"] = (pagenum + 1) + " of " + (maxpage + 1);
                    Contacts = db.ContactForm
                        .where(c => contactsearchkey != null) ? (contact.Subject.Contains(contactsearchkey) : true ||
                contact.ContactFName.Contains(contactsearchkey) : true ||
                contact.ContactLName.Contains(contactsearchkey) : true ||
                contact.ContactEmail.Contains(contactsearchkey) : true ||
                contact.ContactPhone.Contains(contactsearchkey) : true ||
                contact.ContactMessage.Contains(contactsearchkey) : true )
                    .OrderBy(c => c.ContactFormID) 
                    .Skip(start) //skip the previous records not displayed 
                    .Take(perpage) //take 5 record perpage
                    .ToList();
                }
                //end of failed pagination lab attempt

            }
            else
            {
                Contacts = db.ContactForms.ToList();
            }
            return View(Contacts);
        }

        public ActionResult Add()
        {
            AddContact viewmodel = new AddContact();
            viewmodel.Statuses = db.ContactStatuses.ToList();
            viewmodel.RespondWays = db.RespondWays.ToList();

            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Add(string Subject, string ContactFName, string ContactLName, string ContactEmail, string ContactPhone, string ContactMessage, string ContactStatusID, string RespondWayID)
        {
            ContactForm newcontact = new ContactForm();
            Debug.WriteLine("Contact Subject: " + Subject);
            newcontact.ContactStatusID = ContactStatusID;
            newcontact.RespondWayID = RespondWayID;

            //first add contact form
            db.ContactForms.Add(newcontact);
            db.SaveChanges();

            //make sure it's not a null list
            if (Statuses != null)
            {
                if (Statuses.Length > 0)
                {
                    newcontact.ContactStatus = new List<ContactStatus>();
                    //add status to that contact form
                    foreach (int ContactStatusID in Statuses.ToList())
                    {
                        ContactStatus status = db.ContactStatuses.Find(ContactStatusID);
                        newcontact.ContactStatus.Add(status);
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        //update
        public ActionResult Update(int id)
        {
            //get data
            UpdateContact viewmodel = new UpdateContact();
            viewmodel.Contact = db.ContactForms
                .Include(contact => contact.ContactStatus)
                .FirstOrDefault(contact => contact.ContactFormID == id);

            //get all statuses
            viewmodel.Statuses = db.ContactStatuses.ToList();
            //get all respond ways
            viewmodel.RespondWays = db.RespondWays.ToList();

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Update(int id, string Subject, string ContactFName, string ContactLName, string ContactEmail, string ContactPhone, string ContactMessage, string ContactStatusID, string RespondWayID)
        {
            ContactForm contact = db.ContactForms
                .Include(c => c.ContactStatus)
                .FirstOrDefault(c => c.ContactFormID == id);

            //remove existing status to this contactform
            foreach (var status in contact.ContactStatus.ToList())
            {
                contact.ContactStatus.Delete(status);
            }
            db.SaveChanges();

            Debug.WriteLine("Contact Subject: " + Subject);
            contact.ContactStatusID = ContactStatusID;
            contact.RespondWayID = RespondWayID;

            //update the contact
            db.SaveChanges();

            //make sure it's not a null list
            if (Statuses != null)
            {
                if (Statuses.Length > 0)
                {
                    contact.ContactStatus = new List<ContactStatus>();
                    //add status to that contact form
                    foreach (int ContactStatusID in Statuses.ToList())
                    {
                        ContactStatus status = db.ContactStatuses.Find(ContactStatusID);
                        newcontact.ContactStatus.Add(status);
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        //show
        public ActionResult Show(int id)
        {
            ContactForm contact = db.ContactForms
                .Include(c => c.ContactStatus)
                .FirstOrDefault(c => c.ContactFormID == id);
            //Debug.WriteLine
            return View(contact);
        }


        public ActionResult Delete(int id)
        {
            //SQL query
            string query = "SELECT * FROM ContactForm WHERE ContactFormID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            ContactForm contactForm = db.ContactForms.SqlQuery(query, param).FirstOrDefault();
            return View(contactForm);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            //SQL query
            string query = "DELETE FROM ContactForm WHERE ContactFormID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }
    }
}