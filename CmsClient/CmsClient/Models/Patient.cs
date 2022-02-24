using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class Patient
    {
        [Key]
        [DisplayName("Patient Id")]
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "No Special Characters")]
        [DisplayName("First Name")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "No Special Characters")]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [DisplayName("Gender")]
        public string Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }

        [Required]
        [Range(1, 120, ErrorMessage = "Age must be between 1 - 120 years")]
        [DisplayName("Age")]
        public int Age { get; set; }

    }
}
