using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zinc.Controllers
{
    public class MessageProcessor
    {
        private MessageParser parser;

        public MessageProcessor(MessageParser parser)
        {
            this.parser = parser;
        }
    }
}