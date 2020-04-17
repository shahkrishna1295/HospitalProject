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
    public class BranchController : Controller
    {
        private HospitalProjectContext db = new HospitalProjectContext();

        // GET: Branches
        public ActionResult List(string branchsearchkey)
        {
            //access the search key

            //Debug.WriteLine("The search key is " + branchsearchkey);

            //getting all the branches
            string query = "Select * from branchmodel";

            if (branchsearchkey != "")
            {
                //appending the query to include the search key
                query = query + " where servicetitle like '%" + branchsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }

            List<BranchModel> branch = db.Branch.SqlQuery(query).ToList();
            return View(branch);

        }

        public ActionResult Show(int id)
        {
            //get the data for an inidivial service
            string main_query = "select * from branchmodel where branchid = @id";

            //sql parameter to pass pk
            var pk_parameter = new SqlParameter("@id", id);
            BranchModel branch = db.Branch.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //getting the services assigned to the particular branch
            string aside_query = "select * from servicemodel inner join servicexbranchmodel on branchmodel.serviceid = servicexbranchmodel.serviceid where servicexbranchmodel.branch=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<ServiceModel> serviceoffered = db.Service.SqlQuery(aside_query, fk_parameter).ToList();

            
            //passing the values to the viewmodel retrived from the database
            ShowBranchModel viewmodel = new ShowBranchModel();
            viewmodel.Branch = branch;
            viewmodel.Services = serviceoffered;

            return View(viewmodel);
        }

    }
}