using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class DocReview
    {
        [Key]
        public int DocReviewID { get; set; }
        public string DocReviewDesc { get; set; }
        public DateTime DocReviewDate { get; set; }
        public int DocRating { get; set; }

        //representing 
        public int DocID { get; set; }
        [ForeignKey("DocID")]
        public virtual Doctor Doctor { get; set; }

    }
}