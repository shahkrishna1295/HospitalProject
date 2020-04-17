using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class ShowApplicant
    {
        //represent a signle applicant
        public virtual ApplicantModel applicant { get; set; }

        //represent jobs associated with applicant
        public List<JobModel> jobs { get; set; }

        //represent all jobs
        public List<JobModel> all_jobs { get; set; }

    }
}