using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HospitalProject.Models
{
    public class UserRegistration
    {
        /* User registration contains the below information:
                   - A RegisterID
                   - A username
                   - A password
                   - Firstname
                   - Lastname
                   - Email
                   - Phone
                   - Age
                   - Occupation
                   - Emergency Contact Number

                  User registration should reference:
                  - - Role ID
               */
        [Key]
        public int UserRegistrationID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ContactFName { get; set; }
        public string ContactLName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string EmergencyContact { get; set; }

        //Representing the Many in (One Role to Many UserRegistrations)
        public string RoleID { get; set; }
        [ForeignKey("RoleID")]
        public virtual Role Roles { get; set; }
    }
}