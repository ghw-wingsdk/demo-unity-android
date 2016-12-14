using System;

using UnityEngine;

namespace WASdkAPI
{
    internal class WACoreProxy4Android : WACoreProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass coreProxy = null;

        public WACoreProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            coreProxy = new AndroidJavaClass("com.wa.sdk.core.WACoreProxy");
        }

        /// <summary>
        /// 初始化WASDK，如果在Activity中已经初始化，不需要在调用这个方法初始化
        /// </summary>
        public override void initialize()
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            coreProxy.CallStatic("initialize", activity);
        }

        /// <summary>
        /// 设置ClientId，这是人工干扰模式，一旦设置，只有清除缓存才能恢复原来的clientId
        /// </summary>
        /// <param name="clientId">clientId 客户端id</param>
        public override void setClientId(string clientId)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setClientId", clientId);
        }

        /// <summary>
        /// 设置WA SDK用户id（如果使用WA SDK的登录（含第三方平台登录），不需要手动设置）
        /// </summary>
        /// <param name="userId">用户id</param>
        public override void setUserId(string userId)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setUserId", userId);
        }

        /// <summary>
        /// 设置游戏用户id
        /// </summary>
        /// <param name="gameUserId">游戏用户id</param>
        public override void setGameUserId(string gameUserId)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setGameUserId", gameUserId);
        }

        /// <summary>
        /// 设置server id
        /// </summary>
        /// <param name="serverId">服务器id</param>
        public override void setServerId(string serverId)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setServerId", serverId);
        }

        /// <summary>
        /// 设置用户等级
        /// </summary>
        /// <param name="level">用户等级</param>
        public override void setLevel(int level)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setLevel", level);
        }

        /// <summary>
        /// 设置调试模式，如果开启调试模式，可以在logcat中查看日志输出，默认关闭
        /// </summary>
        /// <param name="debugMode">是否开启调试模式</param>
        public override void setDebugMode(bool debugMode)
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return;
            }
            coreProxy.CallStatic("setDebugMode", debugMode);
        }

        /// <summary>
        /// 获取client id，根据特定的生成算法生成client id。
        /// </summary>
        /// <returns>当前设备的设备id（根据SDK的算法生成）</returns>
        public override string getClientId()
        {
            if (null == androidJavaClass && null == coreProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.core.WACoreProxy failed!");
                return null;
            }
            return coreProxy.CallStatic<string>("getClientId");
        }
    }
}
