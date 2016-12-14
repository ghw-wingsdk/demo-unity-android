using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{

    public class WAUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string id
        {
            get;
            set;
        }

        public string name
        {
            get;
            set;
        }

        public string picture
        {
            get;
            set;
        }

        public string platform
        {
            get;
            set;
        }

        public string waUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否好友
        /// </summary>
        public bool isFriend
        {
            get;
            set;
        }

        /// <summary>
        /// 手机号
        /// </summary>
        public string phone
        {
            get;
            set;
        }

        /// <summary>
        /// 是否邀请过
        /// </summary>
        public bool isInvited
        {
            get;
            set;
        }

        /// <summary>
        /// 上次邀请时间
        /// </summary>
        public long lastInvitedTime
        {
            get;
            set;
        }

        /// <summary>
        /// 户是否玩过此游戏，1，玩过；0，没玩过
        /// </summary>
        public bool isPlayedThisGame
        {
            get;
            set;
        }

        /// <summary>
        /// 标识用户属于哪个分组，可取的值：contacts(来自通信录)recommend(推荐玩家)
        /// </summary>
        public string group
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject数据转换成C# 实体类
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAUser fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAUser waUser = new WAUser();
            waUser.id = javaObject.Get<string>("id");
            waUser.name = javaObject.Get<string>("name");
            waUser.picture = javaObject.Get<string>("picture");
            waUser.platform = javaObject.Get<string>("platform");
            waUser.waUserId = javaObject.Get<string>("ghwUserId");
            waUser.phone = javaObject.Get<string>("phone");
            waUser.isFriend = javaObject.Get<bool>("isFriend");
            waUser.isInvited = javaObject.Get<bool>("isInvited");
            waUser.lastInvitedTime = javaObject.Get<long>("lastInvitedTime");
            waUser.isPlayedThisGame = javaObject.Get<bool>("isPlayedThisGame");
            waUser.group = javaObject.Get<string>("group");
            return waUser;
        }
    }
}
