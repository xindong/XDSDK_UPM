using System;
using System.Collections.Generic;

namespace xdsdk.Unity
{
    public class User
    {
        public User()
        {
        }

        private string id;
        private string name;
        private string friendlyName;
        private long authorizationState;
        private bool safety;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string FriendlyName
        {
            get
            {
                return friendlyName;
            }

            set
            {
                friendlyName = value;
            }
        }

        public long AuthorizationState
        {
            get
            {
                return authorizationState;
            }

            set
            {
                authorizationState = value;
            }
        }

        public bool Safety
        {
            get
            {
                return safety;
            }

            set
            {
                safety = value;
            }
        }

        public static User InitWithDict(Dictionary<string, object> dict){
            User user = new User();
            if (dict.ContainsKey("id"))
            {
                user.Id = (string)dict["id"];
            }
            if (dict.ContainsKey("name"))
            {
                user.Name = (string)dict["name"];
            }
            if (dict.ContainsKey("id"))
            {
                user.FriendlyName = (string)dict["friendly_name"];
            }
            if (dict.ContainsKey("authoriz_state"))
            {
                user.AuthorizationState = (long)dict["authoriz_state"];
            }
            if (dict.ContainsKey("safety"))
            {
                user.Safety = (bool)dict["safety"];
            }
            return user;
        }
    }
}
