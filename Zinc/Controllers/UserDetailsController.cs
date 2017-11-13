using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zinc.Models;

namespace Zinc.Controllers
{
    public class UserDetailsController
    {
        private UserDetailsModel userModel;

        public UserDetailsController(UserDetailsModel userModel)
        {
            this.userModel = userModel;
        }

        /*
        parse the message first, then load the user details
        only load the user details that you need
        if user is scheduling something, you don't need to load everything, 
        same as if they're just snoozing, etc

        */

        public UserDetailsModel GetAllUserDetails()
        {
            //search db for current user(phone number lookup)
            //get all their details

            //go through UserDetailsCOntroller
            if (userModel.Number == "4145200673")
            {
                userModel.Name = "Keith";
                userModel.Groups = "";
            }
            else
            {
                userModel.Name = "Unknown";
                userModel.Number = "555";
            }

            return userModel;
        }

        public void GetField(string field)
        {

        }

        public void GetUserGroups()
        {

        }

        public void Snooze()
        {

        }

        public void UnSnooze()
        {

        }
    }
}