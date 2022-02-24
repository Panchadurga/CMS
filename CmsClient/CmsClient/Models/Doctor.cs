using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class Doctor
    {
        [Key]
        [DisplayName("Doctor Id")]
        public int DoctorId { get; set; }


        [Required(ErrorMessage = "Required")]        
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "No Special Characters")]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Required")]    
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "No Special Characters")]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }


        [Required]
        [DisplayName("Gender")]
        public string Sex { get; set; }

        [Required]
        [DisplayName("Specialization")]
        public string Specialization { get; set; }


        //[DisplayFormat(DataFormatString = "{HH:MM}")]
        //[DataType(DataType.Time)]
        [DisplayName("Visiting Hour(From)")]
        [Required]
        public string StartTime { get; set; }


        [DisplayName("Visiting Hour(To)")]
        //[DisplayFormat(DataFormatString = "{HH:MM}")]
        //[DataType(DataType.Time)]
        [Required]
       
        public string EndTime { get; set; }


    }
}
