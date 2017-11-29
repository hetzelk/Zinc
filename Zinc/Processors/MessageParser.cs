using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Extensions;
using Zinc.Models;

namespace Zinc.Processors
{
    public class MessageParser
    {
        public MessageParser(UserDetailsModel userModel, IncomingMessageModel messageModel)
        {
            userModel.phone_number = messageModel.initial_number;

            ParseMessage(messageModel);

            //get user details after the message is parsed because not all details are required for all message types

            //UsersController userController = new UsersController(userModel);
            //userModel = userController.GetAllUserDetails();
        }


        public void ParseMessage(IncomingMessageModel messageModel)
        {
            string[] options = new string[]
            {
                "ezzinc",
                "help", "stop", "mute", "unmute", "snooze",
                "events", "reminders", "groups",
                "new", "change", "delete",
                "event", "group", "invite"
            };

            //checkInitializationProgress();
            foreach (string option in options)
            {
                if (messageModel.initial_message.ToLower().Contains(option))
                {
                    //find all things that can be set with a bool
                    SetOptions(messageModel, option.ToLower());
                }
            }

            //only parse through for all the dates if a date is required
            if (messageModel.requiresDate)
            {
                int hour = 0;

                int counter = 0;
                bool addTitle = true;
                foreach (string word in messageModel.initial_message_array)
                {
                    if (word.ToLower().Contains("am") || word.ToLower().Contains("pm"))
                    {
                        try
                        {
                            hour = word.GetDateTimeHours();
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            DateTime eventDate = word.ToDateTime();

                            messageModel.eventDate = eventDate.AddHours(-eventDate.Hour);
                            addTitle = false;
                        }
                        catch { }
                    }

                    if (counter >= 2 && addTitle)
                    {
                        messageModel.eventName += word + " ";
                    }
                    counter++;
                }

                if (hour != 0)
                {
                    messageModel.eventDate = messageModel.eventDate.AddHours(hour);
                }
            }

            if (messageModel.newGroup)
            {
                //invite user to group and the user can approve joining the group to get those reminders
            }

            if (messageModel.newInvite)
            {

            }
        }

        public void SetOptions(IncomingMessageModel messageModel, string option)
        {
            switch (option)
            {
                case "ezzinc":
                    messageModel.ezzinc = true;
                    break;

                case "help":
                    if (messageModel.initial_message.ToLower() == "help")
                    {
                        messageModel.help = true;
                    }
                    break;
                case "stop":
                    if (messageModel.initial_message.ToLower() == "stop")
                    {
                        messageModel.stop = true;
                    }
                    break;
                case "mute":
                    if (messageModel.initial_message.ToLower() == "mute")
                    {
                        messageModel.mute = true;
                    }
                    break;
                case "unmute":
                    if (messageModel.initial_message.ToLower() == "unmute")
                    {
                        messageModel.unmute = true;
                    }
                    break;
                case "snooze":
                    if (messageModel.initial_message.ToLower() == "snooze")
                    {
                        messageModel.snooze = true;
                    }
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
                    messageModel.create = true;
                    break;
                case "change":
                    messageModel.change = true;
                    break;
                case "delete":
                    messageModel.delete = true;
                    break;

                case "event":
                    messageModel.itemEvent = true;
                    break;
                case "group":
                    messageModel.itemGroup = true;
                    break;
                case "invite":
                    messageModel.itemInvite = true;
                    break;

                default:
                    break;
            }
        }
    }
}
