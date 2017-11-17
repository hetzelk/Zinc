using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Zinc.Extensions;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class Responder
    {
        public Models.UserDetailsModel userModel;
        public IncomingMessageModel messageModel;

        public Responder(MessageProcessor processor)
        {
            this.userModel = processor.userModel;
            this.messageModel = processor.messageModel;
        }

        public Responder()
        {
        }

        public string sendMessage(string textMessage)
        {
            try
            {
                NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(String.Empty);
                outgoingQueryString.Add("Message", textMessage);
                outgoingQueryString.Add("PhoneNumbers", "4145200673");
                string postdata = outgoingQueryString.ToString();

                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] postBytes = ascii.GetBytes(outgoingQueryString.ToString());

                var request = HttpWebRequest.Create("https://app.eztexting.com/sending/messages?format=json&User=keithh8112&Password=QkF8r9eCMVrv");
                request.Method = "POST";

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postBytes.Length;

                // add post data to request
                Stream postStream = request.GetRequestStream();
                postStream.Write(postBytes, 0, postBytes.Length);
                postStream.Flush();
                postStream.Close();

                var response = request.GetResponse();
                string message = FormMessage();
                //send the message
                //update the dynamo entry to invalid since the message was sent
                //update the logs as necessary

                return message;
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