using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class FaqCategory
    {
        [Key]
        public int FaqCatID { get; set; }
        public string FaqCatName { get; set; }

        //representing many (one category many faqs)
        public ICollection<Faq> Faqs { get; set; }
    }
}