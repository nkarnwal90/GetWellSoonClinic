using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GET_WELL_SOON_CLINIC.Models
{
    public enum PatientGender
    {
        Male,
        Female
    }
    public class Gender
    {
        public PatientGender gender { get; set; }
    }
}