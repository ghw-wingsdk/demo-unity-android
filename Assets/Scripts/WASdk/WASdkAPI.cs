using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace WASdkAPI
{

    /// <summary>
    /// WA SDK Core代理抽象类，Android和IOS都继承这个抽象类
    /// </summary>
    public abstract class WACoreProxy
    {
        private static WACoreProxy _instance = null;

        /// <summary>
        /// 获取WA SDK Core代理单例
        /// </summary>
        public static WACoreProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WACoreProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WACoreProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 初始化SDK，这个不需要在Unity中调用，在Android Activity的onCreate中已经调用
        /// </summary>
        public abstract void initialize();

        /// <summary>
        /// 设置ClientId，这是人工干扰模式，一旦设置，只有清除缓存才能恢复原来的clientId
        /// </summary>
        /// <param name="clientId">clientId 客户端id</param>
        public abstract void setClientId(string clientId);

        /// <summary>
        /// 设置WA SDK用户id（如果使用WA SDK的登录（含第三方平台登录），不需要手动设置）
        /// </summary>
        /// <param name="userId">用户id</param>
        public abstract void setUserId(string userId);

        /// <summary>
        /// 设置游戏用户id
        /// </summary>
        /// <param name="gameUserId">游戏用户id</param>
        public abstract void setGameUserId(string gameUserId);

        /// <summary>
        /// 设置server id
        /// </summary>
        /// <param name="serverId">服务器id</param>
        public abstract void setServerId(string serverId);

        /// <summary>
        /// 设置用户等级
        /// </summary>
        /// <param name="level">用户等级</param>
        public abstract void setLevel(int level);

        /// <summary>
        /// 设置调试模式，如果开启调试模式，可以在logcat中查看日志输出，默认关闭
        /// </summary>
        /// <param name="debugMode">是否开启调试模式</param>
        public abstract void setDebugMode(bool debugMode);

        /// <summary>
        /// 获取client id，根据特定的生成算法生成client id。
        /// </summary>
        /// <returns>当前设备的设备id（根据SDK的算法生成）</returns>
        public abstract string getClientId();

    }

    /// <summary>
    /// WA SDK 用户相关接口代理类
    /// </summary>
    public abstract class WAUserProxy
    {
        private static WAUserProxy _instance = null;

        public static WAUserProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WAUserProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WAUserProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 设置登录流程类型
        /// </summary>
        /// <param name="flowType">登录流程类型</param>
        public abstract void setLoginFlowType(int flowType);

        /// <summary>
        /// 调用接口登录
        /// </summary>
        /// <param name="platform">登录的平台</param>
        /// <param name="callback">登录回调</param>
        /// <param name="extInfo">额外信息</param>
        public abstract void login(string platform, WACallback<WALoginResult> callback, string extInfo);

        /// <summary>
        /// 带登录界面的登录
        /// </summary>
        /// <param name="enableCache">是否启用缓存</param>
        /// <param name="callback">登录回调</param>
        public abstract void loginUI(bool enableCache, WACallback<WALoginResult> callback);

        /// <summary>
        /// 清除登录缓存
        /// </summary>
        public abstract void clearLoginCache();

        /// <summary>
        /// 登出
        /// </summary>
        public abstract void logout();

        /// <summary>
        /// 绑定平台账号
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void bindingAccount(string platform, string extInfo, WABindCallback callback);

        /// <summary>
        /// 查询已绑定的平台账号列表
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void queryBoundAccount(WACallback<WAAccountResult> callback);


        /// <summary>
        /// 解绑第三方平台账户
        /// </summary>
        /// <param name="platform">需要解绑的第三方账户的平台类型</param>
        /// <param name="platformUserId">第三方平台用户id</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void unBindAccount(string platform, string platformUserId, WACallback<WAResult> callback);

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="platform">需要切换的账号平台类型</param>
        /// <param name="WACallback">回调，结果返回</param>
        public abstract void switchAccount(string platform, WACallback<WALoginResult> callback);

        /// <summary>
        /// 新建WA账户
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void createNewAccount(WACallback<WALoginResult> callback);

        /// <summary>
        /// 打开内置的账户管理页面
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void openAccountManager(WAAccountCallback callback);

        /// <summary>
        /// 获取当前用户的用户信息
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void getAccountInfo(string platform, WACallback<WAUser> callback);
    }

    /// <summary>
    /// 应用墙接口代理类
    /// </summary>
    public abstract class WAApwProxy
    {
        private static WAApwProxy _instance = null;

        public static WAApwProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WAApwProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WAApwProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 显示应用墙入口悬浮窗按钮
        /// </summary>
        public abstract void showEntryFlowIcon();

        /// <summary>
        /// 隐藏应用墙入口悬浮窗按钮
        /// </summary>
        public abstract void hideEntryFlowIcon();
    }

    /// <summary>
    /// 公共模块接口代理类
    /// </summary>
    public abstract class WACommonProxy
    {
        private static WACommonProxy _instance = null;

        public static WACommonProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WACommonProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WACommonProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }


        /// <summary>
        /// 打开Logcat入口悬浮按钮
        /// </summary>
        public abstract void enableLogcat();

        /// <summary>
        /// 关闭Logcat入口悬浮按钮
        /// </summary>
        public abstract void disableLogcat();

        /// <summary>
        /// Logcat中添加一条log信息，不分等级
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="msg">日志信息</param>
        public abstract void log(string tag, string msg);

        /// <summary>
        /// 应用运行时授权自检权限，如果没有权限，开始授权
        /// </summary>
        /// <param name="permission">检查的权限名称</param>
        /// <param name="WAPermissionCallback">回调，授权结果</param>
        public abstract void checkSelfPermission(string permission, WAPermissionCallback callback);

        /// <summary>
        /// 获取应用更新的链接地址
        /// 用于App有分包的情况，如果返回的链接不为空，该渠道包就用返回的链接地址进行更新， 如果为null，就随意更新（应用市场或者其他）
        /// </summary>
        /// <param name="callback">回调，结果返回，返回string类型，没有返回null</param>
        public abstract void getAppUpdateLink(WACallback<string> callback);
    }

    /// <summary>
    /// 热更新接口代理类
    /// </summary>
    public abstract class WAHupProxy
    {
        private static WAHupProxy _instance = null;

        public static WAHupProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WAHupProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WAHupProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="callback">结果回调</param>
        public abstract void checkUpdate(WACallback<WACheckUpdateResult> callback);

        /// <summary>
        /// 开始更新
        /// </summary>
        /// <param name="updateInfo">检查更新结果</param>
        /// <param name="callback">更新回调</param>
        public abstract void startUpdate(WACheckUpdateResult updateInfo, WACallback<string> callback);
    }

    /// <summary>
    /// 数据收集接口代理类
    /// </summary>
    public abstract class WATrackProxy
    {
        private static WATrackProxy _instance = null;

        public static WATrackProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WATrackProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WATrackProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 心跳开始，在Activity中调用, Unity忽略该接口
        /// </summary>
        public abstract void startHeartBeat();

        /// <summary>
        /// 心跳结束，在Activity中调用, Unity忽略该接口
        /// </summary>
        public abstract void stopHeartBeat();

        /// <summary>
        /// 数据收集
        /// </summary>
        /// <param name="event">事件数据实体类对象</param>
        public abstract void trackEvent(WAEvent waEvent);
    }

    /// <summary>
    /// 支付接口代理类
    /// </summary>
    public abstract class WAPayProxy
    {
        private static WAPayProxy _instance = null;

        public static WAPayProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WAPayProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WAPayProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 支付服务是否可用
        /// </summary>
        /// <returns>支付服务是否可用，true为可用，false为不可用</returns>
        public abstract bool isPayServiceAvailable();

        /// <summary>
        /// 对所有已经选择的支付渠道初始化
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void initialize(WACallback<WAResult> callback);

        /// <summary>
        /// 查询库存
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void queryInventory(WACallback<WASkuResult> callback);

        /// <summary>
        /// 有支付页面的支付接口，可能包含原生支付和webview支付
        /// </summary>
        /// <param name="waProductId">WA商品id</param>
        /// <param name="extInfo">额外信息</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void payUI(string waProductId, string extInfo, WACallback<WAPurchaseResult> callback);


        /// <summary>
        /// 清理所有已选择的支付渠道
        /// </summary>
        public abstract void onDestroy();
    }

    /// <summary>
    /// 社交接口代理类
    /// </summary>
    public abstract class WASocialProxy
    {
        private static WASocialProxy _instance = null;

        /// <summary>
        /// 获取单例
        /// </summary>
        public static WASocialProxy Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR || UNITY_STANDLONE
                    _instance = new WASocialProxy4Default();
                    Debug.LogError("Not supported os");
#elif UNITY_ANDROID
                _instance = new WASocialProxy4Android();

//#elif UNITY_IOS
                //_instance = new SDKInterfaceIOS();
#endif
                }

                return _instance;
            }
        }

        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="shareContent">分享内容实体类</param>
        /// <param name="shareWithApi">是否通过api分享</param>
        /// <param name="extInfo">额外信息</param>
        /// <param name="callback">回调</param>
        public abstract void share(string platform, WAShareContent shareContent, bool shareWithApi, string extInfo, WACallback<WAShareResult> callback);

        /// <summary>
        /// 应用邀请
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="appLinkUrl">应用app link</param>
        /// <param name="previewImageUrl">预览图片地址</param>
        /// <param name="callback">回调,结果返回</param>
        public abstract void appInvite(string platform, string appLinkUrl, string previewImageUrl, WACallback<WAResult> callback);

        /// <summary>
        /// 查询可邀请好友
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="duration">多长时间之内不再显示已邀请的好友,毫秒</param>
        /// <param name="callback">回调</param>
        public abstract void queryInvitableFriends(string platform, long duration, WACallback<WAFriendsResult> callback);

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
        public abstract void sendRequest(string platform, string requestType, string title, string message, string objectId,
            List<string> receiptIds, WACallback<WARequestSendResult> callback, string extInfo);

        /// <summary>
        /// 游戏邀请
        /// </summary>
        /// <param name="platform">平台类型</param>
        /// <param name="title">邀请标题</param>
        /// <param name="message">邀请消息</param>
        /// <param name="ids">接收者id</param>
        /// <param name="callback">回调，结果返回</param>
        /// <see cref="sendRequest(string, string, string, string, string, List{string}, WACallback{WARequestSendResult}, string)">代替</see>
        //public abstract void gameInvite(string platform, string title, string message, List<string> ids, WACallback<WAInviteResult> callback);

        /// <summary>
        /// 创建邀请信息接口，用户成功邀请好友后调用，仅仅游戏邀请可用
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="requestId">请求id</param>
        /// <param name="recipients">接收者id列表</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void createInviteRecord(string platform, string requestId, List<string> recipients, WACallback<WAResult> callback);



        /// <summary>
        /// 邀请安装奖励，检查用户是否经过好友邀请才使用<br></br>
        /// <p>注意：这个接口必须在用户登录并且选服后调用。每次选服后都进行一次调用，内部 有检查是否已经发送数据到服务器的逻辑。
        /// 该方法异步执行这个接口可以在平台登录后（并且选服后）调用，也可以在绑定平台的账户后（并且选服后）调用</p>
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void inviteInstallReward(string platform, WACallback<WAResult> callback);

        /// <summary>
        /// 事件触发邀请奖励，由特定的事件触发的奖励<br></br>
        /// <p>这个接口必须在用户登录并且选服后，设置了userId和serverId后调用。并且是用户受邀请玩游戏，
        /// 已经调用<see cref="inviteEventReward(string, string, WACallback{WAResult})"/>上报了服务器后，邀请者才能获取到这个接口产生的奖励。</p>
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="callback"> 回调，结果返回</param>
        public abstract void inviteEventReward(string platform, string eventName, WACallback<WAResult> callback);


        /// <summary>
        /// 查询好友列表，返回正在使用当前APP的好友列表
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void queryFriends(string platform, WACallback<WAFriendsResult> callback);

        /// <summary>
        /// 查询Facebook Graph Object
        /// </summary>
        /// <param name="objectType">OpenGraph Object类型</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void queryFBGraphObjects(string objectType, WACallback<WAFBGraphObjectResult> callback);

        /// <summary>
        ///  Facebook赠送礼物
        /// </summary>
        /// <param name="title">请求对话框的标题</param>
        /// <param name="message">请求对话框内容</param>
        /// <param name="objectId">礼物Open Graph对象id</param>
        /// <param name="receipts">接收者Facebook openId</param>
        /// <param name="callback">回调，结果返回</param>
        /// <seealso cref="sendRequest(string, string, string, string, string, List, WACallback, string)"/>
        //public abstract void fbSendGift(string title, string message, string objectId,  List<string> receipts, WACallback<WAGiftingResult> callback);

        /// <summary>
        /// Facebook索要礼物
        /// </summary>
        /// <param name="title">请求对话框的标题</param>
        /// <param name="message">请求对话框内容</param>
        /// <param name="objectId">礼物Open Graph对象id</param>
        /// <param name="receipts">接收者Facebook openId</param>
        /// <param name="callback">回调，结果返回</param>
        /// <seealso cref="sendRequest(string, string, string, string, string, List{string}, WACallback{WARequestSendResult}, string)"/>
        //public abstract void fbAskForGift(string title, string message, string objectId, List<string> receipts, WACallback<WAGiftingResult> callback);


        /// <summary>
        /// Facebook查询收到的礼物
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void fbQueryReceivedGifts(WACallback<WAFBGameRequestResult> callback);

        /// <summary>
        /// Facebook查询收到的索要礼物请求
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public abstract void fbQueryAskForGiftRequests(WACallback<WAFBGameRequestResult> callback);

        /// <summary>
        /// Facebook删除请求（可删除各种Game Request，邀请、礼物等）
        /// </summary>
        /// <param name="requestId">请求id</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void fbDeleteRequest(string requestId, WACallback<WAResult> callback);

        /// <summary>
        /// 登录Game Service
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调</param>
        public abstract void signInGame(string platform, WACallback<WAPlayer> callback);

        /// <summary>
        /// 登出Game Service
        /// </summary>
        /// <param name="platform">平台名称</param>
        public abstract void signOutGame(string platform);


        /// <summary>
        /// 是否已经登录Game
        /// </summary>
        /// <param name="platform">平台</param>
        /// <returns> 是否已登录Game， true 已登录； false 未登录</returns>
        public abstract bool isGameSignedIn(string platform);

        /// <summary>
        /// 解锁成就
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="id">成就id</param>
        /// <param name="callback"><回调，返回结果/param>
        public abstract void unlockAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback);

        /// <summary>
        /// 增加成就完成进度，仅仅对分布成就有效
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="id">成就id</param>
        /// <param name="numSteps">增加的数量</param>
        /// <param name="callback">回调，返回结果</param>
        public abstract void increaseAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback);

        /// <summary>
        /// 展示成就列表
        /// </summary>
        /// <param name="platform">平台名称</param>
        public abstract void displayAchievement(string platform);

        /// <summary>
        /// 展示成就列表
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="callback">回调，在成就页面关闭的时候返回结果</param>
        /// <returns></returns>
        public abstract int displayAchievement(string platform, WACallback<WAResult> callback);

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
        public abstract void setStepsAchievement(string platform, string id, int numSteps, WACallback<WAUpdateAchievementResult> callback);

        /// <summary>
        /// 显示隐藏的成就<br></br>
        /// 有些成就初试状态是处于隐藏状态下的，当玩家达到某种条件的时候才对玩家显示，这个接口就可以满足这个需求
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="id">成就id</param>
        /// <param name="callback">回调</param>
        public abstract void revealAchievement(string platform, string id, WACallback<WAUpdateAchievementResult> callback);

        /// <summary>
        /// 加载成就数据
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="forceReload">是否强制重新加载</param>
        /// <param name="callback">回调</param>
        public abstract void loadAchievements(string platform, bool forceReload, WACallback<WALoadAchievementResult> callback);

        /// <summary>
        /// 根据Group id查询Group详细信息，支持同时多个查询
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="gids">group id或者是group screen name</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void getGroupByGid(string platform, string[] gids, string extInfo, WACallback<WAGroupResult> callback);

        /// <summary>
        /// 查询当前应用关联的Group详细信息
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void getCurrentAppLinkedGroup(string platform, string extInfo, WACallback<WAGroupResult> callback);

        /// <summary>
        /// 查询当前用户加入的Group详细信息
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void getCurrentUserGroup(string platform, string extInfo, WACallback<WAGroupResult> callback);

        /// <summary>
        /// 当前用户是否为指定Group的成员
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupId">指定Group id</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void isCurrentUserGroupMember(string platform, string groupId, string extInfo, WACallback<bool> callback);

        /// <summary>
        /// 当前用户加入到指定的Group
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupId">指定的Group id</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public abstract void joinGroup(string platform, string groupId, string extInfo, WACallback<WAResult> callback);

        /// <summary>
        /// 打开Group页面，应用或者网页
        /// </summary>
        /// <param name="platform">平台</param>
        /// <param name="groupUri">Group uri</param>
        /// <param name="extInfo">额外数据</param>
        public abstract void openGroupPage(string platform, string groupUri, string extInfo);
    }
}