using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
            DynamoController dynamo = new DynamoController();
            List<Document> reminders = dynamo.GetReminders(now);
            Parallel.ForEach(reminders, reminder =>
            {
                dynamo.GetEventDetails(reminder);

            });
        }

        private void SendReminders(Document document)
        {
            Responder responder = new Responder();
            //responder.sendMessage();
        }
    }
}