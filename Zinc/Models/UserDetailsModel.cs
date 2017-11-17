using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.Model;

namespace Zinc.Models
{
    public class UserDetailsModel
    {
        private QueryResponse queryResponse;

        public UserDetailsModel()
        {
        }

        public UserDetailsModel(QueryResponse queryResponse)
        {
            this.queryResponse = queryResponse;
            
            this.phone_number = queryResponse.Items[0][UsersTable.phone_number].S;//phone_number is uuid
            this.enabled = queryResponse.Items[0][UsersTable.enabled].BOOL;
            this.first_name = queryResponse.Items[0][UsersTable.first_name].S;
            this.last_name = queryResponse.Items[0][UsersTable.last_name].S;
            this.mute = queryResponse.Items[0][UsersTable.mute].BOOL;
            this.birthday = queryResponse.Items[0][UsersTable.birthday].S;
            this.default_reminder_times = queryResponse.Items[0][UsersTable.default_reminder_times].S;
            this.groups = queryResponse.Items[0][UsersTable.groups].S;
        }

        public string user_uuid { get; set; }
        public bool enabled { get; set; }
        public string phone_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public bool mute { get; set; }
        public string birthday { get; set; }        
        public string default_reminder_times { get; set; }//string array in minute increments, 60, 120, 1440 defaults
        public string groups { get; set; }
    }
}