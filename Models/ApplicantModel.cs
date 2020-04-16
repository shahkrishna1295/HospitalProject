using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class ApplicantModel
    {
        [Key]
        public int ApplicantID { get; set; }

        public string ApplicantFirstName { get; set; }
        public string ApplicantLastName { get; set; }

        public string ApplicantAddress { get; set; }

        public string ApplicantEmail { get; set; }

        public int ApplicantPhone { get; set; }

        public string ApplicantEducationSummary { get; set; }

        public string ApplicantWorkExperience { get; set; }

        public string ApplicantSkills { get; set; }
    }
}
