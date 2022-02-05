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
        public string Currentpassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Currentpassword")]
        public string Confirmpassword { get; set; }
    }
}
