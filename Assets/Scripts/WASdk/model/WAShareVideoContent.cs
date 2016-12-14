using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// 分享视频内容实体类
    /// </summary>
    public class WAShareVideoContent : WAShareContent
    {
        public string contentDescription;
        public string contentTitle;
        public WASharePhoto previewPhoto;
        public WAShareVideo video;

        protected WAShareVideoContent(Builder builder)
        {
            message = builder.message;
            contentUri = builder.contentUri;
            peopleIds = builder.peopleIds;
            placeId = builder.placeId;
            refStr = builder.refStr;

            contentDescription = builder.contentDescription;
            contentTitle = builder.contentTitle;
            previewPhoto = builder.previewPhoto;
            video = builder.video;
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public override AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WAShareVideoContent$Builder");
            javaObjectBuilder.Call<AndroidJavaObject>("setMessage", message);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentUri", Utils.uriToAndroidJavaObject(contentUri));
            javaObjectBuilder.Call<AndroidJavaObject>("setPeopleIds", Utils.stringListToAndroidJavaObject(peopleIds));
            javaObjectBuilder.Call<AndroidJavaObject>("setPlaceId", placeId);
            javaObjectBuilder.Call<AndroidJavaObject>("setRef", refStr);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentDescription", contentDescription);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentTitle", contentTitle);
            if(null != previewPhoto)
            {
                javaObjectBuilder.Call<AndroidJavaObject>("setPreviewPhoto", previewPhoto.toAndroidJavaObject());
            }
            if (null != video)
            {
                javaObjectBuilder.Call<AndroidJavaObject>("setVideo", video.toAndroidJavaObject());
            }
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        /// <summary>
        /// 分享视频内容实体类Builder
        /// </summary>
        public class Builder : WAShareContentBuilder<WAShareVideoContent, Builder>
        {
            public string contentDescription;
            public string contentTitle;
            public WASharePhoto previewPhoto;
            public WAShareVideo video;

            /// <summary>
            /// Sets the description of the video.
            /// </summary>
            /// <param name="contentDescription">The description of the video.</param>
            /// <returns>he builder.</returns>
            public Builder setContentDescription(string contentDescription)
            {
                this.contentDescription = contentDescription;
                return this;
            }

            /// <summary>
            /// Sets the title to display for this video.
            /// </summary>
            /// <param name="contentTitle">The video title.</param>
            /// <returns>The builder.</returns>
            public Builder setContentTitle(string contentTitle)
            {
                this.contentTitle = contentTitle;
                return this;
            }

            /// <summary>
            /// Sets the photo to be used as a preview for the video.
            /// </summary>
            /// <param name="previewPhoto">Preview photo for the content.</param>
            /// <returns>The builder.</returns>
            public Builder setPreviewPhoto(WASharePhoto previewPhoto)
            {
                this.previewPhoto = previewPhoto;
                return this;
            }

            /// <summary>
            /// Sets the video to be shared.
            /// </summary>
            /// <param name="video">video数据</param>
            /// <returns>The builder.</returns>
            public Builder setVideo(WAShareVideo video)
            {
                if (video == null)
                {
                    return this;
                }

                this.video = video;
                return this;
            }

            public override WAShareVideoContent build()
            {
                return new WAShareVideoContent(this);
            }
        }
    }
}
