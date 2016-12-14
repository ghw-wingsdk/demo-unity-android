using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// Facebook Application信息数据实体类
    /// </summary>
    public class WAFBApplication
    {
        public string id
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string category
        {
            get;
            set;
        }
        public string name_space
        {
            get;
            set;
        }
        public string link
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject数据转换成C#对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAFBApplication fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAFBApplication application = new WAFBApplication();
            application.id = javaObject.Get<string>("id");
            application.name = javaObject.Get<string>("name");
            application.category = javaObject.Get<string>("category");
            application.name_space = javaObject.Get<string>("namespace");
            application.link = javaObject.Get<string>("link");
            return application;
        }
    }
}
