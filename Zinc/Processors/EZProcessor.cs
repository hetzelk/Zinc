using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Controllers;
using Zinc.Extensions;
using Zinc.Models;

namespace Zinc.Processors
{
    public class EZProcessor
    {
        public string response;

        private IncomingMessageModel messageModel;
        public UserDetailsModel userModel;

        public EZProcessor(IncomingMessageModel messageModel)
        {
            userModel = new UserDetailsModel();
            userModel.phone_number = messageModel.initial_number;

            this.messageModel = messageModel;

            ProcessIncoming();
        }

        public void ProcessIncoming()
        {
            try
            {
                var message_whole = String.Format("{0} - {1}: {2}", DateTime.Now, messageModel.initial_number, messageModel.initial_message);
                Logger logs = new Logger("logs", message_whole);
                logs.Dispose(logs);

                /*
                 user initilization
                 figure out a way to get all the user details
                 *new user*
                 Make a website to speed up this process.

                The user configuration will be the most time consuming part

                reply yes to just use all the defaults, they are the most optimized for simplicity

                 "Are these default reminder times fine? Or would you like to add or remove them? 30 minutes before, 2 hours before, 24 hours before event.
                 Reply with "ADD 12h" to add a 12 hour reminder"

                "Is the default event time of 2PM fine? Otherwise you have to specify an event time plus the date."
                 */

                MessageParser parser = new MessageParser(userModel, messageModel);
                MessageProcessor processor = new MessageProcessor(userModel, messageModel, parser);
                Responder responder = new Responder(userModel, messageModel, processor);

                string response_text = String.Format("You said {0} at {1}", messageModel.initial_message, DateTime.Now.ToUniversalTime().ToString("o"));

                if (messageModel.sendText)
                {
                    if (responder.sendMessage(userModel.phone_number, response_text))
                    {
                        response = response_text;
                    }
                    else
                    {
                        response = "sending message failed";
                    }
                }
                else
                {
                    response = response_text + " ADDITIONAL: Text not sent to phone";
                }
            }
            catch (Exception e)
            {
                Logger logs = new Logger("exceptions", DateTime.Now.ToString() + " - " + e.ToString());
                logs.Dispose(logs);

                response = e.ToString();
            }
        }
    }
}