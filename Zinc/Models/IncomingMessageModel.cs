using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Models
{
    public class IncomingMessageModel
    {
        public string initial_number { get; set; }
        public string initial_message { get; set; }
        public string[] initial_message_array { get; set; }

        public bool help { get; set; }
        public bool stop { get; set; }
        public bool snooze { get; set; }
        public bool mute { get; set; }
        public bool unmute { get; set; }

        public bool invite { get; set; }
        public string name { get; set; }
        public string number { get; set; }

        public bool events { get; set; }
        public bool reminders { get; set; }
        public bool groups { get; set; }

        public bool createEvent { get; set; }
        public bool changeEvent { get; set; }
        public bool deleteEvent { get; set; }

        public string eventName { get; set; }
        public string eventDate { get; set; }
        public string eventGroup { get; set; }

        public bool createGroup { get; set; }
        public string groupName { get; set; }
        public List<UserDetailsModel> groupMembers { get; set; }

        //additional options
        public string stampToSend { get; set; }
        public bool sendText { get; set; }
    }
}