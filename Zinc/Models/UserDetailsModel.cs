using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Models
{
    public class UserDetailsModel
    {
        public string user_uuid { get; set; }

        public bool enabled { get; set; }

        public string phone_number { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public bool mute { get; set; }

        public string birthday { get; set; }

        //string array in minute increments, 60, 120, 1440 defaults
        public string default_reminder_times { get; set; }

        public string groups { get; set; }
    }
}