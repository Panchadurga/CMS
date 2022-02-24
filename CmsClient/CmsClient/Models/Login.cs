using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class Login
    {
        [Key]
        [Required(ErrorMessage = "Mandatory field")]
        [DisplayName("User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mandatory field")]
        [DisplayName("Password")]
        public string Password { get; set; }

        
        public string ErrorMessage { get; set; }
    }
}
