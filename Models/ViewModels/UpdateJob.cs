using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateJob
    {
        //single applicant
        public ApplicantModel applicant { get; set; }
        //list of jobs
        public List<JobModel> job { get; set; }
    }
}
