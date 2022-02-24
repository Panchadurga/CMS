using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CmsClient.Models
{
    public class ChooseDoctors
    {
        
        [Required]
        [DisplayName("Choose Specialization")]
        public string SelectSpeciality { get; set; }

    }
}
