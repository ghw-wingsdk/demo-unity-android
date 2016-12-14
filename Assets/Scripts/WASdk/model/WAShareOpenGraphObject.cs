using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// Share open graph object
    /// </summary>
    public class WAShareOpenGraphObject : WAShareOpenGraphValueContainer
    {
        public const string FB_OPEN_GRAPH_CREATE_OBJECT_KEY = "fbsdk:create_object";

        private WAShareOpenGraphObject(Builder builder)
        {
            this.map = builder.map;
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns>AndroidJavaObject对象</returns>
        public override AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WAShareOpenGraphObject$Builder");

            Dictionary<string, object> actionMap = getMap();
            if (null != actionMap && actionMap.Count > 0)
            {
                object obj;
                foreach (var item in actionMap)
                {
                    obj = item.Value;
                    if (null == obj)
                    {
                        continue;
                    }
                    if (obj is bool)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putBoolean", item.Key, (bool)obj);
                    }
                    else if (obj is bool[])
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putBooleanArray", item.Key, (bool[])obj);
                    }
                    else if (obj is double)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putDouble", item.Key, (double)obj);
                    }
                    else if (obj is double[])
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putDoubleArray", item.Key, (double[])obj);
                    }
                    else if (obj is int)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putInt", item.Key, (int)obj);
                    }
                    else if (obj is int[])
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putIntArray", item.Key, (int[])obj);
                    }
                    else if (obj is long)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putLong", item.Key, (long)obj);
                    }
                    else if (obj is long[])
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putLongArray", item.Key, (long[])obj);
                    }
                    else if (obj is string)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putString", item.Key, (string)obj);
                    }
                    else if (obj is List<string>)
                    {
                        List<string> list = (List<string>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        javaObjectBuilder.Call<AndroidJavaObject>("putStringArrayList", item.Key, Utils.stringListToAndroidJavaObject(list));
                    }
                    else if (obj is WASharePhoto)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putPhoto", item.Key, ((WASharePhoto)obj).toAndroidJavaObject());
                    }
                    else if (obj is WAShareOpenGraphObject)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("putObject", item.Key, ((WAShareOpenGraphObject)obj).toAndroidJavaObject());
                    }
                    else if (obj is List<WAShareOpenGraphObject>)
                    {
                        List<WAShareOpenGraphObject> list = (List<WAShareOpenGraphObject>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        AndroidJavaObject arrayListJavaObject = new AndroidJavaObject("java.util.ArrayList");
                        foreach (WAShareOpenGraphObject objectItem in list)
                        {
                            arrayListJavaObject.Call<bool>("add", objectItem.toAndroidJavaObject());
                        }
                        javaObjectBuilder.Call<AndroidJavaObject>("putObjectArrayList", item.Key, arrayListJavaObject);
                    }
                    else if (obj is List<WASharePhoto>)
                    {
                        List<WASharePhoto> list = (List<WASharePhoto>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        AndroidJavaObject arrayListJavaObject = new AndroidJavaObject("java.util.ArrayList");
                        foreach (WASharePhoto photoItem in list)
                        {
                            arrayListJavaObject.Call<bool>("add", photoItem.toAndroidJavaObject());
                        }
                        javaObjectBuilder.Call<AndroidJavaObject>("putBooleanArray", item.Key, arrayListJavaObject);
                    }
                }
            }
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        /// <summary>
        /// Builder for the <see cref="WAShareOpenGraphObject"/> interface.
        /// </summary>
        public class Builder : WAShareOpenGraphValueContainer.Builder<WAShareOpenGraphObject, Builder>
        {
            public Builder()
            {
                this.putBool(FB_OPEN_GRAPH_CREATE_OBJECT_KEY, true);
            }

            public WAShareOpenGraphObject build()
            {
                return new WAShareOpenGraphObject(this);
            }

        }
    }
}
