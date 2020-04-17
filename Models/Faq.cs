using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class Faq
    {
        [Key]
        public int FaqID { get; set; }
        public string FaqQtn { get; set; }
        public string FaqAns { get; set; }
        public int FaqCatID { get; set; }

        //representing one (one faq many category)
        [ForeignKey("FaqCatID")]
        public virtual FaqCategory FaqCategory { get; set; }

    }
}