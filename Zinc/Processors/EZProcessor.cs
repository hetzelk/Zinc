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

                MessageParser parser = new MessageParser(userModel, messageModel);
                MessageProcessor processor = new MessageProcessor(userModel, messageModel);
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