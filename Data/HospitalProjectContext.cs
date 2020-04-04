using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
//using Emergency.Models;
using System.ComponentModel.DataAnnotations;

namespace HospitalProject.Data
{
    public class HospitalProjectContext : DbContext
    {

        public HospitalProjectContext() : base("name=HospitalProjectContext")
        {
        }

        public System.Data.Entity.DbSet<HospitalProject.Models.EmergencyModels> Emergency { get; set; } 
        public System.Data.Entity.DbSet<HospitalProject.Models.TestimonialModel> Testimonial { get; set; } 
        public System.Data.Entity.DbSet<HospitalProject.Models.BranchModel> Branch { get; set; } 
        public System.Data.Entity.DbSet<HospitalProject.Models.ServiceModel> Service { get; set; } 

    }
}