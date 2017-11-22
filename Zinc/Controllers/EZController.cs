﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zinc.Extensions;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class EZController : ApiController
    {
        UserDetailsModel userModel;
        IncomingMessageModel messageModel;

        // GET api/ez?from=123123123&message=helloWorld
        public string Get()
        {
            userModel = new UserDetailsModel();
            messageModel = new IncomingMessageModel();

            var query_params = Request.GetQueryNameValuePairs();
            messageModel.initial_number = query_params.Where(item => item.Key == "from").First().Value.ToString();
            messageModel.initial_message = query_params.Where(item => item.Key == "message").First().Value.ToString();
            messageModel.initial_message_array = query_params.Where(item => item.Key == "message").First().Value.Split(' ').ToArray();

            try
            {
                messageModel.stampToSend = query_params.Where(item => item.Key == "StampToSend").First().Value.ToString();
            }
            catch { messageModel.stampToSend = "null"; }

            try
            {
                messageModel.sendText = query_params.Where(item => item.Key == "sendtext").First().Value.ToBool();
            }
            catch { messageModel.sendText = true; }

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
                MessageProcessor processor = new MessageProcessor(parser);
                Responder response = new Responder(processor);

                string response_text = String.Format("You said {0} at {1}", messageModel.initial_message, DateTime.Now.ToUniversalTime().ToString("o"));

                if (messageModel.sendText)
                {
                    if (response.sendMessage(userModel.phone_number, response_text))
                    {
                        return response_text;
                    }
                    else
                    {
                        return "sending message failed";
                    }
                }
                else
                {
                    return response_text + " ADDITIONAL: Text not sent to phone";
                }
            }
            catch (Exception e)
            {
                Logger logs = new Logger("exceptions", DateTime.Now.ToString() + " - " + e.ToString());
                logs.Dispose(logs);

                return e.ToString();
            }
        }
    }
}
