using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class UserDetailsController
    {
        private UserDetailsModel userModel;
        private AmazonDynamoDBClient client;

        public UserDetailsController(UserDetailsModel userModel)
        {
            client = new AmazonDynamoDBClient();
            this.userModel = userModel;
        }

        /*
        parse the message first, then load the user details
        only load the user details that you need
        if user is scheduling something, you don't need to load everything, 
        same as if they're just snoozing, etc

        */

        public UserDetailsModel GetAllUserDetails()
        {
            var entry = client.GetItem("Users",
                new Dictionary<string, AttributeValue>
                {
                        { "phone_number", new AttributeValue { S = userModel.phone_number }}
                });

            //search db for current user(phone number lookup)
            //get all their details

            //go through UserDetailsCOntroller
            if (entry.Item.ContainsKey("first_name"))
            {
                userModel.user_uuid = entry.Item["user_uuid"].S;
                userModel.first_name = entry.Item["first_name"].S;
                userModel.last_name = entry.Item["last_name"].S;
                userModel.default_reminder_times = entry.Item["default_reminder_times"].S;
                userModel.birthday = entry.Item["birthday"].S;
                userModel.mute = entry.Item["mute"].BOOL;
            }
            else
            {
                userModel.first_name = "Unknown";
                userModel.phone_number = "";
            }

            return userModel;
        }

        public void GetField(string field)
        {

        }

        public void GetUserGroups()
        {
            var entry = client.GetItem("Users",
                new Dictionary<string, AttributeValue>
                {
                        { "phone_number", new AttributeValue { S = userModel.phone_number }}
                });

            if (entry.Item.ContainsKey("groups"))
            {

            }
        }

        public void Mute()
        {

        }

        public void UnMute()
        {

        }

        public void UpdateDynamoExample(string phone_number)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                var request = new UpdateItemRequest
                {
                    TableName = "Users",
                    Key = new Dictionary<string, AttributeValue>
                        {
                            { "phone_number", new AttributeValue { S = phone_number }}
                        },
                    AttributeUpdates = new Dictionary<string, AttributeValueUpdate>()
                        {
                            { "ColumnName ex:phone_number", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = "4145200673" } } }
                        }
                };
                var updateItemResponse = client.UpdateItem(request);
            }
        }
    }
}