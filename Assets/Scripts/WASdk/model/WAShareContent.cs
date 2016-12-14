using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public abstract class WAShareContent
    {
        /// <summary>
        /// 分享内容的文本
        /// </summary>
        public string message;
        /// <summary>
        /// 分享内容Uri
        /// </summary>
        public Uri contentUri;
        /// <summary>
        /// 接收者id列表
        /// </summary>
        public List<string> peopleIds;
        /// <summary>
        /// A value to be added to the referrer URL when a person follows a link from this shared content on feed.
        /// </summary>
        public string placeId;
        /// <summary>
        /// A value to be added to the referrer URL when a person follows a link from this shared content on feed.
        /// </summary>
        public string refStr;

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public abstract AndroidJavaObject toAndroidJavaObject();
    }
}
