using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WAConstants
    {
        


    }

    public class WALoginFLowType
    {
        /** 登录账号绑定类型：登录的时候不会重新绑定设备 **/
        public const int LOGIN_FLOW_TYPE_DEFAULT = 1;
        /** 登录账号绑定类型：登录时重新将新账号与当前设备绑定 **/
        public const int LOGIN_FLOW_TYPE_REBIND = 2;
    }


    public class WAEventType
    {
        /// <summary>
        /// <para>登陆游戏</para>
        /// <para>建议参数：</para>
        /// <para>无参数</para>
        /// </summary>
        public const string LOGIN = "ghw_login";

        /// <summary>
        /// <para>点击充值，充值初始化（真实货币）</para>
        /// <para>建议参数：</para>
        /// <para>无参数</para>
        /// </summary>
        public const string INITIATED_PAYMENT = "ghw_initiated_payment";

        /// <summary>
        /// <para>充值完成（真实货币）</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.TRANSACTION_ID 交易流水号（string）</para>
        /// <para>2) WAEventParameterName.PAYMENT_TYPE} 支付类型（string）</para>
        /// <para>3) WAEventParameterName.CURRENCY_TYPE} 货币类型（string），采用标准：ISO 4217 Currency Codes，部分值参考WACurrencyType类定义的常量</para>
        /// <para>4) WAEventParameterName.CURRENCY_AMOUNT} 现金数量（float）</para>
        /// <para>5) WAEventParameterName.VERTUAL_COIN_AMOUNT} 虚拟货币数量（int）</para>
        /// <para>6) WAEventParameterName.VERTUAL_COIN_CURRENCY} 虚拟货币类型（string）</para>
        /// <para>7) WAEventParameterName.IAP_ID} 道具id（string）</para>
        /// <para>8) WAEventParameterName.IAP_NAME} 道具名称（string）</para>
        /// <para>9) WAEventParameterName.IAP_AMOUNT} 道具数量（int）</para>
        /// <para>10) WAEventParameterName.LEVEL} 等级（int）</para>
        /// </summary>
        public const string COMPLETE_PAYMENT = "ghw_payment";

        /// <summary>
        /// <para>点击购买（虚拟货币）</para>
        /// <para>建议参数：</para>
        /// <para>无参数</para>
        /// </summary>
        public const string INITIATED_PURCHASE = "ghw_initiated_purchase";

        /// <summary>
        /// <para>购买完成（虚拟货币）</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.ITEM_NAME 购买虚拟物品名称（string）</para>
        /// <para>2) WAEventParameterName.ITEM_AMOUNT 购买虚拟物品数量（int）</para>
        /// <para>3) WAEventParameterName.PRICE 价格（float）</para>
        /// <para>4) WAEventParameterName.LEVEL 等级（int）</para>
        /// </summary>
        public const string COMPLETE_PURCHASE = "ghw_purchase";

        /// <summary>
        /// <para>创建角色</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.ROLE_TYPE 角色类型</para>
        /// <para>2) WAEventParameterName.GENDER 性别（int），取值参考 WAGender 类字段</para>
        /// <para>3) WAEventParameterName.NICKNAME 角色名称（String）</para>
        /// <para>4) WAEventParameterName.REGISTER_TIME 注册时间（long）</para>
        /// <para>5) WAEventParameterName.VIP VIP等级（int）</para>
        /// <para>6) WAEventParameterName.BINDED_GAME_GOLD 绑定钻石(int)</para>
        /// <para>7) WAEventParameterName.GAME_GOLD 用户钻石数（int）</para>
        /// <para>8) WAEventParameterName.LEVEL 用户等级数（int）</para>
        /// <para>9) WAEventParameterName.FIGHTING 战斗力(String)</para>
        /// </summary>
        public const string USER_CREATED = "ghw_user_create";

        /// <summary>
        /// <para>用户资料更新</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.NICKNAME 角色名称（string）</para>
        /// <para>2) WAEventParameterName.VIP VIP 等级（int）</para>
        /// <para>3) WAEventParameterName.ROLE_TYPE 角色类型(string)</para>
        /// </summary>
        public const string USER_INFO_UPDATE = "ghw_user_info_update";

        /// <summary>
        /// <para>导入用户事件</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.IS_FIRST_ENTER 是否第一次进服（int）</para>
        /// </summary>
        public const string IMPORT_USER = "ghw_user_import";

        /// <summary>
        /// <para>货币状况变更</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.GOLD_TYPE 货币类型(string)</para>
        /// <para>2) WAEventParameterName.APPROACH 变更途径(string)</para>
        /// <para>3) WAEventParameterName.AMOUNT 变更货币数,消耗使用负数标识(int)</para>
        /// <para>3) WAEventParameterName.CURRENT_AMOUNT 当前类型货币数(int)</para>
        /// </summary>
        public const string GOLD_UPDATE = "ghw_gold_update";

        /// <summary>
        /// <para>玩家任务统计</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.TASK_ID 任务Id(string)</para>
        /// <para>2) WAEventParameterName.TASK_NAME 任务名称(string)</para>
        /// <para>3) WAEventParameterName.TASK_TYPE 任务类型(string)</para>
        /// <para>4) WAEventParameterName.TASK_STATUS 任务状态(int), 状态标识：1-领取任务，2-开始任务，3-待领奖（任务完成），4-已领奖</para>
        /// </summary>
        public const string TASK_UPDATE = "ghw_task_update";

        /// <summary>
        /// <para>等级或分数达成</para>
        /// <para>建议参数：</para>
        /// <para>1) WAEventParameterName.SCORE 分数（int）</para>
        /// <para>2) WAEventParameterName.LEVEL 等级（int）</para>
        /// <para>2) WAEventParameterName.FIGHTING 等级（int）</para>
        /// </summary>
        public const string LEVEL_ACHIEVED = "ghw_level_achieved";

        /// <summary>
        /// <para>自定义事件名称的头部</para>
        /// <para>建议参数：</para>
        /// <para>根据实际需要传参</para>
        /// </summary>
        public const string CUSTOM_EVENT_PREFIX = "ghw_self_";
    }

    public class WAEventParameterName
    {

        /// <summary>
        /// 性别（GhwGender）
        /// </summary>
        public const string GENDER = "gender";

        /// <summary>
        /// 等级（int）
        /// </summary>
        public const string LEVEL = "level";

        /// <summary>
        /// 交易的流水号（String）
        /// </summary>
        public const string TRANSACTION_ID = "transactionId";

        /// <summary>
        /// 支付类型（string）
        /// </summary>
        public const string PAYMENT_TYPE = "paymentType";

        /// <summary>
        /// 货币类型（string）
        /// </summary>
        public const string CURRENCY_TYPE = "currencyType";

        /// <summary>
        /// 现金额（float）
        /// </summary>
        public const string CURRENCY_AMOUNT = "currencyAmount";

        /// <summary>
        /// 虚拟游戏币（float）
        /// </summary>
        public const string VERTUAL_COIN_AMOUNT = "virtualCoinAmount";

        /// <summary>
        /// 虚拟货币类型（string）
        /// </summary>
        public const string VERTUAL_COIN_CURRENCY = "virtualCurrency";

        /// <summary>
        /// 道具ID（String）
        /// </summary>
        public const string IAP_ID = "iapId";

        /// <summary>
        /// 道具名称（String）
        /// </summary>
        public const string IAP_NAME = "iapName";

        /// <summary>
        /// 道具数量（int）
        /// </summary>
        public const string IAP_AMOUNT = "iapAmount";

        /// <summary>
        /// 游戏内虚拟物品的ID（String）
        /// </summary>
        public const string ITEM_ID = "itemId";

        /// <summary>
        /// 游戏内虚拟物品的名称/ID（String）
        /// </summary>
        public const string ITEM_NAME = "itemName";

        /// <summary>
        /// 交易的数量（int）
        /// </summary>
        public const string ITEM_AMOUNT = "itemAmount";

        /// <summary>
        /// 得分数（int）
        /// </summary>
        public const string SCORE = "score";

        /// <summary>
        /// 价格（float/int）
        /// </summary>
        public const string PRICE = "price";

        /// <summary>
        /// 昵称（String）
        /// </summary>
        public const string NICKNAME = "nickname";

        /// <summary>
        /// VIP等级（int）
        /// </summary>
        public const string VIP = "vip";

        /// <summary>
        /// 角色类型（String）
        /// </summary>
        public const string ROLE_TYPE = "roleType";

        /// <summary>
        /// 绑定钻石(int)
        /// </summary>
        public const string BINDED_GAME_GOLD = "bindGameGold";

        /// <summary>
        /// 用户钻石数(int)
        /// </summary>
        public const string GAME_GOLD = "gameGold";

        /// <summary>
        /// 战斗力(int)
        /// </summary>
        public const string FIGHTING = "fighting";

        /// <summary>
        /// 注册时间(long)
        /// </summary>
        public const string REGISTER_TIME = "registerTime";

        /// <summary>
        /// 任务Id(String)
        /// </summary>
        public const string TASK_ID = "taskId";

        /// <summary>
        /// 任务名称(String)
        /// </summary>
        public const string TASK_NAME = "taskName";

        /// <summary>
        /// 任务类型(String)
        /// </summary>
        public const string TASK_TYPE = "taskType";

        /// <summary>
        /// 任务状态(int),状态标识：1-领取任务，2-开始任务，3-待领奖（任务完成），4-已领奖
        /// </summary>
        public const string TASK_STATUS = "taskStatus";

        /// <summary>
        /// 货币类型(String)
        /// </summary>
        public const string GOLD_TYPE = "goldType";

        /// <summary>
        /// 变更途径(String)        /// </summary>
        public const string APPROACH = "approach";

        /// <summary>
        /// 变更货币数（int）
        /// </summary>
        public const string AMOUNT = "amount";

        /// <summary>
        /// 当前货币数量 （int）
        /// </summary>
        public const string CURRENT_AMOUNT = "currentAmount";

        /// <summary>
        /// 是否第一次进入，第一次导入用户（int）默认为0， 是为：1
        /// </summary>
        public const string IS_FIRST_ENTER = "isFirstEnter";

    }


    /// <summary>
    /// 货币类型，采用标准：ISO 4217 Currency Codes
    /// </summary>
    public class WACurrencyType
    {
        /// <summary>
        /// 货币类型:Brazil Real(巴西）
        /// </summary>
        public const string CURRENCY_BRL = "BRL";
        /// <summary>
        /// 货币类型:Canada Dollar(加拿大）
        /// </summary>
        public const string CURRENCY_CAD = "CAD";
        /// <summary>
        /// 货币类型:China Yuan Renminbi / 人民币（中国大陆货币类型）
        /// </summary>
        public const string CURRENCY_CNY = "CNY";
        /// <summary>
        /// 货币类型:Euro Member Countries / 欧元
        /// </summary>
        public const string CURRENCY_EUR = "EUR";
        /// <summary>
        /// 货币类型:United Kingdom Pound（英镑）
        /// </summary>
        public const string CURRENCY_GBP = "GBP";
        /// <summary>
        /// 货币类型:Hong Kong Dollar / 港币（中国香港货币类型
        /// </summary>
        public const string CURRENCY_HKD = "HKD";
        /// <summary>
        /// 货币类型:Japan Yen / 日元（日本货币类型）
        /// </summary>
        public const string CURRENCY_JPY = "JPY";
        /// <summary>
        /// 货币类型:Korea (North) Won / 北朝鲜（朝鲜）货币类型
        /// </summary>
        public const string CURRENCY_KPW = "KPW";
        /// <summary>
        /// 货币类型:Korea (South) Won / 南朝鲜（韩国）货币类型
        /// </summary>
        public const string CURRENCY_KRW = "KRW";
        /// <summary>
        /// 货币类型:Taiwan New Dollar / 新台币（中国台湾货币类型）
        /// </summary>
        public const string TWD = "TWD";
        /// <summary>
        /// 货币类型:United States Dollar / 美元（美国货币类型）
        /// </summary>
        public const string CURRENCY_USD = "USD";
        /// <summary>
        /// 货币类型:Viet Nam Dong / 越南币
        /// </summary>
        public const string CURRENCY_VND = "VND";
    }

    /// <summary>
    /// 性别
    /// </summary>
    public class WAGender
    {
        /// <summary>
        /// 性别：女性
        /// </summary>
        public const int GENDER_FEMALE = 0;
        /// <summary>
        /// 性别：男性
        /// </summary>
        public const int GENDER_MALE = 1;
        /// <summary>
        /// 性别：未知 
        /// </summary>
        public const int GENDER_UNKNOWN = 2;
    }

    /// <summary>
    /// WA 渠道定义
    /// </summary>
    public class WAChannel
    {
        /// <summary>
        /// 渠道：Google
        /// </summary>
        public const string CHANNEL_GOOGLE = "GOOGLE";

        /// <summary>
        /// 渠道：Apple
        /// </summary>
        public const string CHANNEL_APPLE = "APPLE"; 

        /// <summary>
        /// 渠道：Facebook
        /// </summary>
        public const string CHANNEL_FACEBOOK = "FACEBOOK"; 

        /// <summary>
        /// 渠道：Appsflyer
        /// </summary>
        public const string CHANNEL_APPSFLYER = "APPSFLYER";

        /// <summary>
        /// 渠道：Chartboost
        /// </summary>
        public const string CHANNEL_CHARTBOOST = "CHARTBOOST";

        /// <summary>
        /// 渠道：Ghw（WingAnalytic）
        /// </summary>
        public const string CHANNEL_WA = "WINGA"; 

        ///// <summary>
        ///// 渠道：WEBPAY（Webview Payment）
        ///// </summary>
        //public const string CHANNEL_WEBPAY = "WEBPAY";

        /// <summary>
        /// 渠道：VK
        /// </summary>
        public const string CHANNEL_VK = "VK";

        ///// <summary>
        ///// 渠道：360
        ///// </summary>
        //public const string CHANNEL_360 = "360";

        ///// <summary>
        ///// 渠道：百度
        ///// </summary>
        //public const string CHANNEL_BAIDU = "BAIDU";

        ///// <summary>
        ///// 渠道：UC
        ///// </summary>
        //public const string CHANNEL_UC = "UC";
        
        ///// <summary>
        ///// 渠道：微信
        ///// </summary>
        //public const string CHANNEL_WECHAT = "WECHAT";
    }

    /// <summary>
    /// 社交发送请求的类型
    /// </summary>
    public class WARequestType
    {
        /// <summary>
        /// 社交请求（Request）类型：邀请
        /// </summary>
        public const string REQUEST_INVITE = "INVITE";
        /// <summary>
        /// 社交请求（Request）类型：请求
        /// </summary>
        public const string REQUEST_REQUEST = "REQUEST";
        /// <summary>
        /// 社交请求（Request）类型：发送礼物
        /// </summary>
        public const string REQUEST_GIFT_SEND = "GIFT_SEND";
        /// <summary>
        /// 社交请求（Request）类型：索要礼物
        /// </summary>
        public const string REQUEST_GIFT_ASK = "GIFT_ASK";
    }

    /// <summary>
    /// Facebook权限
    /// </summary>
    public class WAFBPermission
    {
        /// <summary>
        /// Facebook登陆权限--获取用户配置信息的权限（read权限）
        /// </summary>
        public const string FB_PERMISSION_PUBLIC_PROFILE = "public_profile";

        /// <summary>
        /// Facebook登陆权限--获取用户好友的权限（read权限）
        /// </summary>
        public const string FB_PERMISSION_USER_FRIENDS = "user_friends";

        /// <summary>
        /// Facebook登陆权限--发布权限（write权限）
        /// </summary>
        public const string FB_PERMISSION_PUBLISH_ACTIONS= "publish_actions";
    }

    public class WAStatusCode
    {
        
        /// <summary>
        /// 成功
        /// </summary>
        public const int CODE_SUCCESS = 200;

        /// <summary>
        /// 出现错误
        /// </summary>
        public const int CODE_ERROR = 400;

        /// <summary>
        /// 请求未认证：访问受限资源是缺少认证信息，或者认证未通过
        /// </summary>
        public const int CODE_UNAUTHERIZED = 401;

        /// <summary>
        /// 禁止访问：由于应用上下文原因或请求端上下文的原因被禁止访问资源，例如IP限制等
        /// </summary>
        public const int CODE_FORBIDEN = 403;

        /// <summary>
        /// 找不到被访问资源：接口不存在、页面不存在或对应的业务实体找不到
        /// </summary>
        public const int CODE_NOT_FOUND = 404;

        /// <summary>
        /// 服务器内部故障
        /// </summary>
        public const int CODE_SERVER_ERROR = 500;

        /// <summary>
        /// 所请求接口或页面未实现
        /// </summary>
        public const int CODE_API_INVALID = 501;

        /// <summary>
        /// 无效appId: appId不存在或未开启
        /// </summary>
        public const int CODE_SDK_APPID_INVALID = 4010;

        
        /// <summary>
        /// 无效osign：osign校验失败
        /// </summary>
        public const int CODE_SIGN_ERROR = 4011;

        /// <summary>
        /// 请求已过期：ots校验失败
        /// </summary>
        public const int CODE_REQUEST_TIME_OUT = 4012;

        /// <summary>
        /// 第三方平台验证失败
        /// </summary>
        public const int CODE_PLATFORM_VERIFY_ERROR = 4013;

        /// <summary>
        /// 访客登录验证失败，登录验证失败
        /// </summary>
        public const int CODE_ACCOUNT_VERIFY_ERROR = 4014;

        /// <summary>
        /// 用户已经绑定了这个平台的其他账户
        /// </summary>
        public const int CODE_PLATFORM_BOUND_ALREADY = 4015;

        /// <summary>
        /// prePlatform验证失败
        /// </summary>
        public const int CODE_PRE_PLATFORM_VERIFY_ERROR = 4016;

        /// <summary>
        /// 用户不存在（没有找到）
        /// </summary>
        public const int CODE_USER_NOT_FOUND = 4017;

        /// <summary>
        /// 账户已经被其他用户绑定
        /// </summary>
        public const int CODE_ACCOUNT_BOUND_BY_OTHERS = 4018;

        /// <summary>
        /// 无效orderId
        /// </summary>
        public const int CODE_ORDER_ID_INVALID = 4019;

        /// <summary>
        /// 订单验证失败
        /// </summary>
        public const int CODE_ORDER_VERIFY_ERROR = 4020;

        /// <summary>
        /// 邀请奖励事件未找到奖励政策
        /// </summary>
        public const int CODE_REWARD_NOT_FOUND = 4021;

        /// <summary>
        /// 闪退发送报告重复
        /// </summary>
        public const int CODE_REPEAT_CRASH_REPORT = 4022;

        /// <summary>
        /// 未找到渠道信息
        /// </summary>
        public const int CODE_CHENNEL_NOT_FOUND = 4023;

        /// <summary>
        /// 不可以执行解绑操作
        /// </summary>
        public const int CODE_UNABLE_DISBAND = 4024;

        /// <summary>
        /// 支付渠道已关闭
        /// </summary>
        public const int CODE_PAY_PLATFORM_CLOSED = 4026;

        /// <summary>
        /// 登录渠道已关闭
        /// </summary>
        public const int CODE_LOGIN_PLATFORM_CLOSED = 4029;


        /// <summary>
        /// 取消操作
        /// </summary>
        public const int CODE_CANCELED = -100;

        /// <summary>
        /// 文件找不到
        /// </summary>
        public const int CODE_FILE_NOT_FOUND = -101;

        /// <summary>
        /// API不支持
        /// </summary>
        public const int CODE_API_NOT_SUPPORTED = -102;

        // FaceBook
        /// <summary>
        /// 分享享SDK没有初始化
        /// </summary>
        public const int CODE_SDK_UNINITIALIZED = -200;

        /// <summary>
        /// 内容不可分享，一般是传入的内容为空，或者其他
        /// </summary>
        public const int CODE_CONTENT_CAN_NOT_BE_SHARED = -201;

        /// <summary>
        /// 没有登陆
        /// </summary>
        public const int CODE_NOT_LOGIN = -202;

        /// <summary>
        /// 登录失败
        /// </summary>
        public const int CODE_LOGIN_FAILURE = -203;

        /// <summary>
        /// 登陆没有获取到相应的权限
        /// </summary>
        public const int CODE_NO_PERMISSION = -204;

        /// <summary>
        /// 异常信息错误
        /// </summary>
        public const int CODE_EXCEPTION = -205;

        /// <summary>
        /// 文件大小超出限制
        /// </summary>
        public const int CODE_FILE_SIZE_LIMIT = -206;

        /// <summary>
        /// 没有以平台账户登录
        /// </summary>
        public const int CODE_NOT_LOGIN_WITH_PLATFORM = -207;

        /// <summary>
        /// ServerId没有设置
        /// </summary>
        public const int CODE_SERVER_ID_NOT_FOUND = -208;

        /// <summary>
        /// 账户不存在
        /// </summary>
        public const int CODE_ACCOUNT_NOT_FOUND = -209;

        /// <summary>
        /// 账户不允许解绑
        /// </summary>
        public const int CODE_ACCOUNT_NOT_ALLOW_UNBIND = -210;

        /// <summary>
        /// 登录的平台账户和当前用户不匹配
        /// </summary>
        public const int CODE_PLATFORM_ACCOUNT_NOT_MATCH = -211;

        /// <summary>
        /// Game userId没有设置
        /// </summary>
        public const int CODE_GAME_USER_ID_NOT_FOUND = -212;



        // Google Service
        /// <summary>
        /// 没有安装Google服务
        /// </summary>
        public const int CODE_GOOGLE_SERVICE_MISSING = -301;

        /// <summary>
        /// Google服务正在更新中
        /// </summary>
        public const int CODE_GOOGLE_SERVICE_UPDATING = -302;

        /// <summary>
        /// Google服务版本过低，需要更新
        /// </summary>
        public const int CODE_GOOGLE_SERVICE_VERSION_UPDATE_REQUIRED = -303;

        /// <summary>
        /// Google服务被禁用
        /// </summary>
        public const int CODE_GOOGLE_SERVICE_DISABLED = -304;

        /// <summary>
        /// Google服务不可用
        /// </summary>
        public const int CODE_GOOGLE_SERVICE_INVALID = -305;


        // Hot patch
        /// <summary>
        /// 设备不支持
        /// </summary>
        public const int CODE_DEVICE_NO_SUPPORTED = -401;

        /// <summary>
        /// 网络不可用
        /// </summary>
        public const int CODE_NETWORK_UNAVAILABLE = -402;

        //　支付
        /// <summary>
        /// 支付：服务连接中断
        /// </summary>
        public const int CODE_PAY_SERVICE_DISCONNECTED = -501;

        /// <summary>
        /// 支付：服务不可用
        /// </summary>
        public const int CODE_PAY_SERVICE_UNUSABLE = -502;

        /// <summary>
        /// 支付: 商品不可用
        /// </summary>
        public const int CODE_PAY_ITEM_UNAVAILABLE = -503;

        /// <summary>
        /// 支付：开发者错误
        /// </summary>
        public const int CODE_PAY_DEVELOPER_ERROR = -504;

        /// <summary>
        /// 支付：已经拥有该商品（没有消耗）
        /// </summary>
        public const int CODE_PAY_ITEM_ALREADY_OWNED = -505;

        /// <summary>
        /// 支付：没有拥有该商品
        /// </summary>
        public const int CODE_PAY_ITEM_NOT_OWNED = -506;

        /// <summary>
        /// 支付：支付成功但是没有上报或者上报失败了
        /// </summary>
        public const int CODE_PAY_WITHOUT_REPORT = -507;

        /// <summary>
        /// 支付：支付成功，但是通知后台的时候校验失败了
        /// </summary>
        public const int CODE_PAY_CHECKING_FAILED = -508;

        /// <summary>
        /// 支付：订单时间间隔限制（在特定的时间内重复下订单）
        /// </summary>
        public const int CODE_PAY_REORDER_TIME_LIMIT = -509;


        // Game Service result code
        /// <summary>
        /// Game: 成就不是分步成就
        /// Indicates that the call to increment achievement failed since the achievement is not an incremental achievement.
        /// </summary>
        public const int CODE_ACHIEVEMENT_NOT_INCREMENTAL = -601;
        /// <summary>
        ///  Game: 成就id不存在，找不到指定的成就
        ///  Could not find the achievement, so the operation to update the achievement failed.
        /// </summary>
        public const int CODE_ACHIEVEMENT_UNKNOWN = -602;
        /// <summary>
        /// Game: 成就已经解锁了
        /// Indicates that the incremental achievement was also unlocked when the call was made to increment the achievement.
        /// </summary>
        public const int CODE_ACHIEVEMENT_UNLOCKED = -603;
        /// <summary>
        /// Game: 内部成就，不能直接解锁
        /// An incremental achievement cannot be unlocked directly, so the call to unlock achievement failed.
        /// </summary>
        public const int CODE_ACHIEVEMENT_UNLOCK_FAILURE = -604;
        /// <summary>
        /// Game: 游戏服务已登出，需要重新登录
        /// </summary>
        public const int CODE_GAME_NEED_SIGN = -605;
    }
}
