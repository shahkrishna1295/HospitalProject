using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class ContactStatus
    {
        /* ContactStatus is describing the status of the contact form information which users submitted
         * Some things can describe a ContactStatus:
         * StatusName:
         *      - Pending
         *      - Solved
         *      - New
         */
        [Key]
        public int ContactStatusID { get; set; }
        public string StatusName { get; set; }
    }
}