using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "Firstname is required")]
        [DisplayName("First Name")]
        public string Firstname { get; set; }


        [Required(ErrorMessage = "Lastname is required")]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }


        [Key]
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]+$", ErrorMessage = "Special Characters not allowed")]
        [DisplayName("User Name")]
        public string Username { get; set; }



        [Required]
        [Key]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[a-z0-9]+@gmail.com", ErrorMessage = "you must provide a gmail account")]
        [DisplayName("Email Address")]
        public string Email { get; set; }



        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "At least one uppercase, one lowercase, one digit, one special character and minimum eight in length")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "At least one uppercase, one lowercase, one digit, one special character and minimum eight in length")]
        [DisplayName("Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        //[RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile number")]
        [StringLength(13, MinimumLength = 10)]
        [DisplayName("Mobile Number")]
        public string MobileNo { get; set; }


        [Required]
        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }

        [Required]
        [DisplayName("Security Answer")]
        public string Answer { get; set; }

        public bool Status { get; set; }
        public string SecurityCode { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
