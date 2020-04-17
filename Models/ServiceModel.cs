using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class ServiceModel
    {
        [Key]
        public int ServiceID { get; set; }

        public string ServiceTitle { get; set; }

        public string ServiceCategory { get; set; }

        //representing the many to many of branches and servcies
        //public ICollection<ServiceXBranchModel> ServiceXBranch { get; set; }
    }
}