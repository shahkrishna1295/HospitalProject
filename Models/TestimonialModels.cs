using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class TestimonialModels
    {
        [Key]
        public int TestimonialID { get; set; }
        public string TestimonialTitle { get; set; }
        public string TestimonialMessage { get; set; }
        public string TestimonialAttachment { get; set; }
        public string NameOfPatient { get; set; }
        public Boolean Status { get; set; }
    }
}