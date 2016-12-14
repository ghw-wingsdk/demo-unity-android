using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    class WAUserProxy4Default : WAUserProxy
    {
        public override void bindingAccount(string platform, string extInfo, WABindCallback callback)
        {
            throw new NotImplementedException();
        }

        public override void clearLoginCache()
        {
            throw new NotImplementedException();
        }

        public override void createNewAccount(WACallback<WALoginResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void getAccountInfo(string platform, WACallback<WAUser> callback)
        {
            throw new NotImplementedException();
        }

        public override void login(string platform, WACallback<WALoginResult> callback, string extInfo)
        {
            throw new NotImplementedException("Not supported");
        }

        public override void loginUI(bool enableCache, WACallback<WALoginResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void logout()
        {
            throw new NotImplementedException();
        }

        public override void openAccountManager(WAAccountCallback callback)
        {
            throw new NotImplementedException();
        }

        public override void queryBoundAccount(WACallback<WAAccountResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void setLoginFlowType(int flowType)
        {
            throw new NotImplementedException();
        }

        public override void switchAccount(string platform, WACallback<WALoginResult> callback)
        {
            throw new NotImplementedException();
        }

        public override void unBindAccount(string platform, string platformUserId, WACallback<WAResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}
