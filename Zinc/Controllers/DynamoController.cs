using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class DynamoController
    {
        AmazonDynamoDBClient client;

        public DynamoController()
        {
            client = new AmazonDynamoDBClient();
        }

        public void GetDynamoReminders(DateTime now)
        {
            EventsController events = new EventsController();
            RemindersController remindersCon = new RemindersController();
            GroupsController groups = new GroupsController();
            UsersController users = new UsersController();
            //Responder responder = new Responder();

            List<RemindersModel> reminders = remindersCon.GetReminders(now);
            Parallel.ForEach(reminders, reminder =>
            {
                TextMessageModel text = events.GetEventForText(reminder.event_uuid);
                UserDetailsModel user = new UsersController().GetUser(text.user_uuid);
                remindersCon.SendReminder(user, text);
            });
        }
    }
}