using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAPlayerLevelInfo
    {
        public WAPlayerLevel currentLevel
        {
            get;
            set;
        }
        public long currentXpTotal
        {
            get;
            set;
        }
        public long lastLevelUpTimestamp
        {
            get;
            set;
        }
        public WAPlayerLevel nextLevel
        {
            get;
            set;
        }
        public bool isMaxLevel
        {
            get;
            set;
        }

        public static WAPlayerLevelInfo fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAPlayerLevelInfo levelInfo = new WAPlayerLevelInfo();
            try
            {
                levelInfo.currentLevel = WAPlayerLevel.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("currentLevel"));
            }
            catch (Exception e)
            {
            }
            levelInfo.currentXpTotal = javaObject.Get<long>("currentXpTotal");
            levelInfo.lastLevelUpTimestamp = javaObject.Get<long>("lastLevelUpTimestamp");
            try
            {
                levelInfo.nextLevel = WAPlayerLevel.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("nextLevel"));
            }
            catch (Exception e)
            {
            }
            levelInfo.isMaxLevel = javaObject.Get<bool>("isMaxLevel");
            return levelInfo;

        }
    }
}
