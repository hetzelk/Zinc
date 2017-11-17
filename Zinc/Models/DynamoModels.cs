using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Models
{
    public static class UsersTable
    {
        public static string table_name = "Users";

        //user_uuid and phone_number are the same.
        //This just makes sense because an ID of phone_number wouldn't make sense in some cases, 
        //but without phone number, user_uuid would be confusing
        public static string phone_number = "phone_number";
        public static string user_uuid = "user_uuid";
        public static string enabled = "enabled";
        public static string first_name = "first_name";
        public static string last_name = "last_name";
        public static string birthday = "birthday";
        public static string mute = "mute";
        public static string default_reminder_times = "default_reminder_times";
        public static string surprise = "surprise";
        public static string groups = "groups";
    }

    public static class GroupsTable
    {
        public static string table_name = "Groups";

        public static string group_uuid = "group_uuid";
        public static string group_name = "group_name";
        public static string members = "members";
    }

    public static class EventsTable
    {
        public static string table_name = "Events";

        public static string event_uuid = "event_uuid";
        public static string event_date = "event_date";
        public static string event_name = "event_name";
        public static string description = "description";
        public static string note = "note";
        public static string repeat_cadence = "repeat_cadence";
        public static string surprise = "surprise";
        public static string user_uuid = "user_uuid";
    }

    public static class RemindersTable
    {
        public static string table_name = "Reminders";

        public static string reminder_uuid = "reminder_uuid";
        public static string reminder_date = "reminder_date";
        public static string event_uuid = "event_uuid";
        public static string group_uuid = "group_uuid";
        public static string user_uuid = "user_uuid";
        public static string valid = "valid";
    }

    public static class MessageHistoryTable
    {
        public static string table_name = "MessageHistory";


    }
}