using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class Speciality
    {
        [Key]
        public int SpecialityID { get; set; }
        public string SpecialityName { get; set; }
        //Representing many in many doctor many speciality 
        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}