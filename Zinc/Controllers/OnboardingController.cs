using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class OnboardingController
    {
        public Responder responder;
        TextMessageModel textModel;
        public UserDetailsModel userModel;
        public IncomingMessageModel messageModel;

        public OnboardingController(UserDetailsModel userModel, IncomingMessageModel messageModel)
        {
            this.responder = new Responder();
            this.textModel = new TextMessageModel();
            this.userModel = userModel;
            this.messageModel = messageModel;

            switch (userModel.status)
            {
                case "0":
                    BeginOnboarding();
                    break;

                default:
                    break;
            }
        }

        public void BeginOnboarding()
        {
            textModel.description = "Welcome, Thanks for joining ZINC! What is your first name? So your contacts know who you are.";

            responder.sendEzMessage(userModel, textModel);
        }

        public void Step2()
        {
            textModel.description = "Thanks " + userModel.first_name + ", now would you like to go with the optimized settings?(or customize them?) [YES] or [NO]";

            responder.sendEzMessage(userModel, textModel);
        }
    }
}