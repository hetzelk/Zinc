using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.Model;

namespace Zinc.Models
{
    public class GroupModel
    {
        private GetItemResponse queryResponse;

        public GroupModel()
        {
        }

        public GroupModel(GetItemResponse queryResponse)
        {
            this.queryResponse = queryResponse;

            this.group_name = queryResponse.Item[GroupsTable.group_name].S;
            this.member_uuids = queryResponse.Item[GroupsTable.members].SS;
        }

        public string group_uuid { get; set; }
        public string group_name { get; set; }
        public List<string> member_uuids { get; set; }
        public List<UserDetailsModel> members { get; set; }
    }
}