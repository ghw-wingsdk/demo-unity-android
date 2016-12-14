using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAPlayerLevel
    {
        public int levelNumber
        {
            get;
            set;
        }
        public long maxXp
        {
            get;
            set;
        }
        public long minXp
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject对象转换成C#的对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAPlayerLevel fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if (null == javaObject)
            {
                return null;
            }
            WAPlayerLevel level = new WAPlayerLevel();
            level.levelNumber = javaObject.Get<int>("levelNumber");
            level.maxXp = javaObject.Get<long>("maxXp");
            level.minXp = javaObject.Get<long>("minXp");
            return level;
        }
    }
}
