using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Controllers;
using Zinc.Models;

namespace Zinc.Processors
{
    public class NewEventProcessor
    {
        public string response;
        private newEventModel new_event;

        public NewEventProcessor(newEventModel new_event)
        {
            this.new_event = new_event;
            ProcessNewEvent();
        }

        public void ProcessNewEvent()
        {
            EventsModel event_model = new EventsModel(new_event);
            event_model.event_uuid = new EventsController().CreateEvent(event_model);

            GroupsController groups = new GroupsController();
            List<UserDetailsModel> members = groups.GetGroup(new_event.group);

            RemindersController reminders = new RemindersController();
            RemindersModel reminder = null;
            foreach (UserDetailsModel user in members)
            {
                reminder = new RemindersModel();
                reminder.reminder_user_uuid = user.user_uuid;
                reminder.event_uuid = event_model.event_uuid;
                reminder.event_date = new_event.event_date;
                reminder.event_creator_user_uuid = new_event.phone_number;

                reminders.CreateDefaultNotificationReminders(user, reminder);
            }
        }
    }
}