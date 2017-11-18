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
            ReminderController remindersCon = new ReminderController();
            GroupsController groups = new GroupsController();
            UsersController users = new UsersController();
            Responder responder = new Responder();

            List<Document> reminders = remindersCon.GetReminders(now);
            Parallel.ForEach(reminders, reminder =>
            {
                TextMessageModel text = events.GetEventDetails(reminder["event_uuid"].ToString());
                List<UserDetailsModel> group = new List<UserDetailsModel>();
                if (reminder["group_uuid"].ToString() != "0")
                {
                    text.group_uuid = reminder["group_uuid"].ToString();

                    group = groups.GetGroup(text.group_uuid);
                }
                else
                {
                    group.Add(users.GetUser(reminder["user_uuid"].ToString()));
                }

                remindersCon.SendReminders(group, text);
            });
        }
    }
}