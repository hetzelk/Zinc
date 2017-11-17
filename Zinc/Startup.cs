using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Zinc.Controllers;
using System.Threading;

[assembly: OwinStartup(typeof(Zinc.Startup))]

namespace Zinc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            checkReminders();
        }
        public void checkReminders()
        {
            ReminderChecker checker = new ReminderChecker();
            while(true)//while running
            {
                var now = DateTime.Now;
                checker.GetDynamoReminders(now);
                Thread.Sleep(60000);
            }
        }
    }
}
