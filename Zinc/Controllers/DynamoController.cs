using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Document> GetReminders(DateTime now)
        {
            List<Document> reminders = new List<Document>();

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

        public TextMessageModel GetEventDetails(string event_uuid)
        {
            Dictionary<string, Condition> keyConditions = GenerateKeyCondition(EventsTable.event_uuid, event_uuid);
            
            QueryRequest request = new QueryRequest()
            {
                TableName = EventsTable.table_name,
                Limit = 1,
                AttributesToGet = typeof(EventsTable).GetFields().Select(field => field.Name).ToList(),
                ConsistentRead = true,
                KeyConditions = keyConditions
            };

            return new TextMessageModel(client.Query(request));        
        }

        public List<UserDetailsModel> GetGroup(string group_uuid)
        {
            Dictionary<string, Condition> keyConditions = GenerateKeyCondition(GroupsTable.group_uuid, group_uuid);

            QueryRequest request = new QueryRequest()
            {
                TableName = GroupsTable.table_name,
                Limit = 1,
                AttributesToGet = typeof(GroupsTable).GetFields().Select(field => field.Name).ToList(),
                ConsistentRead = true,
                KeyConditions = keyConditions
            };

            var groupDetails = new GroupModel(client.Query(request));

            List<UserDetailsModel> members = new List<UserDetailsModel>();
            foreach (string number in groupDetails.user_uuids)
            {
                members.Add(GetUser(number));
            }

            return members;
        }

        public UserDetailsModel GetUser(string phone_number)
        {
            Dictionary<string, Condition> keyConditions = GenerateKeyCondition(UsersTable.phone_number, phone_number);

            QueryRequest request = new QueryRequest()
            {
                TableName = UsersTable.table_name,
                Limit = 1,
                AttributesToGet = typeof(EventsTable).GetFields().Select(field => field.Name).ToList(),
                ConsistentRead = true,
                KeyConditions = keyConditions
            };

            return new UserDetailsModel(client.Query(request));
        }

        public Dictionary<string, Condition> GenerateKeyCondition(string key, string value)
        {
            Dictionary<string, Condition> keyConditions = new Dictionary<string, Condition>
            {
                {
                    key,
                    new Condition
                    {
                        ComparisonOperator = "EQ",
                        AttributeValueList = new List<AttributeValue>
                        {
                            new AttributeValue { S = value }
                        }
                    }
                }
            };

            return keyConditions;
        }
    }
}