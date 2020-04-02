using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Emergency
    {
        [Key]
        public int EmergencyId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Article { get; set; }

        public string KeywordOne { get; set; }
        public string KeywordTwo { get; set; }
        public string KeywordThree { get; set; }

    }
}