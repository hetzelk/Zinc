using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon.DynamoDBv2.DocumentModel;

namespace Zinc.Models
{
    public class RemindersModel
    {
        private Document document;

        public RemindersModel()
        {
        }

        public RemindersModel(Document document)
        {
            this.document = document;

            this.reminder_uuid = document[RemindersTable.reminder_uuid];
            this.reminder_date = document[RemindersTable.reminder_date];
            this.event_uuid = document[RemindersTable.event_uuid];
            this.reminder_user_uuid = document[RemindersTable.reminder_user_uuid];
            this.event_creator_user_uuid = document[RemindersTable.event_creator_user_uuid];
            this.valid = (bool)document[RemindersTable.valid];
        }

        public string reminder_uuid { get; set; }
        public string reminder_date { get; set; }
        public string event_date { get; set; }
        public string event_uuid { get; set; }
        public string reminder_user_uuid { get; set; }
        public string event_creator_user_uuid { get; set; }
        public bool valid { get; set; }
    }
}