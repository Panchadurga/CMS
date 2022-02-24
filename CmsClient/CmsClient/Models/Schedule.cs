using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class Schedule
    {
        [Key]
        [DisplayName("Appointment Id")]
        public int AppointmentId { get; set; }

        [Required]
        [DisplayName("Patient Id")]
        public int PatientId { get; set; }

        [Required]
        [DisplayName("Specialization")]
        public string Specialization { get; set; }

        [Required]
        [DisplayName("Doctor Name")]
        public string DoctorName{ get; set; }

        [Required]
        [DisplayName("Visiting Date")]
        [DataType(DataType.Date)]
        public DateTime VisitDate { get; set; }


        [Required]
        [DisplayName("Appointment Time")]
        public String AppointmentTime { get; set; }


    }
   
}
