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

        private void UnsubscribeUser(UserDetailsModel userModel)
        {
            //double check with them first, but this will just unsubscribe them right away for now
            userscon.Unsubscribe(userModel.user_uuid);
        }

        private void OnboardUser(IncomingMessageModel messageModel)
        {
            throw new NotImplementedException();
        }
    }
}