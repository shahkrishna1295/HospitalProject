using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateBranchServiceModel
    {
        // this model is to add or delete service from any branch of hospital 
        //getting all the list of branches
        public virtual List<BranchModel> Branches { get; set; }

        //getting all the list of services
        public virtual List<ServiceModel> Services { get; set; }
    }
}