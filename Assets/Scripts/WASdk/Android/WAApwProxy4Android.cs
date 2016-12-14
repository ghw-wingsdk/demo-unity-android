using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    internal class WAApwProxy4Android : WAApwProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass apwProxy = null;

        public WAApwProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            apwProxy = new AndroidJavaClass("com.wa.sdk.appwall.WAApwProxy");
        }

        /// <summary>
        /// 显示应用墙入口悬浮窗按钮
        /// </summary>
        public override void showEntryFlowIcon()
        {
            if (null == androidJavaClass && null == apwProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.appwall.WAApwProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                apwProxy.CallStatic("showEntryFlowIcon", activity);
            }));
        }

        /// <summary>
        /// 隐藏应用墙入口悬浮窗按钮
        /// </summary>
        public override void hideEntryFlowIcon()
        {
            if (null == androidJavaClass && null == apwProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.appwall.WAApwProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                apwProxy.CallStatic("hideEntryFlowIcon", activity);
            }));
        }
    }
}
