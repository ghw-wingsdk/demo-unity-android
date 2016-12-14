using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WAFriendsResult : WAResult
    {
        public List<WAUser> friends
        {
            get;
            set;
        }

        public void addFriend(WAUser user)
        {
            if(null == friends)
            {
                friends = new List<WAUser>();
            }
            friends.Add(user);
        }
    }
}
