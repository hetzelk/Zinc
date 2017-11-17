using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class MessageProcessor
    {
        public UserDetailsModel userModel;
        public IncomingMessageModel messageModel;

        public MessageProcessor(MessageParser parser)
        {
            this.userModel = parser.userModel;
            this.messageModel = parser.messageModel;

            ProcessMessage();
        }

        public void ProcessMessage()
        {
            
        }
    }
}