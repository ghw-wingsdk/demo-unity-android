using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// WAShareContentBuilder抽象类
    /// </summary>
    /// <typeparam name="C">需要创建的对象的类</typeparam>
    /// <typeparam name="B">Builder类型</typeparam>
    public abstract class WAShareContentBuilder<C, B> where C : WAShareContent where B : WAShareContentBuilder<C, B>
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

        /**
         * Set the content message
         * @param message
         * @return
         */
        public B setMessage(string message)
        {
            this.message = message;
            return (B)this;
        }

        /**
         * Set the URL for the content being shared.
         *
         * @param contentUri {@link Uri} representation of the content link.
         * @return The builder.
         */
        public B setContentUri(Uri contentUri)
        {
            this.contentUri = contentUri;
            return (B)this;
        }

        /**
         * Set the list of Ids for taggable people to tag with this content.
         *
         * @param peopleIds {@link List} of Ids for people to tag.
         * @return The builder.
         */
        public B setPeopleIds(List<string> peopleIds)
        {
            this.peopleIds = peopleIds == null ? null : peopleIds;
            return (B)this;
        }

        /**
         * Set the Id for a place to tag with this content.
         *
         * @param placeId The Id for the place to tag.
         * @return The builder.
         */
        public B setPlaceId(string placeId)
        {
            this.placeId = placeId;
            return (B)this;
        }

        /**
         * Set the value to be added to the referrer URL when a person follows a link from this
         * shared content on feed.
         *
         * @param ref The ref for the content.
         * @return The builder.
         */
        public B setRef(string refStr)
        {
            this.refStr = refStr;
            return (B)this;
        }

        public abstract C build();
    }
}
