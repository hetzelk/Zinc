using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class EventsController
    {
        private AmazonDynamoDBClient client;
        public EventsController()
        {
            client = new AmazonDynamoDBClient();
        }

        public TextMessageModel GetEventForText(string event_uuid)
        {
            GetItemResponse entry = client.GetItem(
                EventsTable.table_name,
                new Dictionary<string, AttributeValue>
                {
                        { EventsTable.event_uuid, new AttributeValue { S = event_uuid }}
                });

            return new TextMessageModel(entry);
        }

        public EventsModel GetEvent(string event_uuid)
        {
            GetItemResponse entry = client.GetItem(
                EventsTable.table_name,
                new Dictionary<string, AttributeValue>
                {
                        { EventsTable.event_uuid, new AttributeValue { S = event_uuid }}
                });

            return new EventsModel(entry);
        }

        public string CreateEvent(EventsModel newEvent)
        {
            DateTime date = DateTime.Parse(newEvent.event_date);
            string event_uuid = date.Year + "" + date.Month + "" + date.Day + new Random().Next(0, 99999);
            
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
            attributes[EventsTable.event_uuid] = new AttributeValue { S = event_uuid };
            attributes[EventsTable.event_date] = new AttributeValue { S = newEvent.event_date };
            attributes[EventsTable.event_name] = new AttributeValue { S = newEvent.event_name };
            attributes[EventsTable.description] = new AttributeValue { S = newEvent.description };
            attributes[EventsTable.note] = new AttributeValue { S = newEvent.note };
            attributes[EventsTable.repeat_cadence] = new AttributeValue { S = newEvent.repeat_cadence };
            attributes[EventsTable.surprise] = new AttributeValue { BOOL = newEvent.surprise };
            attributes[EventsTable.user_uuid] = new AttributeValue { S = newEvent.user_uuid };

            PutItemRequest request = new PutItemRequest
            {
                TableName = EventsTable.table_name,
                Item = attributes
            };

            PutItemResponse response = client.PutItem(request);

            return event_uuid;
        }

        public void CreateEventAndReminders(EventsModel newEvent)
        {
            CreateEvent(newEvent);
            RemindersController reminders = new RemindersController();
            RemindersModel reminder = new RemindersModel();

            //create reminder_uuid in the createreminder function
            reminder.reminder_date = newEvent.event_date;
            reminder.event_uuid = newEvent.event_uuid;
            reminder.reminder_user_uuid = newEvent.user_uuid;
            reminder.event_creator_user_uuid = newEvent.user_uuid;//TODO: figure out how to get this
            reminders.CreateReminder(reminder);
        }
    }
}