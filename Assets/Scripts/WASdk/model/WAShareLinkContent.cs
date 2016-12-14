using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAShareLinkContent : WAShareContent
    {
        public string contentDescription;
        public string contentTitle;
        public Uri imageUri;

        protected WAShareLinkContent(Builder builder)
        {
            message = builder.message;
            contentUri = builder.contentUri;
            peopleIds = builder.peopleIds;
            placeId = builder.placeId;
            refStr = builder.refStr;
            contentDescription = builder.contentDescription;
            contentTitle = builder.contentTitle;
            imageUri = builder.imageUri;
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public override AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WAShareLinkContent$Builder");
            javaObjectBuilder.Call<AndroidJavaObject>("setMessage", message);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentUri", Utils.uriToAndroidJavaObject(contentUri));
            javaObjectBuilder.Call<AndroidJavaObject>("setPeopleIds", Utils.stringListToAndroidJavaObject(peopleIds));
            javaObjectBuilder.Call<AndroidJavaObject>("setPlaceId", placeId);
            javaObjectBuilder.Call<AndroidJavaObject>("setRef", refStr);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentDescription", contentDescription);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentTitle", contentTitle);
            javaObjectBuilder.Call<AndroidJavaObject>("setImageUri", Utils.uriToAndroidJavaObject(imageUri));
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        public class Builder : WAShareContentBuilder<WAShareLinkContent, Builder>
        {
            public string contentDescription;
            public string contentTitle;
            public Uri imageUri;

            /// <summary>
            /// Set the contentDescription of the link.
            /// </summary>
            /// <param name="contentDescription">The contentDescription of the link.</param>
            /// <returns>The builder.</returns>
            public Builder setContentDescription(string contentDescription)
            {
                this.contentDescription = contentDescription;
                return this;
            }

            /// <summary>
            /// Set the contentTitle to display for this link.
            /// </summary>
            /// <param name="contentTitle">The link contentTitle.</param>
            /// <returns>The builder.</returns>
            public Builder setContentTitle(string contentTitle)
            {
                this.contentTitle = contentTitle;
                return this;
            }

            /// <summary>
            /// Set the URL of a picture to attach to this content.
            /// </summary>
            /// <param name="imageUri">The network URL of an image.</param>
            /// <returns>The builder.</returns>
            public Builder setImageUri(Uri imageUri)
            {
                this.imageUri = imageUri;
                return this;
            }

            public override WAShareLinkContent build()
            {
                return new WAShareLinkContent(this);
            }
        }
    }
}
