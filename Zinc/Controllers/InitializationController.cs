using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class InitializationController
    {
        Dictionary<int, string> questions = new Dictionary<int, string>();
        //{
        //    1: "What is your first name?",
        //    2: "WHen is your birthday? example: 5/21/1985",
        //    3: "Are all the dfaults alright? Or would you like to customize everything? Reply NO to customize",

        //    4:"Is 2PM a good default event time? This is so that you can just add an event without a time and the default time will be added to your date for reminders, if no other time is specified",
        //    5:"Are these default reminder times alright? 1 hour before, 3 hours before, and 24 hours before?"
        //};

        public InitializationController(Models.UserDetailsModel user)
        {
            int progress = CheckProgress();
        }

        public int CheckProgress()
        {
            return 0;
        }

        public void SetProgress()
        {

        }

        public string Start()
        {
            return "?";
        }
        
        public string ContinueInit(int progressState)
        {
            if (progressState == 0)
            {
                return Start();
            }

            return questions[progressState];
        }
    }
}