using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.Model;

namespace Zinc.Models
{
    public class TextMessageModel
    {
        private GetItemResponse queryResponse;

        public TextMessageModel()
        {
        }

        public TextMessageModel(GetItemResponse queryResponse)
        {
            this.queryResponse = queryResponse;

            this.event_date = queryResponse.Item[EventsTable.event_date].S;
            this.event_name = queryResponse.Item[EventsTable.event_name].S;
            this.description= queryResponse.Item[EventsTable.description].S;
            this.note = queryResponse.Item[EventsTable.note].S;
            this.surprise = queryResponse.Item[EventsTable.surprise].BOOL;
            this.user_uuid = queryResponse.Item[EventsTable.user_uuid].S;
        }

        public string event_date { get; set; }
        public string event_name { get; set; }
        public string reminder_uuid { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string user_uuid { get; set; }
        public bool surprise { get; set; }
    }
}