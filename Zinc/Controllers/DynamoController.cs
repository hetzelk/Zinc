using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Controllers
{
    public class DynamoController
    {
        AmazonDynamoDBClient client;

        public DynamoController()
        {
            client = new AmazonDynamoDBClient();
        }

        public List<Document> GetReminders(DateTime now)
        {
            List<Document> reminders = new List<Document>();
            var client = new AmazonDynamoDBClient();

            string timenow = now.ToString("o");
            timenow.Trim();
            Table reminderTable = Table.LoadTable(client, "Reminders");
            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition("reminder_date", ScanOperator.BeginsWith, "11-24-2017T18:00");

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "reminder_date", "valid", "event_uuid", "user_uuid", "group_uuid" }
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

        public void GetEventDetails(Document reminder)
        {
            var client = new AmazonDynamoDBClient();

            Table reminderTable = Table.LoadTable(client, "Events");

            var event_uuid = reminder["event_uuid"].ToString();

            QueryFilter filter = new QueryFilter("event_uuid", QueryOperator.Equal, event_uuid);

            QueryOperationConfig config = new QueryOperationConfig()
            {
                Limit = 2, // 2 items/page.
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = new List<string> { "reminder_date", "valid", "event_uuid", "user_uuid", "group_uuid" },
                ConsistentRead = true,
                Filter = filter
            };

            Search search = reminderTable.Query(config);

            List<Document> documentList = new List<Document>();
            do
            {
                documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    foreach (var key in document.Keys)
                    {

                        //add the details to the text message modal here
                    }
                }
            } while (!search.IsDone);
            //return textmessagemodel            
        }
    }
}