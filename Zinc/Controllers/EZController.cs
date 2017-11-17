using System;
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

        /*
         * Future items to be added
         * name, group
         */
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
                return response.sendMessage("");
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
