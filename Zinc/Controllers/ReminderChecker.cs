using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Zinc.Models;
using Zinc.Extensions;

namespace Zinc.Controllers
{
    public class ReminderChecker
    {
        private AmazonDynamoDBClient client;

        public ReminderChecker()
        {
            client = new AmazonDynamoDBClient();
        }

        public void GetDynamoReminders(DateTime now)
        {
            DynamoController db = new DynamoController();
            //change this later, make sure it is in UTC, this is just my watch time
            List<Document> reminders = db.GetReminders(now);
            Parallel.ForEach(reminders, reminder =>
            {
                TextMessageModel text = db.GetEventDetails(reminder["event_uuid"].ToString());
                if (reminder["group_uuid"].ToString() != "0")
                {
                    text.group_uuid = reminder["group_uuid"].ToString();

                    List<UserDetailsModel> group = db.GetGroup(text.group_uuid);
                    SendReminders(group, text);
                }
                else
                {
                    UserDetailsModel user = db.GetUser(reminder["user_uuid"].ToString());
                    SendReminders(user, text);
                }
            });
        }

        private void SendReminders(List<UserDetailsModel> members, TextMessageModel text)
        {
            foreach (var member in members)
            {
                SendReminders(member, text);
            }
        }

        private void SendReminders(UserDetailsModel user, TextMessageModel text)
        {
            Responder responder = new Responder();
            responder.sendEzMessage(user, text);
        }
    }
}