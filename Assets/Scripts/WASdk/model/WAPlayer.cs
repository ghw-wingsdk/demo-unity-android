using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAPlayer
    {
        public string playerId
        {
            get;
            set;
        }
        public string displayName
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }
        public string title
        {
            get;
            set;
        }
        public WAPlayerLevelInfo levelInfo
        {
            get;
            set;
        }
        public Uri bannerImageLandScapeUri
        {
            get;
            set;
        }
        public string bannerImageLandScapeUrl
        {
            get;
            set;
        }
        public Uri bannerImagePortraitUri
        {
            get;
            set;
        }
        public string bannerImagePortraitUrl
        {
            get;
            set;
        }
        public Uri hiResImageUri
        {
            get;
            set;
        }
        public string hiResImageUrl
        {
            get;
            set;
        }
        public Uri iconImageUri
        {
            get;
            set;
        }
        public string iconImageUrl
        {
            get;
            set;
        }
        public long lastPlayedWithTimestamp
        {
            get;
            set;
        }
        public long retrievedTimestamp
        {
            get;
            set;
        }
        public bool hasHiResImage
        {
            get;
            set;
        }
        public bool hasIconImage
        {
            get;
            set;
        }
        public string platform
        {
            get;
            set;
        }

        public static WAPlayer fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAPlayer waPlayer = new WAPlayer();
            waPlayer.playerId = javaObject.Get<string>("playerId");
            waPlayer.displayName = javaObject.Get<string>("displayName");
            waPlayer.name = javaObject.Get<string>("name");
            waPlayer.title = javaObject.Get<string>("title");
            try
            {
                waPlayer.levelInfo = WAPlayerLevelInfo.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("levelInfo"));
            }
            catch (Exception e)
            {
            }
            try
            {
                waPlayer.bannerImageLandScapeUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("bannerImageLandScapeUri"));
            }
            catch (Exception e)
            {
            }
            waPlayer.bannerImageLandScapeUrl = javaObject.Get<string>("bannerImageLandScapeUrl");
            try
            {
                waPlayer.bannerImagePortraitUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("bannerImagePortraitUri"));
            }
            catch (Exception e)
            {
            }
            waPlayer.bannerImagePortraitUrl = javaObject.Get<string>("bannerImagePortraitUrl");
            try
            {
                waPlayer.hiResImageUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("hiResImageUri"));
            }
            catch (Exception e)
            {
            }
            waPlayer.hiResImageUrl = javaObject.Get<string>("hiResImageUrl");
            try
            {
                waPlayer.iconImageUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("iconImageUri"));
            }
            catch (Exception e)
            {
            }
            waPlayer.iconImageUrl = javaObject.Get<string>("iconImageUrl");
            waPlayer.lastPlayedWithTimestamp = javaObject.Get<long>("lastPlayedWithTimestamp");
            waPlayer.retrievedTimestamp = javaObject.Get<long>("retrievedTimestamp");
            waPlayer.hasHiResImage = javaObject.Get<bool>("hasHiResImage");
            waPlayer.hasIconImage = javaObject.Get<bool>("hasIconImage");
            waPlayer.platform = javaObject.Get<string>("platform");

            return waPlayer;
        }
    }


}
