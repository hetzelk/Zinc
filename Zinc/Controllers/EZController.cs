using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Zinc.Extensions;
using Zinc.Models;
using Zinc.Processors;

namespace Zinc.Controllers
{
    public class EZController : ApiController
    {
        IncomingMessageModel messageModel;

        // GET api/ez?from=123123123&message=helloWorld
        public string Get()
        {
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

            return new EZProcessor(messageModel).response;
        }

        // POST api/ez
        public string Post([FromBody]newEventModel new_event)
        {
            return new NewEventProcessor(new_event).response;
        }
    }

    public class newEventModel
    {
        public string phone_number { get; set; }
        public string event_name { get; set; }
        public string event_date { get; set; }
        public string description { get; set; }
        public string note { get; set; }
        public string group { get; set; }
        public string repeat_cadence { get; set; }
        public string surprise { get; set; }
    }
}