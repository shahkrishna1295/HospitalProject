using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class DonationModel
    {
        [Key]
        public int DonationID { get; set; }

        public string DonatorName { get; set; }
        public string DonatorEmail { get; set; }
        public DateTime DonationDate { get; set; }
        public int DonatorPhone { get; set; }

        //considering the amount in canadian dollars. No decimal allowed
        public int DonationAmount { get; set; }


    }
}
