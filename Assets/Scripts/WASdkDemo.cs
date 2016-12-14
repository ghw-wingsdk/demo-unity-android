using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WASdkAPI;

public abstract class WASdkDemo
{

    public const int TOAST_LENGTH_SHORT = 0;

    public const int TOAST_LENGTH_LONG = 1;

    public const string SP_KEY_ENABLE_LOGCAT = "sp_enable_logcat";

    public const string SP_KEY_ENABLE_APW = "sp_enable_apw";

    public const string SP_KEY_REBIND_DEVICE_ON_LOGIN = "sp_rebind_device_on_login";

    public const string SP_KEY_ENABLE_LOGIN_CACHE = "sp_enable_login_cache";

    private static WASdkDemo _instance = null;

    protected WALoginResult loginResult;

    /// <summary>
    /// 获取WA SDK Core代理单例
    /// </summary>
    public static WASdkDemo Instance
    {
        get
        {
            if (_instance == null)
            {
#if UNITY_EDITOR || UNITY_STANDLONE
                _instance = new WASdkDemo4Default();
                Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WASdkDemo4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
            }

            return _instance;
        }
    }

    /// <summary>
    /// 显示短Toast
    /// </summary>
    /// <param name="msg"></param>
    public abstract void showShortToast(string msg);

    /// <summary>
    /// 显示长Toast
    /// </summary>
    /// <param name="msg"></param>
    public abstract void showLongToast(string msg);

    /// <summary>
    /// 显示Loading
    /// </summary>
    /// <param name="msg"></param>
    public abstract void showLoading(string msg);

    /// <summary>
    /// 隐藏Loading
    /// </summary>
    public abstract void dismissLoading();

    //public abstract void 

    /// <summary>
    /// 获取登录结果
    /// </summary>
    /// <returns></returns>
    public WALoginResult getLoginResult()
    {
        return loginResult;
    }

    /// <summary>
    /// 设置登录结果数据
    /// </summary>
    /// <param name="loginResult"></param>
    public void setLoginResult(WALoginResult loginResult)
    {
        this.loginResult = loginResult;
    }

    public void logout()
    {
        WAUserProxy.Instance.logout();
        loginResult = null;
    }

    /// <summary>
    /// 查询库存结果
    /// </summary>
    /// <param name="result">库存信息</param>
    public abstract void onSkuResult(WASkuResult result);

    /// <summary>
    /// 查询可邀请好友列表结果
    /// </summary>
    /// <param name="result"></param>
    public abstract void onInvitableFriendsResult(string platform, WAFriendsResult result);

    /// <summary>
    /// 查询收到的索要礼物请求结果
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    public abstract void onQueryGiftRequestResult(string platform, string type, WAFBGameRequestResult result);

    /// <summary>
    /// 礼物查询好友列表结果
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="result"></param>
    public abstract void onQueryGiftFriendsResult(string platform, string type, WAFriendsResult result);

    /// <summary>
    /// 从配置文件中读取一个bool类型的配置数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public abstract bool getBoolFromConfig(string key, bool defaultValue);

    /// <summary>
    /// 设置配置文件一个bool类型的配置数据
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public abstract bool setConfigBoolValue(string key, bool value);



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// VK Group ///////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public const int TYPE_GROUP_SEARCH = 1;
    public const int TYPE_GROUP_APP_LINKED = 2;
    public const int TYPE_GROUP_JOINED = 3;

    /// <summary>
    /// Group结果
    /// </summary>
    /// <param name="result"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    public abstract void onGroupResult(int type, WAGroupResult result);

    /// <summary>
    /// 是否为Group的成员结果
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isMember"></param>
    public abstract void onIsGroupMemberResult(string id, bool isMember);


    /// <summary>
    /// 检车热更新
    /// </summary>
    /// <param name="result"></param>
    public abstract void onCheckHupUpdateResult(WACheckUpdateResult result);

    /// <summary>
    /// 获取app下载链接
    /// </summary>
    /// <param name="result"></param>
    public abstract void onGetAppUpdateLinkResult(string result);
}
