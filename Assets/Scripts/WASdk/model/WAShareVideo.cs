using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// 分享视频数据实体类
    /// </summary>
    public class WAShareVideo
    {
        private Uri localUri;

        public WAShareVideo(Builder builder)
        {
            localUri = builder.localUri;
        }

        /// <summary>
        /// 将C#的WASharePhoto对象转换成Java中WASharePhoto类型AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WAShareVideo$Builder");
            javaObjectBuilder.Call<AndroidJavaObject>("setLocalUri", Utils.uriToAndroidJavaObject(localUri));
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        /// <summary>
        /// WASharePhoto Builder
        /// </summary>
        public class Builder
        {
            public Uri localUri;

            /// <summary>
            /// Sets the URL to the video.
            /// </summary>
            /// <param name="imageUri"> Uri that points to a network location or the location of the photo on disk.</param>
            /// <returns>The builder.</returns>
            public Builder setLocalUri(Uri localUri)
            {
                this.localUri = localUri;
                return this;
            }

            public WAShareVideo build()
            {
                return new WAShareVideo(this);
            }
        }
    }
}
