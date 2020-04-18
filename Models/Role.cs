using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class Role
    {
        /* Role is describing the identity of a registered user (patient or volunteer)
         * Some things can describe a RespondWay:
         * Role:
         *      - Patient
         *      - Volunteer
         */

        [Key]
        public int RoleID { get; set; }
        public string RoleName { get; set; }
    }
}

