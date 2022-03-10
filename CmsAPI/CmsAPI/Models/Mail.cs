using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsAPI.Models
{
    public class Mail
    {
        public string email { get; set; }

        public string subject { get; set; }
        public string content { get; set; }
    }
}
