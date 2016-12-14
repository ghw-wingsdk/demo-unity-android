using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class Utils
    {
        private Utils() { }

        /// <summary>
        /// 将AndroidJavaObject类型Uri转换成C# Uri
        /// </summary>
        /// <param name="uriObject"></param>
        /// <returns></returns>
        public static Uri androidJavaObjectToUri(AndroidJavaObject uriObject)
        {
            if (null == uriObject)
            {
                return null;
            }
            string path = uriObject.Call<string>("getPath");
            if (null == path || path.Length < 1)
            {
                return null;
            }
            Uri uri = new Uri(path);
            return uri;
        }

        /// <summary>
        /// C#的Uri对象转换成AndroidJavaObject对象（Android的Uri）
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static AndroidJavaObject uriToAndroidJavaObject(Uri uri)
        {
            if(null == uri)
            {
                return null;
            }
            AndroidJavaClass uriJavaClass = new AndroidJavaClass("android.net.Uri");
            return uriJavaClass.CallStatic<AndroidJavaObject>("parse", uri.AbsoluteUri);
        }

        /// <summary>
        /// 将List&lt;string&gt;类型的对象转换成Java ArrayList&lt;String&gt;的AndroidJavaObject对象
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static AndroidJavaObject stringListToAndroidJavaObject(List<string> list)
        {
            if(null == list)
            {
                return null;
            }
            AndroidJavaObject arrayListJavaObject = new AndroidJavaObject("java.util.ArrayList");
            foreach (string item in list)
            {
                arrayListJavaObject.Call<bool>("add", item);
            }
            return arrayListJavaObject;
        }
    }
}
