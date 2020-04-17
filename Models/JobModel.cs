using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class JobModel
    {
        [Key]
        public int JobID { get; set; }

        public string JobTitle { get; set; }

        public string JobDepartmentName { get; set; }
        public DateTime JobPostedDate { get; set; }

        public string JobDescription { get; set; }

        public string JobRequirements { get; set; }


        //representing many applicants to many jobs
        public ICollection<ApplicantModel> Applicants { get; set; }
    }
}