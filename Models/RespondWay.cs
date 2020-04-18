﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalProject.Models
{
    public class RespondWay
    {
        /* RespondWay is describing the way of replying the users who submit contact forms
         * Some things can describe a RespondWay:
         * RespondWay:
         *      - Email
         *      - Phone call
         *      - SMS
         */
        [Key]
        public int RespondWayID { get; set; }
        public string Way { get; set; }
    }
}