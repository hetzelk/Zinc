using Amazon.Auth.AccessControlPolicy;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class UsersController
    {
        AmazonDynamoDBClient client;
        public UsersController()
        {
            client = new AmazonDynamoDBClient();
        }

        public UserDetailsModel GetUser(string user_uuid)
        {
            GetItemResponse entry = client.GetItem(
                UsersTable.table_name,
                new Dictionary<string, AttributeValue>
                {
                        { UsersTable.phone_number, new AttributeValue { S = user_uuid }}
                });

            return new UserDetailsModel(entry);
        }

        public void CreateUser(UserDetailsModel user)
        {
            Dictionary<string, AttributeValue> attributes = new Dictionary<string, AttributeValue>();
                attributes[UsersTable.phone_number] = new AttributeValue { S = user.phone_number };
                attributes[UsersTable.enabled] = new AttributeValue { BOOL = user.enabled };
                attributes[UsersTable.user_uuid] = new AttributeValue { S = user.user_uuid };
                attributes[UsersTable.first_name] = new AttributeValue { S = user.first_name };
                attributes[UsersTable.last_name] = new AttributeValue { S = user.last_name };
                attributes[UsersTable.mute] = new AttributeValue { BOOL = user.mute };
                attributes[UsersTable.birthday] = new AttributeValue { S = user.birthday };
                attributes[UsersTable.default_reminder_times] = new AttributeValue { SS = user.default_reminder_times };
                attributes[UsersTable.groups] = new AttributeValue { S = user.groups };
                //attributes["examplelist"] = new AttributeValue
                //{
                //    SS = new List<string> { "satire", "folk", "children's novel" }
                //};

            PutItemRequest request = new PutItemRequest
            {
                TableName = UsersTable.table_name,
                Item = attributes
            };

            PutItemResponse response = client.PutItem(request);
        }

        public void Unsubscribe(string user_uuid)
        {
            UpdateItemRequest request = new UpdateItemRequest();
            request.TableName = UsersTable.table_name;
            request.Key = new Dictionary<string, AttributeValue>
                {
                        { UsersTable.phone_number, new AttributeValue { S = user_uuid }}
                };
            request.AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                        { UsersTable.enabled, new AttributeValueUpdate(new AttributeValue { BOOL = false }, AttributeAction.PUT) }
                };

            UpdateItemResponse response = client.UpdateItem(request);
        }

        public void Mute(string user_uuid)
        {
            UpdateItemRequest request = new UpdateItemRequest();
            request.TableName = UsersTable.table_name;
            request.Key = new Dictionary<string, AttributeValue>
                {
                        { UsersTable.phone_number, new AttributeValue { S = user_uuid }}
                };
            request.AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                        { UsersTable.mute, new AttributeValueUpdate(new AttributeValue { BOOL = true }, AttributeAction.PUT) }
                };

            UpdateItemResponse response = client.UpdateItem(request);
        }

        public void UnMute(string user_uuid)
        {
            UpdateItemRequest request = new UpdateItemRequest();
            request.TableName = UsersTable.table_name;
            request.Key = new Dictionary<string, AttributeValue>
                {
                        { UsersTable.phone_number, new AttributeValue { S = user_uuid }}
                };
            request.AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                        { UsersTable.mute, new AttributeValueUpdate(new AttributeValue { BOOL = false }, AttributeAction.PUT) }
                };

            UpdateItemResponse response = client.UpdateItem(request);
        }
    }
}