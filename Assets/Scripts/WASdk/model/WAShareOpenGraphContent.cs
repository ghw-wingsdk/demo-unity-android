using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAShareOpenGraphContent : WAShareContent
    {
        public string previewPropertyName;
        public WAShareOpenGraphAction action;

        protected WAShareOpenGraphContent(Builder builder)
        {
            message = builder.message;
            contentUri = builder.contentUri;
            peopleIds = builder.peopleIds;
            placeId = builder.placeId;
            refStr = builder.refStr;

            previewPropertyName = builder.previewPropertyName;
            action = builder.action;
            
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns></returns>
        public override AndroidJavaObject toAndroidJavaObject()
        {
            AndroidJavaObject javaObjectBuilder = new AndroidJavaObject("com.wa.sdk.social.model.WAShareOpenGraphContent$Builder");
            javaObjectBuilder.Call<AndroidJavaObject>("setMessage", message);
            javaObjectBuilder.Call<AndroidJavaObject>("setContentUri", Utils.uriToAndroidJavaObject(contentUri));
            javaObjectBuilder.Call<AndroidJavaObject>("setPeopleIds", Utils.stringListToAndroidJavaObject(peopleIds));
            javaObjectBuilder.Call<AndroidJavaObject>("setPlaceId", placeId);
            javaObjectBuilder.Call<AndroidJavaObject>("setRef", refStr);


            javaObjectBuilder.Call<AndroidJavaObject>("setPreviewPropertyName", previewPropertyName);
            javaObjectBuilder.Call<AndroidJavaObject>("setAction", action.toAndroidJavaObject());
            return javaObjectBuilder.Call<AndroidJavaObject>("build");
        }

        /// <summary>
        /// 分享开发图谱内容实体类Builder
        /// </summary>
        public class Builder : WAShareContentBuilder<WAShareOpenGraphContent, Builder>
        {
            public string previewPropertyName;
            public WAShareOpenGraphAction action;

            /// <summary>
            /// Sets the property name for the primary WAShareOpenGraphContent in the action.
            /// </summary>
            /// <param name="previewPropertyName">The property name for the preview object.</param>
            /// <returns>he builder.</returns>
            public Builder setPreviewPropertyName(string previewPropertyName)
            {
                this.previewPropertyName = previewPropertyName;
                return this;
            }


            /// <summary>
            /// Sets the Open Graph Action for the content.
            /// </summary>
            /// <param name="action"><see cref="WAShareOpenGraphAction"/></param>
            /// <returns>the builder</returns>
            public Builder setAction(WAShareOpenGraphAction action)
            {

                if (null == action)
                {
                    return this;
                }
                Dictionary<string, object> actionMap = action.getMap();
                if (null == actionMap || actionMap.Count == 0)
                {
                    return this;
                }
                WAShareOpenGraphAction.Builder builder = new WAShareOpenGraphAction.Builder()
                        .setActionType(action.getActionType());

                object obj;
                foreach (var item in actionMap)
                {
                    obj = item.Value;
                    if (null == obj)
                    {
                        continue;
                    }
                    if (obj is bool)
                    {
                        builder.putBool(item.Key, (bool)obj);
                    }
                    else if (obj is bool[])
                    {
                        builder.putBoolArray(item.Key, (bool[])obj);
                    }
                    else if (obj is double)
                    {
                        builder.putDouble(item.Key, (double)obj);
                    }
                    else if (obj is double[])
                    {
                        builder.putDoubleArray(item.Key, (double[])obj);
                    }
                    else if (obj is int)
                    {
                        builder.putInt(item.Key, (int)obj);
                    }
                    else if (obj is int[])
                    {
                        builder.putIntArray(item.Key, (int[])obj);
                    }
                    else if (obj is long)
                    {
                        builder.putLong(item.Key, (long)obj);
                    }
                    else if (obj is long[])
                    {
                        builder.putLongArray(item.Key, (long[])obj);
                    }
                    else if (obj is WAShareOpenGraphObject)
                    {
                        builder.putObject(item.Key, (WAShareOpenGraphObject)obj);
                    }
                    else if (obj is WASharePhoto)
                    {
                        builder.putPhoto(item.Key, (WASharePhoto)obj);
                    }
                    else if (obj is string)
                    {
                        builder.putString(item.Key, (string)obj);
                    }
                    else if (obj is List<WAShareOpenGraphObject>)
                    {
                        List<WAShareOpenGraphObject> list = (List<WAShareOpenGraphObject>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        builder.putObjectArrayList(item.Key, list);
                    }
                    else if (obj is List<string>)
                    {
                        List<string> list = (List<string>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        builder.putStringArrayList(item.Key, list);
                    }
                    else if (obj is List<WASharePhoto>)
                    {
                        List<WASharePhoto> list = (List<WASharePhoto>)obj;
                        if (list.Count == 0)
                        {
                            continue;
                        }
                        builder.putPhotoArrayList(item.Key, list);
                    }
                }
                builder.putAll(actionMap);
                WAShareOpenGraphAction fbAction = builder.build();
                this.action = fbAction;
                return this;
            }

            /// <summary>
            /// 构建一个<see cref="WAShareOpenGraphContent"/>对象
            /// </summary>
            /// <returns>WAShareOpenGraphContent object</returns>
            public override WAShareOpenGraphContent build()
            {
                return new WAShareOpenGraphContent(this);
            }
        }
    }
}
