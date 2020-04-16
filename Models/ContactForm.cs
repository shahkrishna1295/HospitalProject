using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class ContactForm
    {
        /* A contact form is a form contain the below information:
                   - A ContactID
                   - A Subject
                   - Contact Firstname
                   - Contact Lastname
                   - An Email
                   - A Phone
                   - A Message 

                  A contact form should reference:
                  - A Status (default: new)
                  - Respond way (default: email)
               */
        [Key]
        public int ContactFormID { get; set; }
        public string Subject { get; set; }
        public string ContactFName { get; set; }
        public string ContactLName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public string ContactMessage { get; set; }

        //Representing the Many in (One Status to Many Contactforms)
        public string ContactStatusID { get; set; }
        [ForeignKey("ContactStatusID")]
        public virtual ContactStatus ContactStatus { get; set; }
        //Representing the Many in (One respond way to Many ContactForms)
        public string RespondWayID { get; set; }
        [ForeignKey("RespondWayID")]
        public virtual RespondWay RespondWay { get; set; }
    }
}