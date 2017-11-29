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

        public bool ezzinc { get; set; }

        public bool help { get; set; }
        public bool stop { get; set; }
        public bool snooze { get; set; }
        public bool mute { get; set; }
        public bool unmute { get; set; }

        public string name { get; set; }
        public string number { get; set; }

        public bool events { get; set; }
        public bool reminders { get; set; }
        public bool groups { get; set; }

        public bool create { get; set; }
        public bool change { get; set; }
        public bool delete { get; set; }

        public bool itemEvent { get; set; }
        public string eventName { get; set; }
        public DateTime eventDate { get; set; }
        public string eventGroup { get; set; }

        public bool itemGroup { get; set; }
        public string groupName { get; set; }
        public List<UserDetailsModel> groupMembers { get; set; }

        //for inviting to zinc
        public bool itemInvite { get; set; }
        public List<string> inviteMembers { get; set; }

        //additional options
        public string stampToSend { get; set; }
        public bool sendText { get; set; }

        public bool requiresDate
        {
            get
            {
                return (create || change || delete) && itemEvent;
            }
        }

        public bool newEvent
        {
            get
            {
                return create && itemEvent;
            }
        }

        public bool newGroup
        {
            get
            {
                return create && itemGroup;
            }
        }

        public bool newInvite
        {
            get
            {
                return create && itemInvite;
            }
        }
    }
}