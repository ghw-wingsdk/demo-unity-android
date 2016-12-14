using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAGroup
    {
        /// <summary>
        /// Community ID.
        /// </summary>
        public string gid
        {
            get;
            set;
        }
        /// <summary>
        /// Community name.
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        /// Screen name of the community page (e.g. apiclub or club1).
        /// </summary>
        public string screen_name
        {
            get;
            set;
        }
        /// <summary>
        /// Whether the community is closed (0 — open, 1 — closed, 2 — public).
        /// </summary>
        public int is_closed
        {
            get;
            set;
        }
        /// <summary>
        /// Whether a user is the community manager (0 — is not the manager, 1 — is the manager).
        /// </summary>
        public int is_admin
        {
            get;
            set;
        }
        /// <summary>
        /// (If is_admin=1) Rights of the user (1 — moderator, 2 — editor, 3 — administrator).
        /// </summary>
        public int admin_level
        {
            get;
            set;
        }
        /// <summary>
        /// Whether a user is a community member (0 — is not а member, 1 — is a member).
        /// </summary>
        public int is_member
        {
            get;
            set;
        }
        /// <summary>
        /// Community type (group — group, page — public page, event — event).
        /// </summary>
        public string type
        {
            get;
            set;
        }
        /// <summary>
        /// URL of the 50px-wide community logo.
        /// </summary>
        public string photo
        {
            get;
            set;
        }
        /// <summary>
        /// URL of the 100px-wide community logo.
        /// </summary>
        public string photo_medium
        {
            get;
            set;
        }
        /// <summary>
        /// URL of the 200px-wide community logo.
        /// </summary>
        public string photo_big
        {
            get;
            set;
        }
        /// <summary>
        /// ID of the city specified in information about community. Returns city ID that can be used to get its name using places.getCityById method. If city is not specified, "0" is returned.
        /// </summary>
        public WAPlace city
        {
            get;
            set;
        }
        /// <summary>
        /// ID of the country specified in information about community. Returns country ID that can be used to get its name using places.getCountryById method. If country is not specified, 0 is returned.
        /// </summary>
        public WAPlace country
        {
            get;
            set;
        }
        /// <summary>
        /// ID of the location specified in information about community. Returns an object containing the following fields:
        /// </summary>
        public WAPlace place
        {
            get;
            set;
        }
        /// <summary>
        /// Community description text.
        /// </summary>
        public string description
        {
            get;
            set;
        }
        /// <summary>
        /// Name of the home wiki-page of the community.
        /// </summary>
        public string wiki_page
        {
            get;
            set;
        }
        /// <summary>
        /// Number of community members.
        /// </summary>
        public int members_count
        {
            get;
            set;
        }
        /// <summary>
        /// Whether the current user can post on the community's wall (0 — cannot, 1 — can).
        /// </summary>
        public int can_post
        {
            get;
            set;
        }
        /// <summary>
        /// Whether others' posts on the community's wall can be viewed (0 — cannot be viewed, 1 — can be viewed).
        /// </summary>
        public int can_see_all_posts
        {
            get;
            set;
        }
        /// <summary>
        /// Returns the public page status bar. For groups, a string value is returned whether the group is public or not; for events — start date.
        /// </summary>
        public string activity
        {
            get;
            set;
        }
        /// <summary>
        /// Group status. Returns a string with status text that is on the group page below its name.
        /// </summary>
        public string status
        {
            get;
            set;
        }
        /// <summary>
        /// Information from public page contact module.
        /// </summary>
        public string contacts
        {
            get;
            set;
        }
        /// <summary>
        /// platform
        /// </summary>
        public string platform
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject对象转成C#对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAGroup fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if(null == javaObject || null == javaObject.GetRawObject())
            {
                return null;
            }
            WAGroup waGroup = new WAGroup();
            waGroup.gid = javaObject.Get<string>("gid");
            waGroup.name = javaObject.Get<string>("name");
            waGroup.screen_name = javaObject.Get<string>("screen_name");
            waGroup.is_closed = javaObject.Get<int>("is_closed");
            waGroup.is_admin = javaObject.Get<int>("is_admin");
            waGroup.admin_level = javaObject.Get<int>("admin_level");
            waGroup.is_member = javaObject.Get<int>("is_member");
            waGroup.type = javaObject.Get<string>("type");
            waGroup.photo = javaObject.Get<string>("photo");
            waGroup.photo_medium = javaObject.Get<string>("photo_medium");
            waGroup.photo_big = javaObject.Get<string>("photo_big");
            try
            {
                waGroup.city = WAPlace.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("city"));
            }
            catch (Exception e)
            {

            }
            try
            {
                waGroup.country = WAPlace.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("country"));
            }
            catch (Exception e)
            {

            }
            try
            {
                waGroup.place = WAPlace.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("place"));
            }
            catch (Exception e)
            {

            }
            waGroup.description = javaObject.Get<string>("description");
            waGroup.wiki_page = javaObject.Get<string>("wiki_page");
            waGroup.members_count = javaObject.Get<int>("members_count");
            waGroup.can_post = javaObject.Get<int>("can_post");
            waGroup.can_see_all_posts = javaObject.Get<int>("can_see_all_posts");
            waGroup.activity = javaObject.Get<string>("activity");
            waGroup.status = javaObject.Get<string>("status");
            waGroup.contacts = javaObject.Get<string>("contacts");
            waGroup.platform = javaObject.Get<string>("platform");
            return waGroup;
        }
    }
}
