using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    class WAPayProxy4Default : WAPayProxy
    {
        public override void initialize(WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }

        public override bool isPayServiceAvailable()
        {
            throw new NotImplementedException();
        }

        public override void onDestroy()
        {
            throw new NotImplementedException();
        }

        public override void payUI(string waProductId, string extInfo, WACallback<WAPurchaseResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void queryInventory(WACallback<WASkuResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}
