using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public class WAPlace
    {
        /// <summary>
        /// location ID
        /// </summary>
        public string pid
        {
            get;
            set;
        }

        /// <summary>
        /// location title
        /// </summary>
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// geographical latitude, in degrees (from -90 to 90)
        /// </summary>
        public double latitude
        {
            get;
            set;
        }

        /// <summary>
        /// geographical longitude, in degrees (from -180 to 180)
        /// </summary>
        public double longitude
        {
            get;
            set;
        }
        /// <summary>
        /// location type
        /// </summary>
        public string type
        {
            get;
            set;
        }
        /// <summary>
        /// country ID
        /// </summary>
        public string country
        {
            get;
            set;
        }
        /// <summary>
        /// city ID
        /// </summary>
        public string city
        {
            get;
            set;
        }
        /// <summary>
        /// address
        /// </summary>
        public string address
        {
            get;
            set;
        }

        /// <summary>
        /// 将AndroidJavaObject对象转成C#对象
        /// </summary>
        /// <param name="javaObject"></param>
        /// <returns></returns>
        public static WAPlace fromAndroidJavaObject(AndroidJavaObject javaObject)
        {
            if(null == javaObject)
            {
                return null;
            }
            WAPlace waPlace = new WAPlace();
            waPlace.pid = javaObject.Get<string>("pid");
            waPlace.title = javaObject.Get<string>("title");
            waPlace.latitude = javaObject.Get<double>("latitude");
            waPlace.longitude = javaObject.Get<double>("longitude");
            waPlace.type = javaObject.Get<string>("type");
            waPlace.country = javaObject.Get<string>("country");
            waPlace.city = javaObject.Get<string>("city");
            waPlace.address = javaObject.Get<string>("address");
            return waPlace;
        }
    }
}
