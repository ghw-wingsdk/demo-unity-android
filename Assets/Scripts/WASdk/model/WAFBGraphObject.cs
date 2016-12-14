using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// Facebook Open Graph Object对象实体类
    /// </summary>
    public class WAFBGraphObject
    {
        public string id
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        public bool isScraped
        {
            get;
            set;
        }
        public string createdTime
        {
            get;
            set;
        }
        public string description
        {
            get;
            set;
        }
        public string imageUrl
        {
            get;
            set;
        }
        public Dictionary<string, object> data
        {
            get;
            set;
        }

        public void addData(string key, object value)
        {
            if(null == data)
            {
                data = new Dictionary<string, object>();
            }
            data.Add(key, value);
        }

        /// <summary>
        /// 将AndroidJavaObject对象转成C# 对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAFBGraphObject fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAFBGraphObject graphObject = new WAFBGraphObject();
            graphObject.id = javaObject.Get<string>("id");
            graphObject.title = javaObject.Get<string>("title");
            graphObject.type = javaObject.Get<string>("type");
            graphObject.isScraped = javaObject.Get<bool>("isScraped");
            graphObject.createdTime = javaObject.Get<string>("createdTime");
            graphObject.description = javaObject.Get<string>("description");
            graphObject.imageUrl = javaObject.Get<string>("imageUrl");
            AndroidJavaObject dataAndroidObject = javaObject.Get<AndroidJavaObject>("data");
            if (null != dataAndroidObject)
            {
                AndroidJavaObject dataKeySet = dataAndroidObject.Call<AndroidJavaObject>("keySet");
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
                            valueObject = dataAndroidObject.Call<AndroidJavaObject>("get", key);
                            if (null != valueObject)
                            {
                                graphObject.addData(key, valueObject);
                            }
                        }
                    }
                }
            }
            return graphObject;
        }
    }
}
