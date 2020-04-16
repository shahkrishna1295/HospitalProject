using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateJob
    {
        public ApplicantModel applicant { get; set; }
        public List<JobModel> job { get; set; }
    }
}
