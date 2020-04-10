using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalProject.Models.ViewModels
{
    public class UpdateContact
    {
        //original contactform
        public virtual ContactForm Contact { get; set; }
        //provide a (radio button) list of status
        public virtual List<ContactStatus> Statuses { get; set; }
        //provide a (radio button) list of respons way
        public virtual List<RespondWay> RespondWays { get; set; }
    }
}