using System;
using Amazon.DynamoDBv2.Model;
using Zinc.Controllers;
using Zinc.Extensions;

namespace Zinc.Models
{
    public class EventsModel
    {
        public EventsModel()
        {
        }

        public EventsModel(GetItemResponse queryResponse)
        {
            this.event_date = queryResponse.Item[EventsTable.event_date].S;
            this.event_name = queryResponse.Item[EventsTable.event_name].S;
            this.description = queryResponse.Item[EventsTable.description].S;
            this.note = queryResponse.Item[EventsTable.note].S;
            this.surprise = queryResponse.Item[EventsTable.surprise].BOOL;
            this.user_uuid = queryResponse.Item[EventsTable.user_uuid].S;
        }

        public EventsModel(newEventModel new_event)
        {

            this.event_date = new_event.event_date;
            this.event_name = new_event.event_name;
            this.description = new_event.description;
            this.note = new_event.note;
            this.repeat_cadence = new_event.repeat_cadence;
            this.surprise = new_event.surprise.ToBool();
            this.user_uuid = new_event.phone_number;
        }

        public string event_uuid { get; set; }
        public string event_date { get; set; }
        public string event_name { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string repeat_cadence { get; set; }
        public bool surprise { get; set; }
        public string user_uuid { get; set; }
    }
}