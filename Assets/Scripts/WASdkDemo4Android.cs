using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using WASdkAPI;

class WASdkDemo4Android : WASdkDemo
{

    private const string CONFIG_SP_FILE = "wa_demo_config";

    private AndroidJavaClass androidJavaClass = null;
    private AndroidJavaObject configSP = null;
    

    public WASdkDemo4Android()
    {
        androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass spHelperClass = new AndroidJavaClass("com.wa.sdk.common.WASharedPrefHelper");
        configSP = spHelperClass.CallStatic<AndroidJavaObject>("newInstance", activity, CONFIG_SP_FILE);
    }

    /// <summary>
    /// 显示短Toast
    /// </summary>
    /// <param name="msg"></param>
    public override void showShortToast(string msg)
    {
        if (null != androidJavaClass)
        {
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", activity, msg, TOAST_LENGTH_SHORT);
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                toastObject.Call("show");
            }));
        }
    }

    /// <summary>
    /// 显示长Toast
    /// </summary>
    /// <param name="msg"></param>
    public override void showLongToast(string msg)
    {
        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", activity, msg, TOAST_LENGTH_LONG);
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            toastObject.Call("show");
        }));
    }



    AndroidJavaObject loadingObject;
    AndroidJavaClass loadingClass = new AndroidJavaClass("com.ghw.sdk2.widget.LoadingDialog");
    /// <summary>
    /// 显示Loading
    /// </summary>
    /// <param name="msg"></param>
    public override void showLoading(string msg)
    {
        
        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            if (null != loadingObject && loadingObject.Call<bool>("isShowing"))
            {
                loadingObject.Call("setMessage", msg);
                return;
            }
            loadingObject = loadingClass.CallStatic<AndroidJavaObject>("showLoadingDialog", activity, msg);
        }));
    }

    /// <summary>
    /// 隐藏Loading
    /// </summary>
    public override void dismissLoading()
    {
        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            if (null != loadingObject && loadingObject.Call<bool>("isShowing"))
            {

                loadingObject.Call("dismiss");
                loadingObject = null;
            }
        }));
    }

    /// <summary>
    /// 从配置文件中读取一个bool类型的配置数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public override bool getBoolFromConfig(string key, bool defaultValue)
    {
        if(null == configSP)
        {
            return defaultValue;
        }
        return configSP.Call<bool>("getBoolean", key, defaultValue);
    }

    /// <summary>
    /// 设置配置文件一个bool类型的配置数据
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public override bool setConfigBoolValue(string key, bool value)
    {
        if(null == configSP)
        {
            return false;
        }
        return configSP.Call<bool>("saveBoolean", key, value);
    }

    static string[] dialogItems;

    /// <summary>
    /// 查询库存结果
    /// </summary>
    /// <param name="result">库存信息</param>
    public override void onSkuResult(WASkuResult result)
    {
        if(null == result)
        {
            WASdkDemo.Instance.showShortToast("没有查询到库存信息");
            return;
        }
        List<WASkuDetails> skus = result.skuList;
        if (null == skus || skus.Count == 0)
        {
            WASdkDemo.Instance.showShortToast("没有查询到库存信息");
            return;
        }

        WASkuDetails[] skuArray = skus.ToArray();
        dialogItems = new string[skuArray.Length];
        for(int i = 0; i < skuArray.Length; i++)
        {
            dialogItems[i] = skuArray[i].sku;
        }

        new AlertDialog4Android.Builder()
            .setTitle("购买商品")
            .setItems(dialogItems, new SkuOnClickListener())
            .setNegativeButton("取消", new SkuOnClickListener())
            .show();
        
    }


    static WAUser[] friendsArray;
    static WAUser user;
    static Dictionary<string, WAUser> userDictionary;
    /// <summary>
    /// 查询可邀请好友列表结果
    /// </summary>
    /// <param name="result"></param>
    public override void onInvitableFriendsResult(string platform, WAFriendsResult result)
    {
        friendsArray = null;
        user = null;
        userDictionary = null;
        if (null == result)
        {
            WASdkDemo.Instance.showShortToast("没有查询到可邀请的好友");
            return;
        }
        List<WAUser> friends = result.friends;
        if (null == friends || friends.Count == 0)
        {
            WASdkDemo.Instance.showShortToast("没有查询到可邀请的好友");
            return;
        }

        friendsArray = friends.ToArray();
        dialogItems = new string[friendsArray.Length];
        for (int i = 0; i < friendsArray.Length; i++)
        {
            dialogItems[i] = friendsArray[i].name;
        }

        if(WAChannel.CHANNEL_VK.Equals(platform)) {
            new AlertDialog4Android.Builder()
                .setTitle("选择好友")
                .setSingleChoiceItems(dialogItems, -1, new VKInviteOnClickListener())
                .setPositiveButton("邀请", new VKInviteOnClickListener())
                .setNegativeButton("取消", new VKInviteOnClickListener())
                .show();
        }
        else if(WAChannel.CHANNEL_FACEBOOK.Equals(platform))
        {
            new AlertDialog4Android.Builder()
                .setTitle("选择好友")
                .setMultiChoiceItems(dialogItems, null, new FriendsMultiClickListener())
                .setPositiveButton("邀请", new FBInviteOnClickListener())
                .setNegativeButton("取消", new FBInviteOnClickListener())
                .show();
        }
    }

    /// <summary>
    /// 礼物查询好友列表结果
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="result"></param>
    public override void onQueryGiftFriendsResult(string platform, string type, WAFriendsResult result)
    {
        friendsArray = null;
        user = null;
        userDictionary = null;
        if (null == result)
        {
            WASdkDemo.Instance.showShortToast("没有查询到应用好友");
            return;
        }
        List<WAUser> friends = result.friends;
        if (null == friends || friends.Count == 0)
        {
            WASdkDemo.Instance.showShortToast("没有查询到应用好友");
            return;
        }

        friendsArray = friends.ToArray();
        dialogItems = new string[friendsArray.Length];
        for (int i = 0; i < friendsArray.Length; i++)
        {
            dialogItems[i] = friendsArray[i].name;
        }

        if (WAChannel.CHANNEL_FACEBOOK.Equals(platform))
        {
            if(WARequestType.REQUEST_GIFT_SEND.Equals(type))
            {
                new AlertDialog4Android.Builder()
                    .setTitle("选择好友")
                    .setMultiChoiceItems(dialogItems, null, new FriendsMultiClickListener())
                    .setPositiveButton("赠送礼物", new GiftFriendsClickListener(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_SEND))
                    .setNegativeButton("取消", new GiftFriendsClickListener(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_SEND))
                    .show();
            }
            else if(WARequestType.REQUEST_GIFT_ASK.Equals(type))
            {
                new AlertDialog4Android.Builder()
                    .setTitle("选择好友")
                    .setMultiChoiceItems(dialogItems, null, new FriendsMultiClickListener())
                    .setPositiveButton("索要礼物", new GiftFriendsClickListener(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_ASK))
                    .setNegativeButton("取消", new GiftFriendsClickListener(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_ASK))
                    .show();
            }
            else
            {
                WASdkDemo.Instance.showShortToast("不支持的操作类型");
            }
        }
        //WAFBGraphObjectResult
    }

    /// <summary>
    /// 查询收到礼物请求结果(收到的礼物、索要礼物请求)
    /// </summary>
    /// <param name="platform"></param>
    /// <param name="type"></param>
    /// <param name="result"></param>
    public override void onQueryGiftRequestResult(string platform, string type, WAFBGameRequestResult result)
    {
        if(WAChannel.CHANNEL_FACEBOOK == platform)
        {
            if(WARequestType.REQUEST_GIFT_SEND.Equals(type))
            {
                if (null == result)
                {
                    WASdkDemo.Instance.showShortToast("没有收到任何礼物");
                    return;
                }
                List<WAFBGameRequestData> requests = result.requests;
                if (null == requests || requests.Count == 0)
                {
                    WASdkDemo.Instance.showShortToast("没有收到任何礼物");
                    return;
                }
                string[] items = new string[requests.Count];
                string item;
                WAUser from;
                int i = 0;
                foreach (var data in requests)
                {
                    item = "id: " + data.id;
                    item += "\nmessage: " + data.message;
                    from = data.from;
                    if (null != from)
                    {
                        item += "\n来自" + from.name + "的礼物 ";
                    }
                    WAFBGraphObject obj = data.obj;
                    if (null != obj)
                    {
                        item += obj.title;
                    }
                    items[i++] = item;
                }
                new AlertDialog4Android.Builder()
                    .setTitle("收到的礼物")
                    .setItems(items, new ReceivedGiftOnClickListener(requests))
                    .setPositiveButton("确定", new ReceivedGiftOnClickListener(requests))
                    .show();
            }
            else if(WARequestType.REQUEST_GIFT_ASK.Equals(type))
            {
                if (null == result)
                {
                    WASdkDemo.Instance.showShortToast("没有收到任何索要礼物请求");
                    return;
                }
                List<WAFBGameRequestData> requests = result.requests;
                if (null == requests || requests.Count == 0)
                {
                    WASdkDemo.Instance.showShortToast("没有收到任何索要礼物请求");
                    return;
                }

                string[] items = new string[requests.Count];
                string item;
                WAUser from;
                int i = 0;
                foreach (var data in requests)
                {
                    item = "id: " + data.id;
                    item += "\nmessage: " + data.message;
                    from = data.from;
                    if (null != from)
                    {
                        item += "\n" + from.name + "向你索要礼物 ";
                        WAFBGraphObject obj = data.obj;
                        if(null != obj)
                        {
                            item += obj.title;
                        }
                    }
                    items[i++] = item;
                }
                new AlertDialog4Android.Builder()
                    .setTitle("收到的索要礼物请求")
                    .setItems(items, new AskforGiftOnClickListener(requests))
                    .setPositiveButton("确定", new AskforGiftOnClickListener(requests))
                    .show();
            }
        }
        
    }

    /// <summary>
    /// 库存点击事件
    /// </summary>
    class SkuOnClickListener : AlertDialog4Android.OnClickListener
    {
        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if(which < 0)
            {
                return;
            }
            if(null == dialogItems || which >= dialogItems.Length)
            {
                return;
            }

            string sku = dialogItems[which];
            WAPayProxy.Instance.payUI(sku, "", new PayCallback());
        }
    }

    /// <summary>
    /// VK邀请对话框点击监听
    /// </summary>
    class VKInviteOnClickListener : AlertDialog4Android.OnClickListener
    {
        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (which < 0)
            {
                if(AlertDialog4Android.BUTTON_POSITIVE == which)
                {
                    if (null == user)
                    {
                        WASdkDemo.Instance.showShortToast("请选择邀请的好友");
                        return;
                    }
                    List<string> ids = new List<string>();
                    ids.Add(user.id);
                    // VK只能用发送普通请求来测试邀请
                    WASdkDemo.Instance.showLoading("加载中");
                    WASocialProxy.Instance.sendRequest(WAChannel.CHANNEL_VK, WARequestType.REQUEST_REQUEST, 
                        "Help", "Help me", "", ids, new RequestSendCallback(WAChannel.CHANNEL_VK, WARequestType.REQUEST_REQUEST), "");
                }
                else if(AlertDialog4Android.BUTTON_NEGATIVE == which)
                {
                    user = null;
                }
                return;
            }
            if (null == dialogItems || which >= dialogItems.Length)
            {
                return;
            }
            user = friendsArray[which];
        }
    }

    /// <summary>
    /// FB邀请多选点击监听
    /// </summary>
    class FriendsMultiClickListener : AlertDialog4Android.OnMultiChoiceClickListener
    {
        public override void onClick(AndroidJavaObject dialog, int which, bool isChecked)
        {
            if(null == userDictionary)
            {
                userDictionary = new Dictionary<string, WAUser>();
            }
            WAUser waUser = friendsArray[which];
            if(isChecked && !userDictionary.ContainsKey(waUser.id))
            {
                userDictionary.Add(waUser.id, waUser);
            } else if(userDictionary.ContainsKey(waUser.id))
            {
                userDictionary.Remove(waUser.id);
            }
        }
    }

    /// <summary>
    /// FB邀请按钮点击事件监听
    /// </summary>
    class FBInviteOnClickListener : AlertDialog4Android.OnClickListener
    {
        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                if(null == userDictionary || userDictionary.Count == 0)
                {
                    WASdkDemo.Instance.showShortToast("请选择邀请的好友");
                    return;
                }
                List<string> ids = new List<string>();
                foreach (var item in userDictionary)
                {
                    // key 就是id
                    ids.Add(item.Key);
                }
                WASdkDemo.Instance.showLoading("加载中");
                WASocialProxy.Instance.sendRequest(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_INVITE,
                    "Help", "Help me", "", ids, new RequestSendCallback(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_INVITE), "");
            }
            else if (AlertDialog4Android.BUTTON_NEGATIVE == which)
            {
                userDictionary = null;
            }
        }
    }


    /// <summary>
    /// 支付结果回调
    /// </summary>
    class PayCallback : WACallback<WAPurchaseResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast("购买取消");
        }

        public override void onError(int code, string message, WAPurchaseResult result)
        {
            WASdkDemo.Instance.showShortToast("购买失败\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAPurchaseResult result)
        {
            WASdkDemo.Instance.showShortToast("购买成功\ncode: " + code + "\nmessage: " + message);
        }
    }

    /// <summary>
    /// 请求发送回调
    /// </summary>
    class RequestSendCallback : WACallback<WARequestSendResult>
    {
        string platform;
        string requestType;

        public RequestSendCallback(string platform, string requestType)
        {
            this.platform = platform;
            this.requestType = requestType;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast(platform + " -- " + requestType + " 请求发送取消");
            WASdkDemo.Instance.dismissLoading();
        }

        public override void onError(int code, string message, WARequestSendResult result)
        {
            WASdkDemo.Instance.showShortToast(platform + " -- " + requestType + " 请求发送失败： \ncode: " + code + "\nmessage: " + message);
            WASdkDemo.Instance.dismissLoading();
        }

        public override void onSuccess(int code, string message, WARequestSendResult result)
        {
            WASdkDemo.Instance.showShortToast(platform + " -- " + requestType + " 请求发送成功： \ncode: " + code + "\nmessage: " + message 
                + (null == result ? "" : ("\nrequestId: " + result.requestId)));
            WASdkDemo.Instance.dismissLoading();
        }
    }



    /// <summary>
    /// 赠送礼物好友选择对话框点击事件
    /// </summary>
    class GiftFriendsClickListener : AlertDialog4Android.OnClickListener
    {
        string platform;
        string type = WARequestType.REQUEST_GIFT_SEND;

        public GiftFriendsClickListener(string platform, string type)
        {
            this.platform = platform;
            this.type = type;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if(AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                // 选择了好友，并且点击了“确定”按钮
                if (WARequestType.REQUEST_GIFT_SEND.Equals(type))
                {
                    if (null == userDictionary || userDictionary.Count == 0)
                    {
                        WASdkDemo.Instance.showShortToast("请选择赠送礼物的好友");
                        return;
                    }
                    WASdkDemo.Instance.showLoading("加载中");
                    if(WAChannel.CHANNEL_FACEBOOK.Equals(platform))
                    {
                        // 开始查询礼物列表
                        giftArray = null;
                        WASocialProxy.Instance.queryFBGraphObjects("com_ghw_sdk:gift", new QueryFBGiftCallback(type));
                    }
                    else
                    {
                        WASdkDemo.Instance.showShortToast("不支持的平台： " + platform);
                    }
                }
                else if (WARequestType.REQUEST_GIFT_ASK.Equals(type))
                {
                    if (null == userDictionary || userDictionary.Count == 0)
                    {
                        WASdkDemo.Instance.showShortToast("请选择索要礼物的好友");
                        return;
                    }
                    WASdkDemo.Instance.showLoading("加载中");
                    if (WAChannel.CHANNEL_FACEBOOK.Equals(platform))
                    {
                        // 开始查询礼物列表
                        giftArray = null;
                        gift = null;
                        WASocialProxy.Instance.queryFBGraphObjects("com_ghw_sdk:gift", new QueryFBGiftCallback(type));
                    }
                    else
                    {
                        WASdkDemo.Instance.showShortToast("不支持的平台： " + platform);
                    }
                }
                else
                {
                    WASdkDemo.Instance.showShortToast("不支持的操作类型");
                }
                
            }
            else if (AlertDialog4Android.BUTTON_NEGATIVE == which)
            {
                userDictionary = null;
            }
        }
    }

    /// <summary>
    /// 收到的礼物对话框点击事件
    /// </summary>
    class ReceivedGiftOnClickListener : AlertDialog4Android.OnClickListener
    {

        List<WAFBGameRequestData> requests;

        public ReceivedGiftOnClickListener(List<WAFBGameRequestData> requests)
        {
            this.requests = requests;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (which < 0)
            {
                return;
            }
            if(null == requests || requests.Count == 0 || requests.Count <= which)
            {
                return;
            }
            WAFBGameRequestData request = requests[which];
            if(null == request)
            {
                return;
            }
            WAUser from = request.from;
            WAFBGraphObject obj = request.obj;
            new AlertDialog4Android.Builder()
                    .setTitle("收取礼物")
                    .setMessage("提取来自" + (null == from ? " 未知 " : " " + from.name + " 的礼物 ") + (null == obj ? "" : obj.title))
                    .setPositiveButton("提取", new GetGiftListener(request))
                    .setNegativeButton("取消", new GetGiftListener(request))
                    .show();
        }
    }

    /// <summary>
    /// 收取礼物对话框点击事件监听（查看收到的礼物列表时，弹出收取的对话框点击）
    /// </summary>
    class GetGiftListener : AlertDialog4Android.OnClickListener
    {
        WAFBGameRequestData request;

        public GetGiftListener(WAFBGameRequestData request)
        {
            this.request = request;
        }
        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if(AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                Instance.showLoading("正在处理");
                WASocialProxy.Instance.fbDeleteRequest(request.id, new DeleteRequestCallback());
            }
        }
    }


    /// <summary>
    /// 删除请求回调
    /// </summary>
    class DeleteRequestCallback : WACallback<WAResult>
    {
        public override void onCancel()
        {
            Instance.dismissLoading();
            Instance.showShortToast("操作取消");
        }

        public override void onError(int code, string message, WAResult result)
        {
            Instance.dismissLoading();
            Instance.showShortToast("操作错误: \ncode: " + code + "\nmessage: " + message);

        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            Instance.dismissLoading();
            Instance.showShortToast("操作成功");

        }
    }

    /// <summary>
    /// 收到的索要礼物请求对话框点击事件
    /// </summary>
    class AskforGiftOnClickListener : AlertDialog4Android.OnClickListener
    {
        List<WAFBGameRequestData> requests;

        public AskforGiftOnClickListener(List<WAFBGameRequestData> requests)
        {
            this.requests = requests;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (which < 0)
            {
                return;
            }
            WAFBGameRequestData request = requests[which];
            if (null == request)
            {
                return;
            }
            WAUser from = request.from;
            WAFBGraphObject obj = request.obj;
            new AlertDialog4Android.Builder()
                    .setTitle("处理索要礼物请求")
                    .setMessage((null == from ? " 未知 " : " " + from.name + " 向你索要的礼物 ") + (null == obj ? "" : obj.title) + ",是否同意赠送?")
                    .setPositiveButton("同意", new SendGiftListener(request))
                    .setNegativeButton("取消", new SendGiftListener(request))
                    .show();
        }
    }

    /// <summary>
    /// 赠送礼物给好友对话框监听（处理索要礼物请求对话框）
    /// </summary>
    class SendGiftListener : AlertDialog4Android.OnClickListener
    {
        WAFBGameRequestData request;

        public SendGiftListener(WAFBGameRequestData request)
        {
            this.request = request;
        }
        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                Instance.showLoading("正在处理");
                WAUser from = request.from;
                WAUser to = request.to;
                WAFBGraphObject obj = request.obj;
                if(null == from || null == to || null == obj)
                {
                    return;
                }
                List<string> reciepts = new List<string>();
                reciepts.Add(from.id);
                WASocialProxy.Instance.sendRequest(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_SEND, "赠送礼物", 
                    to.name + " 同意你的礼物索要请求", obj.id, reciepts, new SendGiftCallback(request), "");
            }
        }
    }

    /// <summary>
    /// 赠送礼物回调（处理礼物索要请求的时候同意赠送回调）
    /// </summary>
    class SendGiftCallback : WACallback<WARequestSendResult>
    {
        WAFBGameRequestData request;

        public SendGiftCallback(WAFBGameRequestData request)
        {
            this.request = request;
        }

        public override void onCancel()
        {
            Instance.showShortToast("操作取消");
        }

        public override void onError(int code, string message, WARequestSendResult result)
        {
            Instance.showShortToast("操作错误: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WARequestSendResult result)
        {
            Instance.showShortToast("操作成功");
            Instance.showLoading("正在处理");
            WASocialProxy.Instance.fbDeleteRequest(request.id, new DeleteRequestCallback());
        }
    }


    static WAFBGraphObject[] giftArray;
    /// <summary>
    /// 查询Facebook礼物回调
    /// </summary>
    class QueryFBGiftCallback : WACallback<WAFBGraphObjectResult>
    {
        string type = WARequestType.REQUEST_GIFT_SEND;

        public QueryFBGiftCallback(string type)
        {
            this.type = type;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query Facebook gift onCancel");
        }

        public override void onError(int code, string message, WAFBGraphObjectResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query Facebook gift onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAFBGraphObjectResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query Facebook gift onSuccess: " + message);
            if(null == result)
            {
                WASdkDemo.Instance.showShortToast("没有礼物可操作");
                return;
            }
            List<WAFBGraphObject> gifts = result.objects;
            if (null == gifts || gifts.Count == 0)
            {
                WASdkDemo.Instance.showShortToast("没有礼物可操作");
                return;
            }

            giftArray = gifts.ToArray();

            string[] giftItems = new string[giftArray.Length];
            for (int i = 0; i < giftArray.Length; i++)
            {
                giftItems[i] = giftArray[i].id + "\n" + giftArray[i].title;
            }
            // 弹出选择礼物对话框
            new AlertDialog4Android.Builder()
                .setTitle("选择礼物")
                .setSingleChoiceItems(giftItems, -1, new OnFBGiftClickListener(type))
                .setPositiveButton("确定", new OnFBGiftClickListener(type))
                .setNegativeButton("取消", new OnFBGiftClickListener(type))
                .show();
        }
    }

    static WAFBGraphObject gift;

    /// <summary>
    /// 赠送礼物好友选择对话框点击事件
    /// </summary>
    class OnFBGiftClickListener : AlertDialog4Android.OnClickListener
    {
        string type = WARequestType.REQUEST_GIFT_SEND;

        public OnFBGiftClickListener(string type)
        {
            this.type = type;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if (AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                if (WARequestType.REQUEST_GIFT_SEND.Equals(type))
                {
                    if (null == gift)
                    {
                        WASdkDemo.Instance.showShortToast("请选择礼物");
                        return;
                    }
                    if (null == userDictionary || userDictionary.Count == 0)
                    {
                        WASdkDemo.Instance.showShortToast("请选择赠送礼物的好友");
                        return;
                    }
                    List<string> ids = new List<string>();
                    foreach (var item in userDictionary)
                    {
                        // key 就是id
                        ids.Add(item.Key);
                    }
                    WASdkDemo.Instance.showLoading("加载中");
                    WASocialProxy.Instance.sendRequest(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_SEND,
                        "Help", "Help me", gift.id, ids, new RequestSendCallback(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_SEND), "");
                }
                else if (WARequestType.REQUEST_GIFT_ASK.Equals(type))
                {
                    if (null == gift)
                    {
                        WASdkDemo.Instance.showShortToast("请选择礼物");
                        return;
                    }
                    if (null == userDictionary || userDictionary.Count == 0)
                    {
                        WASdkDemo.Instance.showShortToast("请选择索要礼物的好友");
                        return;
                    }
                    List<string> ids = new List<string>();
                    foreach (var item in userDictionary)
                    {
                        // key 就是id
                        ids.Add(item.Key);
                    }
                    WASdkDemo.Instance.showLoading("加载中");
                    WASocialProxy.Instance.sendRequest(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_ASK,
                        "Help", "Help me", gift.id, ids, new RequestSendCallback(WAChannel.CHANNEL_FACEBOOK, WARequestType.REQUEST_GIFT_ASK), "");
                }
                else
                {
                    WASdkDemo.Instance.showShortToast("不支持的操作类型");
                }

            }
            else if (AlertDialog4Android.BUTTON_NEGATIVE == which)
            {
                userDictionary = null;
                gift = null;
            } else if(which >= 0)
            {
                if(null != giftArray && giftArray.Length > which)
                {
                    gift = giftArray[which];
                }
                else
                {
                    WASdkDemo.Instance.showShortToast("礼物列表数据错误，请重试");
                }
                
            }
        }
    }


    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// VK Group ///////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Group结果
    /// </summary>
    /// <param name="type"></param>
    /// <param name="result"></param>
    public override void onGroupResult(int type, WAGroupResult result)
    {
        if(null == result)
        {
            Instance.showShortToast("没有数据");
            return;
        }
        Dictionary<string, WAGroup> groups = result.groups;
        
        if(null == groups || groups.Count == 0)
        {
            Instance.showShortToast("没有数据");
            return;
        }
        string[] items = new string[groups.Count];
        string itemText;
        int i = 0;
        List<WAGroup> groupsList = new List<WAGroup>();
        foreach(var item in groups) {
            WAPlace place = item.Value.place;
            itemText = "名称：" + item.Value.name
                + "\nID：" + item.Value.gid 
                + "\n成员数：" + item.Value.members_count
                + "\n地区：" + (null == place ? "" : place.title)
                + "\n简介：" + item.Value.description;
            items[i] = itemText;
            groupsList.Add(item.Value);
            i++;
        }
        switch(type)
        {
            case TYPE_GROUP_SEARCH:
                new AlertDialog4Android.Builder()
                    .setTitle("Group查询结果")
                    .setItems(items, new GroupClickListener(groupsList))
                    .setPositiveButton("确定", new GroupClickListener(groupsList))
                    .show();
                break;
            default:
                break;
        }
    }

    class GroupClickListener : AlertDialog4Android.OnClickListener
    {
        List<WAGroup> groups;

        public GroupClickListener(List<WAGroup> groups)
        {
            this.groups = groups;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if(which < 0)
            {
                return;
            }
            if(null == groups || groups.Count == 0 || groups.Count <= which)
            {
                return;
            }
            WAGroup group = groups[which];
            WASocialProxy.Instance.openGroupPage(WAChannel.CHANNEL_VK, group.screen_name, "");
        }
    }





    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////// 热更新 ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 是否为Group的成员结果
    /// </summary>
    /// <param name="isMember"></param>
    public override void onIsGroupMemberResult(string id, bool isMember)
    {
        new AlertDialog4Android.Builder()
                    .setTitle("Group查询结果")
                    .setMessage((isMember ? "您是Group（" : "您不是Group（") + id + "）的成员")
                    .setPositiveButton("确定", null)
                    .show();
    }


    /// <summary>
    /// //////////////////////////
    /// </summary>

    static WACheckUpdateResult updateResult;
    /// <summary>
    /// 检车热更新
    /// </summary>
    /// <param name="result"></param>
    public override void onCheckHupUpdateResult(WACheckUpdateResult result)
    {

        if(null == result)
        {
            WASdkDemo.Instance.showShortToast("Check hot update onSuccess: result is null!");
            return;
        }
        updateResult = result;
        WASdkDemo.Instance.showShortToast("Check hot update onSuccess: " + result.message);

        AlertDialog4Android.Builder builder =  new AlertDialog4Android.Builder();
        builder.setTitle("检查热更新");
        if (result.hasUpdate)
        {
            builder.setMessage("检查到新版本" + result.patchVersion + "，是否更新？");
            builder.setPositiveButton("确定", new UpdateDialogListener(1));
            builder.setNegativeButton("取消", new UpdateDialogListener(1));
        }
        else
        {
            builder.setPositiveButton("确定", new UpdateDialogListener(2));
        }
        builder.show();
    }



    /// <summary>
    /// 获取app下载链接
    /// </summary>
    /// <param name="result"></param>
    public override void onGetAppUpdateLinkResult(string result)
    {
        if (null == result || result.Length == 0)
        {
            WASdkDemo.Instance.showShortToast("Get app update link fained: no link found");
            return;
        }

        WASdkDemo.Instance.showShortToast("Get app update link onSuccess");
        new AlertDialog4Android.Builder()
        .setTitle("获取App更新链接")
        .setMessage("App更新链接： " + result)
        .setPositiveButton("确定", new UpdateDialogListener(3))
        .show();
    }

    class UpdateDialogListener : AlertDialog4Android.OnClickListener
    {
        private int type;

        public UpdateDialogListener(int type)
        {
            this.type = type;
        }

        public override void onClick(AndroidJavaObject dialog, int which)
        {
            if(AlertDialog4Android.BUTTON_POSITIVE == which)
            {
                if (type == 1)
                {
                    // 下载patch
                    WASdkDemo.Instance.showShortToast("开始更新");
                    WAHupProxy.Instance.startUpdate(updateResult, new UpdateCallback());
                }
                else if (type == 2)
                {

                }
                else if (type == 3)
                {

                }
            }
        }
    }



    class UpdateCallback : WACallback<string>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast("Hot update onCancel");
        }

        public override void onError(int code, string message, string result)
        {
            WASdkDemo.Instance.showShortToast("Hot update  onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, string result)
        {
            if (null == result || result.Length == 0)
            {
                WASdkDemo.Instance.showShortToast("Hot update fained");
                return;
            }
            WASdkDemo.Instance.showShortToast("Hot update onSuccess");
            new AlertDialog4Android.Builder()
                .setTitle("Patch文件更新完成")
                .setMessage("patch文件地址： " + result)
                .setPositiveButton("确定", new UpdateDialogListener(4))
                .show();
        }
    }
}
