using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Models
{
    public class UserDetailsModel
    {
        public string Uuid { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public string Enabled { get; set; }

        public string Groups { get; set; }
    }
}