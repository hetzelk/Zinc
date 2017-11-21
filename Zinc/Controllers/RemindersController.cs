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
    public class RemindersController
    {
        private AmazonDynamoDBClient client;

        public RemindersController()
        {
            client = new AmazonDynamoDBClient();
        }

        public List<RemindersModel> GetReminders(DateTime now)
        {
            List<RemindersModel> reminders = new List<RemindersModel>();
            //trim the date properly to get every reminder that starts with the current minute
            string timenow = now.ToString("o").Substring(0, 16);

            Table reminderTable = Table.LoadTable(client, RemindersTable.table_name);

            ScanFilter scanFilter = new ScanFilter();
            scanFilter.AddCondition(RemindersTable.reminder_date, ScanOperator.BeginsWith, timenow);
            scanFilter.AddCondition("valid", ScanOperator.Equal, new List<AttributeValue> { new AttributeValue { BOOL = true } });

            ScanOperationConfig config = new ScanOperationConfig()
            {
                Filter = scanFilter,
                Select = SelectValues.SpecificAttributes,
                AttributesToGet = typeof(RemindersTable).GetFields().Select(field => field.Name).ToList()
            };

            Search search = reminderTable.Scan(config);
            
            do
            {
                List<Document> documentList = search.GetNextSet();
                foreach (var document in documentList)
                {
                    reminders.Add(new RemindersModel(document));
                }
            } while (!search.IsDone);
            return reminders;
        }

        public void CreateReminder(RemindersModel reminder)
        {
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
            attributes[RemindersTable.reminder_uuid] = new AttributeValue { S = reminder.reminder_uuid };
            attributes[RemindersTable.reminder_date] = new AttributeValue { S = reminder.reminder_date };
            attributes[RemindersTable.event_uuid] = new AttributeValue { S = reminder.event_uuid };
            attributes[RemindersTable.group_uuid] = new AttributeValue { S = reminder.group_uuid };
            attributes[RemindersTable.user_uuid] = new AttributeValue { S = reminder.user_uuid };
            attributes[RemindersTable.valid] = new AttributeValue { BOOL = true };


            PutItemRequest request = new PutItemRequest
            {
                TableName = RemindersTable.table_name,
                Item = attributes
            };

            PutItemResponse response = client.PutItem(request);
        }

        public void CreateDefaultNotificationReminders(RemindersModel reminder)
        {
            GroupsController groups = new GroupsController();
            var members = groups.GetGroup(reminder.group_uuid);
            UsersController users = new UsersController();
            foreach (var member in members)
            {
                var user = users.GetUser(member.user_uuid);
                foreach (var time in user.default_reminder_times)
                {
                    //adjust the time accordingly right here
                    reminder.reminder_date = "";
                    CreateReminder(reminder);
                }
            }
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
            bool success = responder.sendEzMessage(user, text);
            if (success) UpdateDynamo(user, text);
        }

        private void UpdateDynamo(UserDetailsModel user, TextMessageModel text)
        {
            MessageHistoryController history = new MessageHistoryController();
            history.StoreMessage(user.phone_number, text, true);

            ReminderInvalid(text.reminder_uuid);
        }
        public void ReminderInvalid(string reminder_uuid)
        {
            UpdateItemRequest request = new UpdateItemRequest();
            request.TableName = RemindersTable.table_name;
            request.Key = new Dictionary<string, AttributeValue>
                {
                        { RemindersTable.reminder_uuid, new AttributeValue { S = reminder_uuid }}
                };
            request.AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                        { RemindersTable.valid, new AttributeValueUpdate(new AttributeValue { BOOL = false }, AttributeAction.PUT) }
                };

            UpdateItemResponse response = client.UpdateItem(request);
        }
    }
}