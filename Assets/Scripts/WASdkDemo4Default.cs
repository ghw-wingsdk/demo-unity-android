using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WASdkAPI;

class WASdkDemo4Default : WASdkDemo
{
    public override bool getBoolFromConfig(string key, bool defaultValue)
    {
        throw new NotImplementedException();
    }

    public override bool setConfigBoolValue(string key, bool value)
    {
        throw new NotImplementedException();
    }

    public override void showLoading(string msg)
    {
        throw new NotImplementedException();
    }

    public override void dismissLoading()
    {
        throw new NotImplementedException();
    }


    public override void showLongToast(string msg)
    {
        throw new NotImplementedException();
    }

    public override void showShortToast(string msg)
    {
        throw new NotImplementedException();
    }

    public override void onSkuResult(WASkuResult result)
    {
        throw new NotImplementedException();
    }

    public override void onInvitableFriendsResult(string platform, WAFriendsResult result)
    {
        throw new NotImplementedException();
    }

    public override void onQueryGiftRequestResult(string platform, string type, WAFBGameRequestResult result)
    {
        throw new NotImplementedException();
    }

    public override void onQueryGiftFriendsResult(string platform, string type, WAFriendsResult result)
    {
        throw new NotImplementedException();
    }

    public override void onGroupResult(int type, WAGroupResult result)
    {
        throw new NotImplementedException();
    }

    public override void onIsGroupMemberResult(string id, bool isMember)
    {
        throw new NotImplementedException();
    }

    public override void onCheckHupUpdateResult(WACheckUpdateResult result)
    {
        throw new NotImplementedException();
    }

    public override void onGetAppUpdateLinkResult(string result)
    {
        throw new NotImplementedException();
    }
}
