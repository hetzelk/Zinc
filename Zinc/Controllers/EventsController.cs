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

        public TextMessageModel GetEventDetails(string event_uuid)
        {
            GetItemResponse entry = client.GetItem(
                EventsTable.table_name,
                new Dictionary<string, AttributeValue>
                {
                        { EventsTable.event_uuid, new AttributeValue { S = event_uuid }}
                });

            return new TextMessageModel(entry);
        }
    }
}