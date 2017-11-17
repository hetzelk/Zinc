using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
        public UserDetailsModel userModel;
        public IncomingMessageModel messageModel;
        string[] options = new string[]
        {
            "help", "stop", "mute", "unmute", "snooze",
            "events", "reminders", "groups",
            "new", "invite"
        };

        public MessageParser(UserDetailsModel userModel, IncomingMessageModel messageModel)
        {
            this.userModel = userModel;
            this.messageModel = messageModel;
            userModel.phone_number = messageModel.initial_number;

            ParseMessage();

            //get user details after the message is parsed because not all details are required for all message types

            UserDetailsController userController = new UserDetailsController(userModel);
            userModel = userController.GetAllUserDetails();
        }


        public string ParseMessage()
        {
            //checkInitializationProgress();
            foreach (string option in options)
            {
                if (messageModel.initial_message.Contains(option))
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
                    messageModel.mute = true;
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
