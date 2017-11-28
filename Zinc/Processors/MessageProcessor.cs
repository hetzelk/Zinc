using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Processors
{
    public class MessageProcessor
    {
        public MessageProcessor(UserDetailsModel userModel, IncomingMessageModel messageModel, MessageParser parser)
        {
            ProcessMessage();
        }

        public void ProcessMessage()
        {
            
        }
    }
}