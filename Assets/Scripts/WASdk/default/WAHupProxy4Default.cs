using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    class WAHupProxy4Default : WAHupProxy
    {
        public override void checkUpdate(WACallback<WACheckUpdateResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void startUpdate(WACheckUpdateResult updateInfo, WACallback<string> callback)
        {
            throw new NotImplementedException();
        }
    }
}
