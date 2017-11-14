using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Extensions;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class Responder
    {
        public UserDetailsModel userModel;
        public MessageDetailsModel messageModel;

        public Responder(MessageProcessor processor)
        {
            this.userModel = processor.userModel;
            this.messageModel = processor.messageModel;
        }

        public string sendMessage()
        {
            //create api call to eztexting to reply to user
            //for now, just use this as a return statement 
            try
            {
                return FormMessage();
            }
            catch (Exception e)
            {
                Logger logs = new Logger("exceptions", e.ToString());
                logs.Dispose(logs);
                return e.ToString();
            }
        }

        public string FormMessage()
        {
            Responses responses = new Responses();
            if (messageModel.mute)
            {
                return responses.Mute();
            }
            else
            {
                return "default response";
            }
        }
    }

    public class Responses
    {
        public string NewEvent(string date)
        {
            return String.Format("You created a new event on {0}, your default reminders have been set", date);
        }

        public string Mute()
        {
            return "You have muted all of your notifications, reply with UNMUTE to resume your notifications";
        }

        public string UnMute()
        {
            return "You have resumed all of your notifications, reply with MUTE to pause your notifications";
        }

        public string Snooze(string eventName, int defaultSnoozeTime)
        {
            //parse the time to decide whether reminders are minutes or hours
            //strore defaukt snooze time as a minutes int

            string timeDelay = defaultSnoozeTime.HumanizeMinutes();
            return String.Format("You have snoozed the {0} notification/s, you will be reminded again in {1}.", eventName, timeDelay);
        }
    }
}