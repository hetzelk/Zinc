using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class MessageHistoryController
    {
        AmazonDynamoDBClient client;
        public MessageHistoryController()
        {
            client = new AmazonDynamoDBClient();
        }

        public void StoreMessage(string phone_number, TextMessageModel text, bool successful)
        {
            Random random = new Random();
            string message_uuid = DateTime.Now.ToUniversalTime().ToString("o") + random.Next(1, 100000).ToString();
            
            var charsToRemove = new string[] { "-", "T", ":", "." };
            foreach (var c in charsToRemove)
            {
                message_uuid = message_uuid.Replace(c, string.Empty);
            }

            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
                attributes[MessageHistoryTable.message_uuid] = new AttributeValue { S = message_uuid };
                attributes[MessageHistoryTable.message_date] = new AttributeValue { S = DateTime.Now.ToUniversalTime().ToString("o") };
                attributes[MessageHistoryTable.reminder_uuid] = new AttributeValue { S = text.reminder_uuid };
                attributes[MessageHistoryTable.user_uuid] = new AttributeValue { S = phone_number };
                attributes[MessageHistoryTable.message] = new AttributeValue { S = text.description };
                attributes[MessageHistoryTable.successful] = new AttributeValue { BOOL = successful };

            PutItemRequest request = new PutItemRequest
            {
                TableName = MessageHistoryTable.table_name,
                Item = attributes
            };

            PutItemResponse response = client.PutItem(request);
        }
    }
}