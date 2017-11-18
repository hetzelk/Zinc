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
    public class GroupsController
    {
        private AmazonDynamoDBClient client;
        public GroupsController()
        {
            client = new AmazonDynamoDBClient();
        }

        public List<UserDetailsModel> GetGroup(string group_uuid)
        {
            GetItemResponse entry = client.GetItem(
                GroupsTable.table_name,
                new Dictionary<string, AttributeValue>
                {
                        { GroupsTable.group_uuid, new AttributeValue { S = group_uuid }}
                });

            var groupDetails = new GroupModel(entry);

            List<UserDetailsModel> members = new List<UserDetailsModel>();
            UsersController users = new UsersController();
            foreach (string user_uuid in groupDetails.user_uuids)
            {
                members.Add(users.GetUser(user_uuid));
            }

            return members;
        }
    }
}