using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowApplicant
    {

        public virtual ApplicantModel applicant { get; set; }

        public List<JobModel> jobs { get; set; }

        public List<JobModel> all_jobs { get; set; }

    }
}