using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// 分享图片内容实体类
    /// </summary>
    public class WASharePhotoContent : WAShareContent
    {
        public List<WASharePhoto> photos;

        protected WASharePhotoContent(Builder builder)
        {
            message = builder.message;
            contentUri = builder.contentUri;
            peopleIds = builder.peopleIds;
            placeId = builder.placeId;
            refStr = builder.refStr;

            photos = builder.photos;
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public override AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WASharePhotoContent$Builder");
            javaObjectBuilder.Call<AndroidJavaObject>("setMessage", message);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentUri", Utils.uriToAndroidJavaObject(contentUri));
            javaObjectBuilder.Call<AndroidJavaObject>("setPeopleIds", Utils.stringListToAndroidJavaObject(peopleIds));
            javaObjectBuilder.Call<AndroidJavaObject>("setPlaceId", placeId);
            javaObjectBuilder.Call<AndroidJavaObject>("setRef", refStr);
            if(null != photos)
            {
                foreach(WASharePhoto photo in photos)
                {
                    if(null != photo)
                    {
                        javaObjectBuilder.Call<AndroidJavaObject>("addPhoto", photo.toAndroidJavaObject());
                    }
                }
            }
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        public class Builder : WAShareContentBuilder<WASharePhotoContent, Builder>
        {
            public List<WASharePhoto> photos = new List<WASharePhoto>();

            /// <summary>
            /// 添加一张分享图片信息
            /// </summary>
            /// <param name="photo">分享图片信息</param>
            /// <returns>The Builder</returns>
            public Builder addPhoto(WASharePhoto photo)
            {
                if (photo != null)
                {
                    photos.Add(photo);
                }

                return this;
            }

            /// <summary>
            /// 同时添加多张分享图片信息
            /// </summary>
            /// <param name="photos">分享图片信息列表</param>
            /// <returns></returns>
            public Builder addPhotos(List<WASharePhoto> photos)
            {
                if (photos == null || photos.Count < 1)
                {
                    return this;
                }
                foreach(WASharePhoto photo in photos)
                {
                    photos.Add(photo);
                }
                return this;
            }

            public override WASharePhotoContent build()
            {
                return new WASharePhotoContent(this);
            }
        }
    }
}
