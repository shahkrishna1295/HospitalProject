using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Doctor
    {
        [Key]
        public int DocID { get; set; }
        public string DocFname { get; set; }
        public string DocLname { get; set; }
        public string DocEmail { get; set; }
        public string DocHLoc { get; set; }
        

        //representing many in one doctor many reviews
        public ICollection<DocReview> Reviews { get; set; }
        public virtual ICollection<Speciality> Specialities { get; set; }

    }
}