using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models
{
    public class RespondWay
    {
        /* RespondWay is describing the way of replying the users who submit contact forms
         * Some things can describe a RespondWay:
         * RespondWay:
         *      - Email
         *      - Call
         *      - SMS
         */
        public int RespondWayID { get; set; }
    }
}