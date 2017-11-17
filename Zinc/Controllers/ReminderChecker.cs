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
            List<Document> reminders = db.GetReminders(now);
            Parallel.ForEach(reminders, reminder =>
            {
                TextMessageModel text = db.GetEventDetails(reminder["event_uuid"].ToString());

                
            });
        }

        private void SendReminders(Document document)
        {
            Responder responder = new Responder();
            //responder.sendMessage();
        }
    }
}