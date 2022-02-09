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
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "At least one uppercase, one lowercase, one digit, one special character and minimum eight charcters in length")]

        public string Currentpassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Currentpassword")]
        public string Confirmpassword { get; set; }
    }
}
