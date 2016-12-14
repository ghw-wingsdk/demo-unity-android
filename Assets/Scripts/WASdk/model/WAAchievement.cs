using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAAchievement
    {
        
        /// <summary>
        /// Constant returned by <see cref="state"/> indicating a hidden achievement.
        /// </summary>
        public const int STATE_HIDDEN = 2;

        /// <summary>
        /// Constant returned by <see cref="state"/> indicating a revealed achievement.
        /// </summary>
        public const int STATE_REVEALED = 1;
        
        /// <summary>
        /// Constant returned by <see cref="state"/> indicating a unlocked achievement.
        /// </summary>
        public const int STATE_UNLOCKED = 0;

        /// <summary>
        /// Constant returned by <see cref="type"/> indicating an incremental achievement.
        /// </summary>
        public const int TYPE_INCREMENTAL = 1;

        /// <summary>
        /// Constant returned by <see cref="type"/> indicating a standard achievement.
        /// </summary>
        public const int TYPE_STANDARD = 0;

        /// <summary>
        /// Retrieves the ID of this achievement.
        /// </summary>
        public string achievementId
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the name of this achievement.
        /// </summary>
        public string name
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the type of this achievement - one of TYPE_STANDARD or TYPE_INCREMENTAL.
        /// </summary>
        public int type
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the state of the achievement - one of STATE_UNLOCKED, STATE_REVEALED, or STATE_HIDDEN.
        /// </summary>
        public int state
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the number of steps this user has gone toward unlocking this achievement; only applicable for TYPE_INCREMENTAL achievement types.
        /// </summary>
        public int currentSteps
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the description for this achievement.
        /// </summary>
        public string description
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the number of steps this user has gone toward unlocking this achievement (formatted for the user's locale) into the given CharArrayBuffer.
        /// </summary>
        public string formattedCurrentSteps
        {
            get;
            set;
        }
        /// <summary>
        /// Loads the total number of steps necessary to unlock this achievement (formatted for the user's locale) into the given CharArrayBuffer; only applicable for TYPE_INCREMENTAL achievement types.
        /// </summary>
        public string formattedTotalSteps
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the timestamp (in millseconds since epoch) at which this achievement was last updated.
        /// </summary>
        public long lastUpdatedTimestamp
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the player information associated with this achievement.
        /// </summary>
        public WAPlayer player
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Uri revealedImageUri
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the total number of steps necessary to unlock this achievement; only applicable for TYPE_INCREMENTAL achievement types.
        /// </summary>
        public int totalSteps
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves a URI that can be used to load the achievement's unlocked image icon.
        /// </summary>
        public Uri unlockedImageUri
        {
            get;
            set;
        }
        /// <summary>
        /// Retrieves the XP value of this achievement.
        /// </summary>
        public long xpValue
        {
            get;
            set;
        }

        /// <summary>
        /// 将Android的AndroidJavaObject类型转换成C#类型的WAAchievemnt
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAAchievement fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if(null == javaObject)
            {
                return null;
            }
            WAAchievement achievement = new WAAchievement();
            achievement.achievementId = javaObject.Get<string>("achievementId");
            achievement.name = javaObject.Get<string>("name");
            achievement.type = javaObject.Get<int>("type");
            achievement.state = javaObject.Get<int>("state");
            achievement.currentSteps = javaObject.Get<int>("currentSteps");
            achievement.description = javaObject.Get<string>("description");
            achievement.formattedCurrentSteps = javaObject.Get<string>("formattedCurrentSteps");
            achievement.formattedTotalSteps = javaObject.Get<string>("formattedTotalSteps");
            achievement.lastUpdatedTimestamp = javaObject.Get<long>("lastUpdatedTimestamp");
            try
            {
                achievement.player = WAPlayer.fromAndroidJavaObject(javaObject.Get<AndroidJavaObject>("player"));
            }
            catch (Exception e)
            {
            }
            try
            {
                achievement.revealedImageUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("revealedImageUri"));
            }
            catch (Exception e)
            {
            }
            achievement.totalSteps = javaObject.Get<int>("totalSteps");
            try
            {
                achievement.unlockedImageUri = Utils.androidJavaObjectToUri(javaObject.Get<AndroidJavaObject>("unlockedImageUri"));
            }
            catch (Exception e)
            {
            }
            achievement.xpValue = javaObject.Get<long>("xpValue");
            return achievement;
        }
    }
}
