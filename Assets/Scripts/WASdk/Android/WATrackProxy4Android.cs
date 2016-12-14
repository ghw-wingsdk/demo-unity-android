using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    internal class WATrackProxy4Android : WATrackProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass trackProxy = null;

        public WATrackProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            trackProxy = new AndroidJavaClass("com.wa.sdk.track.WATrackProxy");
        }

        /// <summary>
        /// 心跳开始，在Activity中调用, Unity忽略该接口
        /// </summary>
        public override void startHeartBeat()
        {
            if (null == androidJavaClass || null == trackProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.track.WATrackProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                trackProxy.CallStatic("startHeartBeat", activity);
            }));
        }

        /// <summary>
        /// 心跳结束，在Activity中调用, Unity忽略该接口
        /// </summary>
        public override void stopHeartBeat()
        {
            if (null == androidJavaClass || null == trackProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.track.WATrackProxy failed!");
                return;
            }

            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                trackProxy.CallStatic("stopHeartBeat", activity);
            }));
        }

        /// <summary>
        /// 数据收集
        /// </summary>
        /// <param name="event">事件数据实体类对象</param>
        public override void trackEvent(WAEvent waEvent)
        {
            if (null == androidJavaClass || null == trackProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.track.WATrackProxy failed!");
                return;
            }

            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                // 这里需要将Unity的C#对象转换成Android的Java对象
                AndroidJavaObject eventObjectBuilder = new AndroidJavaObject("com.wa.sdk.track.model.WAEvent$Builder");

                eventObjectBuilder.Call<AndroidJavaObject>("setDefaultEventName", waEvent.defaultEventName);
                eventObjectBuilder.Call<AndroidJavaObject>("setDefaultValue", waEvent.defaultValue);

                if (null != waEvent.defaultEventValues)
                {
                    AndroidJavaObject obj;
                    foreach (var item in waEvent.defaultEventValues)
                    {
                        if (item.Value is int)
                        {
                            obj = new AndroidJavaObject("java.lang.Integer", (int)item.Value);
                        }
                        else if (item.Value is float)
                        {
                            obj = new AndroidJavaObject("java.lang.Float", (float)item.Value);
                        }
                        else if (item.Value is long)
                        {
                            obj = new AndroidJavaObject("java.lang.Long", (long)item.Value);
                        }
                        else if (item.Value is double)
                        {
                            obj = new AndroidJavaObject("java.lang.Double", (double)item.Value);
                        }
                        else if (item.Value is string)
                        {
                            obj = new AndroidJavaObject("java.lang.String", (string)item.Value);
                        }
                        else
                        {
                            obj = new AndroidJavaObject("java.lang.String", item.Value.ToString());
                        }
                        eventObjectBuilder.Call<AndroidJavaObject>("addDefaultEventValue", item.Key, obj);
                    }
                }

                if (null != waEvent.channelSwitchMap)
                {
                    foreach (var item in waEvent.channelSwitchMap)
                    {
                        if (!item.Value)
                        {
                            eventObjectBuilder.Call<AndroidJavaObject>("disableChannel", item.Key);
                        }
                    }
                }

                if (null != waEvent.valueMap)
                {
                    foreach (var item in waEvent.valueMap)
                    {
                        eventObjectBuilder.Call<AndroidJavaObject>("setChannelValue", item.Key, item.Value);
                    }
                }

                if (null != waEvent.eventNameMap)
                {
                    foreach (var item in waEvent.eventNameMap)
                    {
                        eventObjectBuilder.Call<AndroidJavaObject>("setChannelEventName", item.Key, item.Value);
                    }
                }

                if (null != waEvent.eventValueMap)
                {
                    Dictionary<string, object> channelEventValueDictionary;
                    AndroidJavaObject obj;
                    foreach (var item in waEvent.eventValueMap)
                    {
                        channelEventValueDictionary = item.Value;
                        if (null != channelEventValueDictionary)
                        {
                            foreach (var valueItem in channelEventValueDictionary)
                            {
                                if (valueItem.Value is int)
                                {
                                    obj = new AndroidJavaObject("java.lang.Integer", (int)valueItem.Value);
                                }
                                else if (valueItem.Value is float)
                                {
                                    obj = new AndroidJavaObject("java.lang.Float", (float)valueItem.Value);
                                }
                                else if (valueItem.Value is long)
                                {
                                    obj = new AndroidJavaObject("java.lang.Long", (long)valueItem.Value);
                                }
                                else if (valueItem.Value is double)
                                {
                                    obj = new AndroidJavaObject("java.lang.Double", (double)valueItem.Value);
                                }
                                else if (valueItem.Value is string)
                                {
                                    obj = new AndroidJavaObject("java.lang.String", (string)valueItem.Value);
                                }
                                else
                                {
                                    obj = new AndroidJavaObject("java.lang.String", valueItem.Value.ToString());
                                }
                                eventObjectBuilder.Call<AndroidJavaObject>("addChannelEventValue", item.Key, obj);
                            }
                        }
                    }
                }

                AndroidJavaObject eventObject = eventObjectBuilder.Call<AndroidJavaObject>("build");
                trackProxy.CallStatic("trackEvent", activity, eventObject);
            }));

        }
    }
}
