using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.Model;

namespace Zinc.Models
{
    public class TextMessageModel
    {
        private QueryResponse queryResponse;

        public TextMessageModel()
        {
        }

        public TextMessageModel(QueryResponse queryResponse)
        {
            this.queryResponse = queryResponse;

            this.event_date = queryResponse.Items[0][EventsTable.event_date].S;
            this.event_name = queryResponse.Items[0][EventsTable.event_name].S;
            this.description= queryResponse.Items[0][EventsTable.description].S;
            this.note = queryResponse.Items[0][EventsTable.note].S;
            this.surprise = queryResponse.Items[0][EventsTable.surprise].BOOL;
        }

        public string event_date { get; set; }
        public string event_name { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public bool surprise { get; set; }
    }
}