using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    internal class WASocialProxy4Android : WASocialProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass socialProxy = null;

        public WASocialProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            socialProxy = new AndroidJavaClass("com.wa.sdk.social.WASocialProxy");
        }

        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="shareContent">分享内容实体类</param>
        /// <param name="shareWithApi">是否通过api分享</param>
        /// <param name="extInfo">额外信息</param>
        /// <param name="callback">回调</param>
        public override void share(string platform, WAShareContent shareContent, bool shareWithApi, string extInfo, WACallback<WAShareResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAShareResult> onError = new OnError<WAShareResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            if(null == shareContent)
            {
                if(null != callback)
                {
                    callback.onError(WAStatusCode.CODE_ERROR, "ShareContent is null", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject shareContentObject = shareContent.toAndroidJavaObject();
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("share", activity, platform, shareContentObject, shareWithApi, extInfo, new InternalShareCallback(callback));
            }));
        }

        /// <summary>
        /// 应用邀请
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="appLinkUrl">应用app link</param>
        /// <param name="previewImageUrl">预览图片地址</param>
        /// <param name="callback">回调,结果返回</param>
        public override void appInvite(string platform, string appLinkUrl, string previewImageUrl, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("appInvite", activity, platform, appLinkUrl, previewImageUrl, new InternalAndroidSimpleCallback(callback));
            }));

        }

        /// <summary>
        /// 查询可邀请好友
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="duration">多长时间之内不再显示已邀请的好友,毫秒</param>
        /// <param name="callback">回调</param>
        public override void queryInvitableFriends(string platform, long duration, WACallback<WAFriendsResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAFriendsResult> onError = new OnError<WAFriendsResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("queryInvitableFriends", activity, platform, duration, new InternalQueryFriendsCallback(callback));
            }));
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="requestType">请求类型</param>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="objectId">请求携带Object的id</param>
        /// <param name="receiptIds">接收者id</param>
        /// <param name="callback">回调，结果返回</param>
        /// <param name="extInfo">额外信息</param>
        public override void sendRequest(string platform, string requestType, string title, string message, string objectId,
            List<string> receiptIds, WACallback<WARequestSendResult> callback, string extInfo)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WARequestSendResult> onError = new OnError<WARequestSendResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject receiptsObject = null;
                if (null != receiptIds)
                {
                    receiptsObject = new AndroidJavaObject("java.util.ArrayList");
                    foreach (string id in receiptIds)
                    {
                        receiptsObject.Call<bool>("add", id);
                    }
                }
                socialProxy.CallStatic("sendRequest", activity, platform, requestType, title, message, objectId, receiptsObject, new InternalSendRequestCallback(callback), extInfo);
            }));
        }

        /// <summary>
        /// 游戏邀请
        /// </summary>
        /// <param name="platform">平台类型</param>
        /// <param name="title">邀请标题</param>
        /// <param name="message">邀请消息</param>
        /// <param name="ids">接收者id</param>
        /// <param name="callback">回调，结果返回</param>
        /// <see cref="sendRequest(string, string, string, string, string, List{string}, WACallback{WARequestSendResult}, string)">代替</see>
        //public override void gameInvite(string platform, string title, string message, List<string> ids, WACallback<WAInviteResult> callback);

        /// <summary>
        /// 创建邀请信息接口，用户成功邀请好友后调用，仅仅游戏邀请可用
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="requestId">请求id</param>
        /// <param name="recipients">接收者id列表</param>
        /// <param name="callback">回调，结果返回</param>
        public override void createInviteRecord(string platform, string requestId, List<string> recipients, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject receiptsObject = null;
                if (null != recipients)
                {
                    receiptsObject = new AndroidJavaObject("java.util.ArrayList");
                    foreach (string id in recipients)
                    {
                        receiptsObject.Call<bool>("add", id);
                    }
                }
                socialProxy.CallStatic("createInviteRecord", platform, requestId, receiptsObject, new InternalAndroidSimpleCallback(callback));
            }));
        }



        /// <summary>
        /// 邀请安装奖励，检查用户是否经过好友邀请才使用<br></br>
        /// <p>注意：这个接口必须在用户登录并且选服后调用。每次选服后都进行一次调用，内部 有检查是否已经发送数据到服务器的逻辑。
        /// 该方法异步执行这个接口可以在平台登录后（并且选服后）调用，也可以在绑定平台的账户后（并且选服后）调用</p>
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public override void inviteInstallReward(string platform, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("inviteInstallReward", activity, platform, new InternalAndroidSimpleCallback(callback));
            }));
        }

        /// <summary>
        /// 事件触发邀请奖励，由特定的事件触发的奖励<br></br>
        /// <p>这个接口必须在用户登录并且选服后，设置了userId和serverId后调用。并且是用户受邀请玩游戏，
        /// 已经调用<see cref="inviteEventReward(string, string, WACallback{WAResult})"/>上报了服务器后，邀请者才能获取到这个接口产生的奖励。</p>
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="callback"> 回调，结果返回</param>
        public override void inviteEventReward(string platform, string eventName, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("inviteEventReward", platform, eventName, new InternalAndroidSimpleCallback(callback));
            }));
        }


        /// <summary>
        /// 查询好友列表，返回正在使用当前APP的好友列表
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public override void queryFriends(string platform, WACallback<WAFriendsResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAFriendsResult> onError = new OnError<WAFriendsResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("queryFriends", activity, platform, new InternalQueryFriendsCallback(callback));
            }));
        }

        /// <summary>
        /// 查询Facebook Graph Object
        /// </summary>
        /// <param name="objectType">OpenGraph Object类型</param>
        /// <param name="callback">回调，结果返回</param>
        public override void queryFBGraphObjects(string objectType, WACallback<WAFBGraphObjectResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAFBGraphObjectResult> onError = new OnError<WAFBGraphObjectResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("queryFBGraphObjects", activity, objectType, new InternalQueryFBGraphObjectsCallback(callback));
            }));
        }

        /// <summary>
        ///  Facebook赠送礼物
        /// </summary>
        /// <param name="title">请求对话框的标题</param>
        /// <param name="message">请求对话框内容</param>
        /// <param name="objectId">礼物Open Graph对象id</param>
        /// <param name="receipts">接收者Facebook openId</param>
        /// <param name="callback">回调，结果返回</param>
        /// <seealso cref="sendRequest(string, string, string, string, string, List, WACallback, string)"/>
        //public override void fbSendGift(string title, string message, string objectId,  List<string> receipts, WACallback<WAGiftingResult> callback);

        /// <summary>
        /// Facebook索要礼物
        /// </summary>
        /// <param name="title">请求对话框的标题</param>
        /// <param name="message">请求对话框内容</param>
        /// <param name="objectId">礼物Open Graph对象id</param>
        /// <param name="receipts">接收者Facebook openId</param>
        /// <param name="callback">回调，结果返回</param>
        /// <seealso cref="sendRequest(string, string, string, string, string, List{string}, WACallback{WARequestSendResult}, string)"/>
        //public override void fbAskForGift(string title, string message, string objectId, List<string> receipts, WACallback<WAGiftingResult> callback);


        /// <summary>
        /// Facebook查询收到的礼物
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void fbQueryReceivedGifts(WACallback<WAFBGameRequestResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAFBGameRequestResult> onError = new OnError<WAFBGameRequestResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("fbQueryReceivedGifts", activity, new InternalQueryFBGameRequestCallback(callback));
            }));
        }

        /// <summary>
        /// Facebook查询收到的索要礼物请求
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void fbQueryAskForGiftRequests(WACallback<WAFBGameRequestResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAFBGameRequestResult> onError = new OnError<WAFBGameRequestResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("fbQueryAskForGiftRequests", activity, new InternalQueryFBGameRequestCallback(callback));
            }));
        }

        /// <summary>
        /// Facebook删除请求（可删除各种Game Request，邀请、礼物等）
        /// </summary>
        /// <param name="requestId">请求id</param>
        /// <param name="callback">回调，结果返回</param>
        public override void fbDeleteRequest(string requestId, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("fbDeleteRequest", activity, requestId, new InternalAndroidSimpleCallback(callback));
            }));
        }

        /// <summary>
        /// 登录Game Service
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调</param>
        public override void signInGame(string platform, WACallback<WAPlayer> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAPlayer> onError = new OnError<WAPlayer>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("signInGame", activity, platform, new InternalSignInGameCallback(callback));
            }));
        }

        /// <summary>
        /// 登出Game Service
        /// </summary>
        /// <param name="platform">平台名称</param>
        public override void signOutGame(string platform)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("signOutGame", platform);
            }));
        }

        /// <summary>
        /// 是否已经登录Game
        /// </summary>
        /// <param name="platform">平台</param>
        /// <returns> 是否已登录Game， true 已登录； false 未登录</returns>
        public override bool isGameSignedIn(string platform)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                return false;
            }
            return socialProxy.CallStatic<bool>("isGameSignedIn", platform);
        }

        /// <summary>
        /// 解锁成就
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="id">成就id</param>
        /// <param name="callback"><回调，返回结果/param>
        public override void unlockAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAUpdateAchievementResult> onError = new OnError<WAUpdateAchievementResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("unlockAchievement", platform, id, new InternalUpdateAchievementCallback(callback));
            }));
        }

        /// <summary>
        /// 增加成就完成进度，仅仅对分布成就有效
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="id">成就id</param>
        /// <param name="numSteps">增加的数量</param>
        /// <param name="callback">回调，返回结果</param>
        public override void increaseAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAUpdateAchievementResult> onError = new OnError<WAUpdateAchievementResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("increaseAchievement", platform, id, numSteps, new InternalUpdateAchievementCallback(callback));
            }));
        }

        /// <summary>
        /// 展示成就列表
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <returns>调用结果</returns>
        public override void displayAchievement(string platform)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                return;
            }
            else
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    socialProxy.CallStatic<int>("displayAchievement", activity, platform);
                }));
            }
        }

        /// <summary>
        /// 展示成就列表
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="callback">回调，在成就页面关闭的时候返回结果</param>
        /// <returns></returns>
        public override int displayAchievement(string platform, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if(null != callback)
                {
                    callback.onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return WAStatusCode.CODE_ERROR;
            }
            else
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    socialProxy.CallStatic<int>("displayAchievement", activity, platform, new InternalAndroidSimpleCallback(callback));
                }));
                return WAStatusCode.CODE_SUCCESS;
            }

        }

        /// <summary>
        /// 设置分步成就已经完成了多少步，只对分步成就有效，而且当设置完成步数小于事实上已经完成的步数时，
        /// 调用接口不会改变任何东西，比如成就A（总共100步），已完成20，当参数numSteps小于等于20的时候，
        /// 调用这个接口不会有任何效果，当参数numSteps大于20的时候，成就A已完成的步数将会变成新的值<br></br>
        /// Set an achievement to have at least the given number of steps completed.
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="id">成就id</param>
        /// <param name="numSteps">已完成的步数</param>
        /// <param name="callback"> 回调</param>
        public override void setStepsAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAUpdateAchievementResult> onError = new OnError<WAUpdateAchievementResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("setStepsAchievement", platform, id, numSteps, new InternalUpdateAchievementCallback(callback));
            }));
        }

        /// <summary>
        /// 显示隐藏的成就<br></br>
        /// 有些成就初试状态是处于隐藏状态下的，当玩家达到某种条件的时候才对玩家显示，这个接口就可以满足这个需求
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="id">成就id</param>
        /// <param name="callback">回调</param>
        public override void revealAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAUpdateAchievementResult> onError = new OnError<WAUpdateAchievementResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("revealAchievement", platform, id, new InternalUpdateAchievementCallback(callback));
            }));
        }

        /// <summary>
        /// 加载成就数据
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="forceReload">是否强制重新加载</param>
        /// <param name="callback">回调</param>
        public override void loadAchievements(string platform, bool forceReload, WACallback<WALoadAchievementResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WALoadAchievementResult> onError = new OnError<WALoadAchievementResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("loadAchievements", platform, forceReload, new InternalLoadAchievementCallback(callback));
            }));
        }

        /// <summary>
        /// 根据Group id查询Group详细信息，支持同时多个查询
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="gids">group id或者是group screen name</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void getGroupByGid(string platform, string[] gids, string extInfo, WACallback<WAGroupResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAGroupResult> onError = new OnError<WAGroupResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("getGroupByGid", activity, platform, gids, extInfo, new InternalGetGroupCallback(callback));
            }));

        }

        /// <summary>
        /// 查询当前应用关联的Group详细信息
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void getCurrentAppLinkedGroup(string platform, string extInfo, WACallback<WAGroupResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAGroupResult> onError = new OnError<WAGroupResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("getCurrentAppLinkedGroup", activity, platform, extInfo, new InternalGetGroupCallback(callback));
            }));
        }

        /// <summary>
        /// 查询当前用户加入的Group详细信息
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void getCurrentUserGroup(string platform, string extInfo, WACallback<WAGroupResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAGroupResult> onError = new OnError<WAGroupResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("getCurrentUserGroup", activity, platform, extInfo, new InternalGetGroupCallback(callback));
            }));

        }

        /// <summary>
        /// 当前用户是否为指定Group的成员
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupId">指定Group id</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void isCurrentUserGroupMember(string platform, string groupId, string extInfo, WACallback<bool> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<bool> onError = new OnError<bool>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", false);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("isCurrentUserGroupMember", activity, platform, groupId, extInfo, new InternalBoolCallback(callback));
            }));
        }

        /// <summary>
        /// 当前用户加入到指定的Group
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupId">指定的Group id</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void joinGroup(string platform, string groupId, string extInfo, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                socialProxy.CallStatic("joinGroup", activity, platform, groupId, extInfo, new InternalAndroidSimpleCallback(callback));
            }));
        }

        /// <summary>
        /// 打开Group页面，应用或者网页
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupUri">Group uri</param>
        /// <param name="extInfo">额外数据</param>
        public override void openGroupPage(string platform, string groupUri, string extInfo)
        {
            if (null == androidJavaClass || null == socialProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.social.WASocialProxy failed!");
                return;
            }
            else
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    socialProxy.CallStatic("openGroupPage", activity, platform, groupUri, extInfo);
                }));
            }
        }


        /// <summary>
        /// 分享回调，内部使用，结果中转
        /// </summary>
        class InternalShareCallback : AndroidCallback<WAShareResult>
        {
            public InternalShareCallback(WACallback<WAShareResult> callback) : base(callback)
            {

            }

            protected override WAShareResult parseResult(AndroidJavaObject result)
            {
                return WAShareResult.fromAndroidJavaObject(result);
            }
        }
    }

    /// <summary>
    /// App invite结果回调，内部使用，中转结果
    /// </summary>
    internal class InternalAndroidSimpleCallback : AndroidCallback<WAResult>
    {
        public InternalAndroidSimpleCallback(WACallback<WAResult> callback) : base(callback)
        {
        }

        protected override WAResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAResult waResult = new WAResult();
            waResult.code = result.Get<int>("code");
            waResult.message = result.Get<string>("message");
            return waResult;
        }
    }

    /// <summary>
    /// 查询好友回调，内部使用，中转结果
    /// </summary>
    internal class InternalQueryFriendsCallback : AndroidCallback<WAFriendsResult>
    {
        public InternalQueryFriendsCallback(WACallback<WAFriendsResult> callback) : base(callback)
        {
        }

        protected override WAFriendsResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAFriendsResult friendsResult = new WAFriendsResult();
            friendsResult.code = result.Get<int>("code");
            friendsResult.message = result.Get<string>("message");
            AndroidJavaObject friendsListObject = result.Get<AndroidJavaObject>("friends");
            if (null != friendsListObject)
            {
                int size = friendsListObject.Call<int>("size");
                WAUser user;
                for (int i = 0; i < size; i++)
                {
                    user = WAUser.fromAndroidJavaObject(friendsListObject.Call<AndroidJavaObject>("get", i));
                    if (null != user)
                    {
                        friendsResult.addFriend(user);
                    }
                }
            }
            return friendsResult;
        }
    }

    /// <summary>
    /// 发送请求的回调，内部使用，结果中转
    /// </summary>
    internal class InternalSendRequestCallback : AndroidCallback<WARequestSendResult>
    {
        public InternalSendRequestCallback(WACallback<WARequestSendResult> callback) : base(callback)
        {

        }

        protected override WARequestSendResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WARequestSendResult sendRequestResult = new WARequestSendResult();
            sendRequestResult.code = result.Get<int>("code");
            sendRequestResult.message = result.Get<string>("message");
            sendRequestResult.requestId = result.Get<string>("requestId");
            AndroidJavaObject recipientsListObject = result.Get<AndroidJavaObject>("recipients");
            if (null != recipientsListObject)
            {
                int size = recipientsListObject.Call<int>("size");
                AndroidJavaObject recipientObject;
                for (int i = 0; i < size; i++)
                {
                    recipientObject = recipientsListObject.Call<AndroidJavaObject>("get", i);
                    if (null != recipientObject)
                    {
                        sendRequestResult.AddRecipient(recipientObject.ToString());
                    }
                }
            }
            return sendRequestResult;
        }
    }

    /// <summary>
    /// 查询Facebook Open Graph Object回调，内部使用，结果中转
    /// </summary>
    internal class InternalQueryFBGraphObjectsCallback : AndroidCallback<WAFBGraphObjectResult>
    {
        public InternalQueryFBGraphObjectsCallback(WACallback<WAFBGraphObjectResult> callback) : base(callback) {

        }

        protected override WAFBGraphObjectResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAFBGraphObjectResult objectsResult = new WAFBGraphObjectResult();
            AndroidJavaObject recipientsListObject = result.Get<AndroidJavaObject>("objects");
            if (null != recipientsListObject)
            {
                int size = recipientsListObject.Call<int>("size");
                WAFBGraphObject graphObject;
                for (int i = 0; i < size; i++)
                {
                    graphObject = WAFBGraphObject.fromAndroidJavaObject(recipientsListObject.Call<AndroidJavaObject>("get", i));
                    if (null != graphObject)
                    {
                        objectsResult.addObject(graphObject);
                    }
                }
            }
            return objectsResult;
        }
    }

    /// <summary>
    /// 查询Facebook游戏请求的回调，内部使用，中转结果
    /// </summary>
    internal class InternalQueryFBGameRequestCallback : AndroidCallback<WAFBGameRequestResult>
    {
        public InternalQueryFBGameRequestCallback(WACallback<WAFBGameRequestResult> callback) : base(callback)
        {

        }

        protected override WAFBGameRequestResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAFBGameRequestResult requestResult = new WAFBGameRequestResult();
            AndroidJavaObject requestsListObject = result.Get<AndroidJavaObject>("requests");
            if (null != requestsListObject)
            {
                int size = requestsListObject.Call<int>("size");
                WAFBGameRequestData requestObject;
                for (int i = 0; i < size; i++)
                {
                    requestObject = WAFBGameRequestData.fromAndroidJavaObject(requestsListObject.Call<AndroidJavaObject>("get", i));
                    if (null != requestObject)
                    {
                        requestResult.addRequests(requestObject);
                    }
                }
            }
            return requestResult;
        }


    }

    /// <summary>
    /// 登录Game Service回调，内部使用，结果中转
    /// </summary>
    internal class InternalSignInGameCallback : AndroidCallback<WAPlayer>
    {
        public InternalSignInGameCallback(WACallback<WAPlayer> callback) : base(callback)
        {

        }

        protected override WAPlayer parseResult(AndroidJavaObject result)
        {
            return WAPlayer.fromAndroidJavaObject(result);
        }

    }

    /// <summary>
    /// 成就更新回调，内部使用，结果中转
    /// </summary>
    internal class InternalUpdateAchievementCallback : AndroidCallback<WAUpdateAchievementResult>
    {
        public InternalUpdateAchievementCallback(WACallback<WAUpdateAchievementResult> callback) : base(callback)
        {

        }

        protected override WAUpdateAchievementResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAUpdateAchievementResult updateResult = new WAUpdateAchievementResult();
            return updateResult;
        }
    }

    /// <summary>
    /// 加载成就回调，内部使用，结果中转
    /// </summary>
    internal class InternalLoadAchievementCallback : AndroidCallback<WALoadAchievementResult>
    {
        public InternalLoadAchievementCallback(WACallback<WALoadAchievementResult> callback) : base(callback)
        {

        }

        protected override WALoadAchievementResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WALoadAchievementResult achievementResult = new WALoadAchievementResult();
            achievementResult.code = result.Get<int>("code");
            achievementResult.message = result.Get<string>("message");
            AndroidJavaObject achievementListObject = result.Get<AndroidJavaObject>("achievements");
            if (null != achievementListObject)
            {
                int size = achievementListObject.Call<int>("size");
                WAAchievement achievement;
                for (int i = 0; i < size; i++)
                {
                    achievement = WAAchievement.fromAndroidJavaObject(achievementListObject.Call<AndroidJavaObject>("get", i));
                    if (null != achievement)
                    {
                        achievementResult.addAchievement(achievement);
                    }
                }
            }
            return achievementResult;
        }
    }

    /// <summary>
    /// 获取Group列表回调，内部使用，结果中转
    /// </summary>
    internal class InternalGetGroupCallback : AndroidCallback<WAGroupResult>
    {
        public InternalGetGroupCallback(WACallback<WAGroupResult> callback) : base(callback)
        {

        }

        protected override WAGroupResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAGroupResult groupResult = new WAGroupResult();
            groupResult.code = result.Get<int>("code");
            groupResult.message = result.Get<string>("message");
            AndroidJavaObject groupMapObject = result.Get<AndroidJavaObject>("groups");
            if (null != groupMapObject)
            {
                AndroidJavaObject dataKeySet = groupMapObject.Call<AndroidJavaObject>("keySet");
                if (null != dataKeySet)
                {
                    AndroidJavaObject iteratorAndroidObject = dataKeySet.Call<AndroidJavaObject>("iterator");
                    if (null != iteratorAndroidObject)
                    {
                        string key;
                        AndroidJavaObject valueObject;
                        while (iteratorAndroidObject.Call<bool>("hasNext"))
                        {
                            key = iteratorAndroidObject.Call<string>("next");
                            valueObject = groupMapObject.Call<AndroidJavaObject>("get", key);
                            if (null != valueObject)
                            {
                                groupResult.addGroup(key, WAGroup.fromAndroidJavaObject(valueObject));
                            }
                        }
                    }
                }
            }
            return groupResult;
        }
    }

    /// <summary>
    /// 返回值是bool的回调，内部使用，结果中转
    /// </summary>
    internal class InternalBoolCallback : AndroidJavaProxy
    {
        protected WACallback<bool> callback;
        public InternalBoolCallback(WACallback<bool> callback) : base("com.wa.sdk.common.model.WACallback")
        {
            this.callback = callback;
        }
        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public void onSuccess(int code, string message, bool result)
        {
            if (null != callback)
            {
                OnSuccess<bool> onSuccess = new OnSuccess<bool>(callback.onSuccess);
                onSuccess(code, message, result);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void onCancel()
        {
            if (null != callback)
            {
                OnCancel onCancel = new OnCancel(callback.onCancel);
                onCancel();
            }
        }


        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public void onError(int code, string message, bool result, AndroidJavaObject throwable)
        {
            if (null != callback)
            {
                OnError<bool> onError = new OnError<bool>(callback.onError);
                onError(code, message, result);
            }
        }
    }
}
