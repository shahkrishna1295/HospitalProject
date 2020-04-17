using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class BranchModel
    {
        [Key]
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchLocation { get; set; }
        public string BranchContactNumber { get; set; }
        public string BranchEmail { get; set; }
        public string BranchImage { get; set; }

        //representing the many to many of branches and servcies
        //public ICollection<ServiceXBranchModel> ServiceXBranch { get; set; }
    }
}