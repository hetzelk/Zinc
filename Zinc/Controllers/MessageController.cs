using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Zinc.Controllers
{
    public class MessageController : ApiController
    {
        // GET zinc/schedule?from=123123123&message=helloWorld
        public string Get(string number, string message)
        {
            try
            {
                var message_whole = String.Format("{0} - {1}: {2}", DateTime.Now, number, message);

                var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/messages.txt";

                using (var writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(message_whole);
                }
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
                MessageParser parser = new MessageParser(number, message);
                MessageProcessor processor = new MessageProcessor(parser);
                Responder response = new Responder(processor);
                return "";
            }
            catch (Exception e)
            {
                var file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/exceptions.txt";

                using (var writer = new StreamWriter(file, true))
                {
                    writer.WriteLine(e.ToString());
                }

                return e.ToString();
            }
        }
    }
}
