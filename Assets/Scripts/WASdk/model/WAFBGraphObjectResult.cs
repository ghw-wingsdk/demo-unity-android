using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WAFBGraphObjectResult : WAResult
    {
        public List<WAFBGraphObject> objects
        {
            get;
            set;
        }

        public void addObject(WAFBGraphObject obj)
        {
            if(null == objects)
            {
                objects = new List<WAFBGraphObject>();
            }
            objects.Add(obj);
        }
    }
}
