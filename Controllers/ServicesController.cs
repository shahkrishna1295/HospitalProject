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
using HospitalProject.Models.ViewModels;

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

         //POST: Testimonial/Create
        //adding a new testimonial in the database using parameterized method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string ServiceTitle, string ServiceCategory, string Location)
        {
            
            string query = "insert into ServiceModel (ServiceTitle, ServiceCategory) values (@ServiceTitle,@ServiceCategory)";

            //query before the parameters
            Debug.WriteLine(query);
            Console.WriteLine(query);

            //insert query parameters
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@TestimonialTitle", ServiceTitle);
            sqlparams[1] = new SqlParameter("@NameOfPatient", ServiceCategory);
            

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            //get the data for an inidivial service
            string main_query = "select * from servicemodel where serviceid = @id";

            //sql parameter to pass pk
            var pk_parameter = new SqlParameter("@id", id);
            ServiceModel service = db.Service.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //getting the branches assigned to the particular service
            string aside_query = "select * from branchmodel inner join servicexbranchmodel on branchmodel.branchid = servicexbranchmodel.branchid where servicexbranchmodel.service=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<BranchModel> branchassign = db.Branch.SqlQuery(aside_query, fk_parameter).ToList();

            //getting all the branches to add more
            string all_branch_query = "select * from branchmodel";
            List<BranchModel> AllBranches = db.Branch.SqlQuery(all_branch_query).ToList();

            //passing the values to the viewmodel retrived from the database
            ShowServiceModel viewmodel = new ShowServiceModel();
            viewmodel.Service = service;
            viewmodel.Branches = branchassign;
            viewmodel.all_Branches = AllBranches;

            return View(viewmodel);
        }
        public ActionResult AttachLocation(int id, int branchid)
        {
            Debug.WriteLine("service id is" + id + " and branchid is " + branchid);

            //adding the branch to the particular service if action of adding is applied
            //cheking all the branches associated with the service
            string check_query = "select * from servicemodel inner join servicexbranchmodel on servicexbranchmodel.branchid = branchmodel.branchid where branchmodel.branchid=@branchid and serviceid=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@branchid", branchid);
            List<BranchModel> branch = db.Branch.SqlQuery(check_query, check_params).ToList();

            //checks the existance of the branch
            if (branch.Count <= 0)
            {
                //this will attach the branch to the service in the database
                string query = "insert into servicexbranchmodel (branchid, serviceid) values (@branchid, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@branchid", branchid);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/" + id);

        }

        [HttpGet]
        public ActionResult DetachLocation(int id, int branchid)
        {

            Debug.WriteLine("service id is" + id + " and employee is " + branchid);

            //this query will delete the record of a branch from the particular service associated.
            string query = "delete from servicexbranchmodel where branchid=@branchid and serviceid=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@branchid", branchid);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/" + id);
        }

        // GET: Testimonial/Update/id
        public ActionResult Update(int? id)
        {
            //getting the selected service to update
            ServiceModel services = db.Service.SqlQuery("select * from servicemodel where serviceid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            return View(services);
        }

        // POST: Testimonial/Update/id
        //updating the service using entity framework
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "ServiceTitle,ServiceCategory")] ServiceModel services)
        {
            if (ModelState.IsValid)
            {
                db.Entry(services).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(services);
        }

        // GET: service/Delete/id
        public ActionResult DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //finding service by id
            ServiceModel service = db.Service.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Testimonial/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            //getting the service details by id
            ServiceModel service = db.Service.Find(id);

            //simply removing the service from database
            //here REMOVE(service) works as delete query
            db.Service.Remove(service);
            db.SaveChanges();
            return RedirectToAction("List");
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