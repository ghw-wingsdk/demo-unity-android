using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WAGroupResult : WAResult
    {
        public Dictionary<string, WAGroup> groups
        {
            get;
            set;
        }

        public void addGroup(string key, WAGroup value)
        {
            if(null == groups)
            {
                groups = new Dictionary<string, WAGroup>();
            }
            groups.Add(key, value);
        }
    }
}
