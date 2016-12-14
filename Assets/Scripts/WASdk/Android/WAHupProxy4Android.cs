using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    internal class WAHupProxy4Android : WAHupProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass hupProxy = null;

        public WAHupProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            hupProxy = new AndroidJavaClass("com.wa.sdk.hotupdate.WAHupProxy");
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="callback">结果回调</param>
        public override void checkUpdate(WACallback<WACheckUpdateResult> callback)
        {
            if (null == androidJavaClass && null == hupProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.hotupdate.WAHupProxy failed!");
                if (null != callback)
                {
                    OnError<WACheckUpdateResult> onError = new OnError<WACheckUpdateResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.hotupdate.WAHupProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                hupProxy.CallStatic("checkUpdate", new InternalCheckUpdateCallback(callback));
            }));
        }

        /// <summary>
        /// 开始更新
        /// </summary>
        /// <param name="updateInfo">检查更新结果</param>
        /// <param name="callback">更新回调</param>
        public override void startUpdate(WACheckUpdateResult updateInfo, WACallback<string> callback)
        {
            if (null == androidJavaClass && null == hupProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.hotupdate.WAHupProxy failed!");
                if (null != callback)
                {
                    OnError<string> onError = new OnError<string>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.hotupdate.WAHupProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject javaUpdateInfo = new AndroidJavaObject("com.wa.sdk.hotupdate.model.WAUpdateInfo");
                javaUpdateInfo.Set<int>("code", updateInfo.code);
                javaUpdateInfo.Set<string>("message", updateInfo.message);
                javaUpdateInfo.Set<bool>("hasUpdate", updateInfo.hasUpdate);
                javaUpdateInfo.Set<bool>("forceUpdate", updateInfo.forceUpdate);
                javaUpdateInfo.Set<string>("md5", updateInfo.md5);
                javaUpdateInfo.Set<int>("patchId", updateInfo.patchId);
                javaUpdateInfo.Set<int>("patchVersion", updateInfo.patchVersion);
                javaUpdateInfo.Set<string>("moduleId", updateInfo.moduleId);
                javaUpdateInfo.Set<string>("downloadUrl", updateInfo.downloadUrl);
                hupProxy.CallStatic("startUpdate", javaUpdateInfo, new InternalStringCallback(callback));
            }));
        }
    }



    /// <summary>
    /// 检查更新内部回调，结果中转
    /// </summary>
    internal class InternalCheckUpdateCallback : AndroidCallback<WACheckUpdateResult>
    {

        public InternalCheckUpdateCallback(WACallback<WACheckUpdateResult> callback) : base(callback)
        {
        }

        protected override WACheckUpdateResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WACheckUpdateResult checkUpdateResult = new WACheckUpdateResult();
            checkUpdateResult.code = result.Get<int>("code");
            checkUpdateResult.message = result.Get<string>("message");
            checkUpdateResult.hasUpdate = result.Get<bool>("hasUpdate");
            checkUpdateResult.forceUpdate = result.Get<bool>("forceUpdate");
            checkUpdateResult.md5 = result.Get<string>("md5");
            checkUpdateResult.patchId = result.Get<int>("patchId");
            checkUpdateResult.patchVersion = result.Get<int>("patchVersion");
            checkUpdateResult.moduleId = result.Get<string>("moduleId");
            checkUpdateResult.downloadUrl = result.Get<string>("downloadUrl");
            return checkUpdateResult;
        }
    }

    //internal class InternalStringCallback : AndroidCallback<string>
    //{
    //    public InternalStringCallback(WACallback<string> callback) : base(callback)
    //    {

    //    }

    //    protected override string parseResult(AndroidJavaObject result)
    //    {
    //        if(null == result)
    //        {
    //            return null;
    //        }
    //        return result.Call<string>("toString");
    //    }
    //}
}
