using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    class WACommonProxy4Default : WACommonProxy
    {
        public override void checkSelfPermission(string permission, WAPermissionCallback callback)
        {
            throw new NotImplementedException();
        }

        public override void disableLogcat()
        {
            throw new NotImplementedException();
        }

        public override void enableLogcat()
        {
            throw new NotImplementedException();
        }

        public override void getAppUpdateLink(WACallback<string> callback)
        {
            throw new NotImplementedException();
        }

        public override void log(string tag, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
