using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.Model;

namespace Zinc.Models
{
    public class UserDetailsModel
    {
        private GetItemResponse queryResponse;

        public UserDetailsModel()
        {
        }

        public UserDetailsModel(GetItemResponse queryResponse)
        {
            this.queryResponse = queryResponse;

            this.phone_number = queryResponse.Item[UsersTable.phone_number].S;//phone_number is the key on this table
            this.user_uuid = queryResponse.Item[UsersTable.phone_number].S;
            this.enabled = queryResponse.Item[UsersTable.enabled].BOOL;
            this.first_name = queryResponse.Item[UsersTable.first_name].S;
            this.last_name = queryResponse.Item[UsersTable.last_name].S;
            this.mute = queryResponse.Item[UsersTable.mute].BOOL;
            this.birthday = queryResponse.Item[UsersTable.birthday].S;
            this.default_reminder_times = queryResponse.Item[UsersTable.default_reminder_times].SS;
            this.groups = queryResponse.Item[UsersTable.groups].S;
            this.status = queryResponse.Item[UsersTable.status].S;
        }

        public string user_uuid { get; set; }
        public bool enabled { get; set; }
        public string phone_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool mute { get; set; }
        public string birthday { get; set; }        
        public List<string> default_reminder_times { get; set; }//string array in minute increments, 60, 120, 1440 defaults
        public string groups { get; set; }
        public string status { get; set; }
    }
}