using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// 分享结果数据实体类
    /// </summary>
    public class WAShareResult : WAResult
    {
        public string extra
        {
            get;
            set;
        }

        /// <summary>
        /// 从AndroidJavaObject对象转换成WAShareResult对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAShareResult fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if(null == javaObject)
            {
                return null;
            }
            WAShareResult result = new WAShareResult();
            result.code = javaObject.Get<int>("code");
            result.message = javaObject.Get<string>("message");
            result.extra = javaObject.Get<string>("extra");
            return result;
        }
    }

}
