using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class MessageParser
    {
        string number;
        string message;
        string[] options = new string[]
        {
            "help", "stop", "mute", "unmute", "snooze",
            "events", "reminders", "groups",
            "new", "invite"
        };

        /*
         * Future items to be added
         * name, group
         */
        UserDetailsModel user;
        MessageDetailsModel messageModel;

        public MessageParser(string number, string message)
        {
            this.number = number;
            this.message = message.ToLower();
            user = new UserDetailsModel();
            messageModel = new MessageDetailsModel();

            ParseMessage();
            //get user details after the message is parsed because not all details are required for all message types
            GetUserDetails();
        }

        private void GetUserDetails()
        {
            //search db for current user(phone number lookup)
            //get all their details

            //go through UserDetailsCOntroller
            if (number == "4145200673")
            {
                user.Name = "Keith";
                user.Number = number;
                user.Groups = "";
            }
            else
            {
                user.Name = "Unknown";
                user.Number = "555";
            }
        }

        public string ParseMessage()
        {
            //checkInitializationProgress();
            foreach (string option in options)
            {
                if (message.Contains(option))
                {
                    //find all things that can be set with a bool
                    SetOptions(option);
                }
            }
            return "nothing";
        }

        public void SetOptions(string option)
        {
            switch (option)
            {
                case "help":
                    messageModel.createEvent = true;
                    break;
                case "stop":
                    messageModel.stop = true;
                    break;
                case "mute":
                    messageModel.mute= true;
                    break;
                case "unmute":
                    messageModel.mute = false;
                    break;
                case "snooze":
                    messageModel.snooze = true;
                    break;

                case "events":
                    messageModel.events = true;
                    break;
                case "reminders":
                    messageModel.reminders = true;
                    break;
                case "groups":
                    messageModel.groups = true;
                    break;

                case "new":
                    messageModel.createEvent = true;
                    break;
                case "invite":
                    messageModel.invite = true;
                    break;

                default:
                    break;
            }
        }
    }
}
