using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Controllers;
using Zinc.Models;

namespace Zinc.Processors
{
    public class MessageProcessor
    {
        UsersController userscon = new UsersController();
        EventsController eventscon = new EventsController();
        RemindersController reminderscon = new RemindersController();
        GroupsController groupscon = new GroupsController();
        MessageHistoryController messagehistcon = new MessageHistoryController();

        public MessageProcessor(UserDetailsModel userModel, IncomingMessageModel messageModel)
        {
            userModel = userscon.GetUser(userModel.phone_number);

            if (messageModel.ezzinc)
            {
                //start the sign on process if they already haven't
                OnboardUser(messageModel);
            }

            if (messageModel.stop)
            {
                //double check if they want to unsubscribe and turn off
                UnsubscribeUser(userModel);
            }

            if (messageModel.snooze)
            {
                //get the latest message that was sent and resend their latest reminder message in X minutes (the specific users snooze var)

            }

            if (messageModel.mute)
            {
                userscon.Mute(userModel.user_uuid);
            }

            if (messageModel.unmute)
            {
                userscon.UnMute(userModel.user_uuid);
            }

            if (messageModel.create)
            {
                if (messageModel.itemEvent)
                {

                }
                else if (messageModel.itemGroup)
                {

                }
                else if (messageModel.itemInvite)
                {

                }
                else
                {
                    // should not get here
                }
            }

            if (messageModel.change)
            {
                if (messageModel.itemEvent)
                {

                }
                else if (messageModel.itemGroup)
                {

                }
                else if (messageModel.itemInvite)
                {

                }
                else
                {
                    // should not get here
                }
            }

            if (messageModel.delete)
            {
                if (messageModel.itemEvent)
                {

                }
                else if (messageModel.itemGroup)
                {

                }
                else if (messageModel.itemInvite)
                {

                }
                else
                {
                    // should not get here
                }
            }
        }

        private void OnboardUser(IncomingMessageModel messageModel)
        {
            /*
             user initilization
             figure out a way to get all the user details
             *new user*
             Make a website to speed up this process.

            The user configuration will be the most time consuming part

            reply yes to just use all the defaults, they are the most optimized for simplicity

             "Are these default reminder times fine? Or would you like to add or remove them? 30 minutes before, 2 hours before, 24 hours before event.
             Reply with "ADD 12h" to add a 12 hour reminder"

            "Is the default event time of 2PM fine? Otherwise you have to specify an event time plus the date."
             */
            throw new NotImplementedException();
        }

        private void UnsubscribeUser(UserDetailsModel userModel)
        {
            //double check with them first, but this will just unsubscribe them right away for now
            userscon.Unsubscribe(userModel.user_uuid);
        }
    }
}