using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowServiceModel
    {
        //one service
        public virtual ServiceModel Service { get; set; }

        //list of every branch the particular service is offered by the hospital
        public virtual List<BranchModel> Branches { get; set; }

        //list of all the brances
        public virtual List<BranchModel> all_Branches { get; set; }
    }
}