using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    public abstract class WAShareOpenGraphValueContainer
    {
        public Dictionary<string, object> map = new Dictionary<string, object>();

        /// <summary>
        /// Gets a value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The bool value.</returns>
        public object get(string key)
        {
            object value = null;
            map.TryGetValue(key, out value);
            return value;
        }

        /// <summary>
        /// Gets a bool value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="defaultValue">The value to return if no value is found for the specified key.</param>
        /// <returns>The bool value.</returns>
        public bool getBool(string key, bool defaultValue)
        {
            if (map.ContainsKey(key))
            {
                try
                {
                    object value = defaultValue;
                    if (map.TryGetValue(key, out value))
                    {
                        return (bool)value;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e.GetBaseException());
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets an array of bool values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The bool values.</returns>
        public bool[] getBoolArray(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (bool[])value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets a double value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="defaultValue">The value to return if no value is found for the specified key.</param>
        /// <returns>The double value.</returns>
        public double getDouble(string key, double defaultValue)
        {
            try
            {
                object value = defaultValue;
                if (map.TryGetValue(key, out value))
                {
                    return (double)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets an array of double values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The double values.</returns>
        public double[] getDoubleArray(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (double[])value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an int value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="defaultValue">The value to return if no value is found for the specified key.</param>
        /// <returns>The int value.</returns>
        public int getInt(string key, int defaultValue)
        {
            try
            {
                object value = defaultValue;
                if (map.TryGetValue(key, out value))
                {
                    return (int)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets an array of int values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The int values.</returns>
        public int[] getIntArray(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (int[])value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an long value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="defaultValue">The value to return if no value is found for the specified key.</param>
        /// <returns>The long value.</returns>
        public long getLong(string key, long defaultValue)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (long)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets an array of long values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returnsThe long values.></returns>
        public long[] getLongArray(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (long[])value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an object value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The object value.</returns>
        public WAShareOpenGraphObject getObject(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (WAShareOpenGraphObject)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an array of object values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The object values.</returns>
        public List<WAShareOpenGraphObject> getObjectArrayList(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (List<WAShareOpenGraphObject>)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets a photo value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The photo value.</returns>
        public WASharePhoto getPhoto(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (WASharePhoto)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an array of photo values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The photo values.</returns>
        public List<WASharePhoto> getPhotoArrayList(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (List<WASharePhoto>)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets a string value out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The string value.</returns>
        public string getString(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (string)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Gets an array of string values out of the object.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <returns>The string values.</returns>
        public List<string> getStringArrayList(string key)
        {
            try
            {
                object value = null;
                if (map.TryGetValue(key, out value))
                {
                    return (List<string>)value;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Returns the values in the container packaged in a map.
        /// </summary>
        /// <returns>A map with the values.</returns>
        public Dictionary<string, object> getMap()
        {
            return map;
        }

        /// <summary>
        /// 转换成AndroidJavaObject对象
        /// </summary>
        /// <returns>AndroidJavaObject对象</returns>
        public abstract AndroidJavaObject toAndroidJavaObject();


        /// <summary>
        /// WAShareOpenGraphValueContainer Builder抽象类
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <typeparam name="B"></typeparam>
        public abstract class Builder<P, B> where P : WAShareOpenGraphValueContainer where B : Builder<P, B>
        {
            public Dictionary<string, object> map = new Dictionary<string, object>();

            /// <summary>
            /// Sets a bool value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putBool(string key, bool value)
            {
                if(map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of bool values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putBoolArray(string key, bool[] value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets a double value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putDouble(string key, double value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of double values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putDoubleArray(string key, double[] value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an int value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putInt(string key, int value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of int values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putIntArray(string key, int[] value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets a long value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putLong(string key, long value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of long values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putLongArray(string key, long[] value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an object value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putObject(string key, WAShareOpenGraphObject value)
            {
                if (null == value)
                {
                    return (B)this;
                }
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                IDictionary<string, object> valueMap = value.map;

                if (null == valueMap || valueMap.Count < 1)
                {
                    return (B)this;
                }

                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of object values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putObjectArrayList(string key, List<WAShareOpenGraphObject> value)
            {
                if (null == value || value.Count == 0)
                {
                    return (B)this;
                }
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);

                return (B)this;
            }

            /// <summary>
            /// Sets a photo value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putPhoto(string key, WASharePhoto value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets an array of photo values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putPhotoArrayList(string key, List<WASharePhoto> value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets a string value in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putString(string key, string value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }

            /// <summary>
            /// Sets values in the object.
            /// </summary>
            /// <param name="valueMap">The key value map.</param>
            /// <returns>The builder.</returns>
            public B putAll(Dictionary<string, object> valueMap)
            {
                if (null != valueMap && valueMap.Count > 0)
                {
                    foreach (var item in valueMap)
                    {
                        if (map.ContainsKey(item.Key))
                        {
                            map.Remove(item.Key);
                        }
                        map.Add(item.Key, item.Value);
                    }
                }
                return (B)this;
            }

            /// <summary>
            /// Sets an array of string values in the object.
            /// </summary>
            /// <param name="key">The key for the value.</param>
            /// <param name="value">The value.</param>
            /// <returns>The builder.</returns>
            public B putStringArrayList(String key, List<string> value)
            {
                if (map.ContainsKey(key))
                {
                    map.Remove(key);
                }
                map.Add(key, value);
                return (B)this;
            }
        }
    }
}
