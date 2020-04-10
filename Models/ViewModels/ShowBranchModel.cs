using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalProject.Models.ViewModels
{
    public class ShowBranchModel
    {
        //one branch
        public virtual BranchModel Branch { get; set; }

        //list of every service under the particular location of the hospital
        public virtual List<ServiceModel> Services { get; set; }

        //list of all the services offered by the hospitals different locations.
        public virtual List<ServiceModel> all_Services { get; set; }
    }
}