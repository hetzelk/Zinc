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
    public class ReminderController
    {
        private AmazonDynamoDBClient client;

        public ReminderController()
        {
            client = new AmazonDynamoDBClient();
        }

        public List<Document> GetReminders(DateTime now)
        {
            List<Document> reminders = new List<Document>();
            string timenow = now.ToString("o").Substring(0, 16);

            Table reminderTable = Table.LoadTable(client, RemindersTable.table_name);

            ScanFilter scanFilter = new ScanFilter();
            //trim the date properly to get every minutes time
            scanFilter.AddCondition(RemindersTable.reminder_date, ScanOperator.BeginsWith, timenow);

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = typeof(RemindersTable).GetFields().Select(field => field.Name).ToList()
            };

            Search search = reminderTable.Scan(config);

            List<Document> documentList = new List<Document>();
            do
            {
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    reminders.Add(document);
                }
            } while (!search.IsDone);
            return reminders;
        }

        public void SendReminders(List<UserDetailsModel> members, TextMessageModel text)
        {
            foreach (var member in members)
            {
                SendReminder(member, text);
            }
        }

        public void SendReminder(UserDetailsModel user, TextMessageModel text)
        {
            Responder responder = new Responder();
            responder.sendEzMessage(user, text);
        }
    }
}