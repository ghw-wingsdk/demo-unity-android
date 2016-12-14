using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    internal class WACommonProxy4Android : WACommonProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass commonProxy = null;

        public WACommonProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            commonProxy = new AndroidJavaClass("com.wa.sdk.common.WACommonProxy");
        }

        /// <summary>
        /// 打开Logcat入口悬浮按钮
        /// </summary>
        public override void enableLogcat()
        {
            if (null == androidJavaClass && null == commonProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.common.WACommonProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {

                commonProxy.CallStatic("enableLogcat", activity);
            }));
        }

        /// <summary>
        /// 关闭Logcat入口悬浮按钮
        /// </summary>
        public override void disableLogcat()
        {
            if (null == androidJavaClass && null == commonProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.common.WACommonProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                commonProxy.CallStatic("disableLogcat", activity);
            }));
        }

        /// <summary>
        /// Logcat中添加一条log信息，不分等级
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="msg">日志信息</param>
        public override void log(string tag, string msg)
        {
            if (null == androidJavaClass && null == commonProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.common.WACommonProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                commonProxy.CallStatic("log", tag, msg);
            }));
        }

        /// <summary>
        /// 应用运行时授权自检权限，如果没有权限，开始授权
        /// </summary>
        /// <param name="permission">检查的权限名称</param>
        /// <param name="WAPermissionCallback">回调，授权结果</param>
        public override void checkSelfPermission(string permission, WAPermissionCallback callback)
        {
            if (null == androidJavaClass && null == commonProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.common.WACommonProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                commonProxy.CallStatic("checkSelfPermission", permission, new InternalCheckPermissionCallback(callback));
            }));
        }

        /// <summary>
        /// 获取应用更新的链接地址
        /// 用于App有分包的情况，如果返回的链接不为空，该渠道包就用返回的链接地址进行更新， 如果为null，就随意更新（应用市场或者其他）
        /// </summary>
        /// <param name="callback">回调，结果返回，返回String类型，没有返回null</param>
        public override void getAppUpdateLink(WACallback<string> callback)
        {
            if (null == androidJavaClass && null == commonProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.common.WACommonProxy failed!");
                if (null != callback)
                {
                    OnError<string> onError = new OnError<string>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.common.WACommonProxy failed!", null);
                }
                return;

            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                commonProxy.CallStatic("getAppUpdateLink", new InternalStringCallback(callback));
            }));
        }
    }

    /// <summary>
    /// 检查权限内部回调，中转结果
    /// </summary>
    internal class InternalCheckPermissionCallback : AndroidJavaProxy
    {
        WAPermissionCallback callback;

        public InternalCheckPermissionCallback(WAPermissionCallback callback) : base("com.wa.sdk.common.model.WAPermissionCallback")
        {
            this.callback = callback;
        }

        /// <summary>
        /// 取消申请权限
        /// </summary>
        public void onCancel()
        {
            if(null != callback)
            {
                OnCancel onCancel = new OnCancel(callback.onCancel);
                onCancel();
            }
        }

        /// <summary>
        /// 权限申请结果
        /// </summary>
        /// <param name="permissions">申请的权限列表</param>
        /// <param name="grantedResults">权限申请结果</param>
        public void onRequestPermissionResult(string[] permissions, bool[] grantedResults)
        {
            if (null != callback)
            {
                OnRequestPermissionResult onPermissionResult = new OnRequestPermissionResult(callback.onRequestPermissionResult);
                onPermissionResult(permissions, grantedResults);
            }
        }
    }

    /// <summary>
    /// 获取应用升级链接内部回调，转换结果
    /// </summary>
    internal class InternalStringCallback : AndroidJavaProxy
    {
        protected WACallback<string> callback;
        public InternalStringCallback(WACallback<string> callback) : base("com.wa.sdk.common.model.WACallback")
        {
            this.callback = callback;
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public void onSuccess(int code, string message, string result)
        {
            if (null != callback)
            {
                OnSuccess<string> onSuccess = new OnSuccess<string>(callback.onSuccess);
                onSuccess(code, message, result);
            }
        }

        /// <summary>
        /// 成功回调，这个是防止message字段为null的时候，会当做Object来处理，导致找不到相应的回调方法
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public void onSuccess(int code, AndroidJavaObject message, string result)
        {
            if (null != callback)
            {
                OnSuccess<string> onSuccess = new OnSuccess<string>(callback.onSuccess);
                onSuccess(code, null == message ? "" : message.ToString(), result);
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
        public void onError(int code, string message, string result, AndroidJavaObject throwable)
        {
            if (null != callback)
            {
                OnError<string> onError = new OnError<string>(callback.onError);
                onError(code, message, result);
            }
        }
        /// <summary>
        /// 错误回调，这个是防止message字段为null的时候，会当做Object来处理，导致找不到相应的回调方法
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public void onError(int code, AndroidJavaObject message, string result, AndroidJavaObject throwable)
        {
            if (null != callback)
            {
                OnError<string> onError = new OnError<string>(callback.onError);
                onError(code, null == message ? "" : message.ToString(), result);
            }
        }
    }

}
