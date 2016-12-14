using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAFBGameRequestData
    {
        public string id
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
        public string createdTime
        {
            get;
            set;
        }
        public string actionType
        {
            get;
            set;
        }
        public WAUser from
        {
            get;
            set;
        }
        public WAUser to
        {
            get;
            set;
        }
        public WAFBGraphObject obj
        {
            get;
            set;
        }
        public WAFBApplication application
        {
            get;
            set;
        }
        public string data
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject对象转成C#对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAFBGameRequestData fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAFBGameRequestData requestObject = new WAFBGameRequestData();
            requestObject.id = javaObject.Get<string>("id");
            requestObject.message = javaObject.Get<string>("message");
            requestObject.createdTime = javaObject.Get<string>("createdTime");
            requestObject.actionType = javaObject.Get<string>("actionType");
            try
            {
                requestObject.from = WAUser.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("from"));
            }
            catch (Exception e)
            {
            }
            try
            {
                requestObject.to = WAUser.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("to"));
            }
            catch (Exception e)
            {
            }
            try
            {
                requestObject.obj = WAFBGraphObject.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("object"));
            }
            catch (Exception e)
            {
            }
            try
            {
                requestObject.application = WAFBApplication.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("application"));
            }
            catch (Exception e)
            {
            }
            requestObject.data = javaObject.Get<string>("data");

            return requestObject;
        }
    }
}
