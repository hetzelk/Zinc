?using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Controllers
{
    public class Responder
    {
        private MessageProcessor processor;

        public Responder(MessageProcessor processor)
        {
            this.processor = processor;
        }

        public void sendMessage()
        {
            //create api call to eztexting to reply to user
            try
            {

            }
            catch (Exception e)
            {
                //log this
            }
        }

        public void FormMessage()
        {

        }
    }

    public class Responses
    {
        public string NewEvent(string date)
        {
            return String.Format("You created a new event on {0}, your default reminders have been set", date);
        }

        public string Mute(string date)
        {
            return "You have muted all of your notifications, reply with UNMUTE to resume your notifications";
        }

        public string UnMute(string date)
        {
            return "You have resumed all of your notifications, reply with MUTE to pause your notifications";
        }

        public string Snooze(string eventName, int defaultSnoozeTime)
        {
            //parse the time to decide whether reminders are minutes or hours
            //strore defaukt snooze time as a minutes int

            string timeDelay = humanizeShortTime(defaultSnoozeTime);
            return String.Format("You have snoozed the {0} notification/s, you will be reminded again in {1}.", eventName, timeDelay);
        }

        private string humanizeShortTime(int defaultSnoozeTime)
        {
            string hoursOrMinutes = "minutes";
            if (defaultSnoozeTime > 59)
            {
                hoursOrMinutes = "hours";
                //fix this, parse time properly
                defaultSnoozeTime = defaultSnoozeTime / 60;

            }
            //ex.
            return "2 hours 30 minutes";
        }
    }
}