using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CmsClient.Helpers
{
    public class Resetpassword
    {
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "At least one uppercase, one lowercase, one digit, one special character and minimum eight charcters in length")]
        [Display(Name = "New Password")]
        public string Newpassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        //[Compare("Newpassword")]
        [Display(Name = "Confirm password")]
        public string Confirmpassword { get; set; }
    }
}
