using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WAEvent
    {
        /// <summary>
        /// 默认事件名称
        /// </summary>
        public string defaultEventName;
        /// <summary>
        /// 默认数据统计值
        /// </summary>
        public float defaultValue = 0.0f;
        /// <summary>
        /// 默认事件参数名-参数值表
        /// </summary>
        public Dictionary<string, object> defaultEventValues;
        /// <summary>
        /// 渠道开关表
        /// </summary>
        public Dictionary<string, bool> channelSwitchMap;
        /// <summary>
        /// 事件名称表，渠道自定义事件名称，key为渠道名称
        /// </summary>
        public Dictionary<string, string> eventNameMap;
        /// <summary>
        /// 事件统计值表，渠道自定义事件统计值，key为渠道名称
        /// </summary>
        public Dictionary<string, float> valueMap;
        /// <summary>
        /// 事件参数名-参数值表（三维表），渠道自定义的参数名参数值， key为渠道名称，value为该渠道的参数名-参数值表
        /// </summary>
        public Dictionary<string, Dictionary<string, object>> eventValueMap;

        protected WAEvent(Builder builder)
        {
            this.defaultEventName = builder.defaultEventName;
            this.defaultValue = builder.defaultValue;
            this.defaultEventValues = builder.defaultEventValues;

            this.channelSwitchMap = builder.channelSwitchMap;
            this.eventNameMap = builder.eventNameMap;
            this.valueMap = builder.valueMap;
            this.eventValueMap = builder.eventValueMap;
        }

        private WAEvent()
        {

        }

        /// <summary>
        /// 渠道是否已启用
        /// </summary>
        /// <param name="eventChannel">渠道</param>
        /// <returns>rue为启用，false为禁用，如果没有设置，默认true</returns>
        public bool isChannelEnabled(string eventChannel)
        {
            if (null == eventChannel)
            {
                return true;
            }
            bool value = true;
            if (channelSwitchMap.TryGetValue(eventChannel, out value))
            {
                return value;
            }
            return true;
        }

        /// <summary>
        /// 获取默认事件名称
        /// </summary>
        /// <returns>默认事件名称</returns>
        public string getDefaultEventName()
        {
            return defaultEventName;
        }

        /// <summary>
        /// 获取渠道事件名称
        /// </summary>
        /// <param name="eventChannel">事件渠道</param>
        /// <returns>指定渠道的事件名称，如果没有返回默认的事件名称</returns>
        public string getChannelEventName(string eventChannel)
        {
            string channelEventName;
            return eventNameMap.TryGetValue(eventChannel, out channelEventName) ? channelEventName : defaultEventName;
        }

        /// <summary>
        /// 判断某个事件名称是否默认事件名称
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns>是否为默认的事件名称</returns>
        public bool isDefaultEventName(string eventName)
        {
            return !eventNameMap.ContainsKey(eventName);
        }

        /// <summary>
        /// 获取默认统计值
        /// </summary>
        /// <returns>默认统计值</returns>
        public float getDefaultValue()
        {
            return defaultValue;
        }

        /// <summary>
        /// 获取渠道统计值
        /// </summary>
        /// <param name="eventChannel">渠道名称</param>
        /// <returns>渠道指定统计值</returns>
        public float getChannelValue(string eventChannel)
        {
            float value = 0.0f;
            return valueMap.TryGetValue(eventChannel, out value) ? value : defaultValue;
        }

        /// <summary>
        /// 获取渠道事件统计参数
        /// </summary>
        /// <param name="eventChannel">渠道名称</param>
        /// <returns>指定渠道的事件统计参数</returns>
        public Dictionary<string, object> getChannelEventValues(string eventChannel)
        {
            Dictionary<string, object> channelEventValues;
            return eventValueMap.TryGetValue(eventChannel, out channelEventValues) ? channelEventValues : null;
        }

        /// <summary>
        /// 发送当前事件
        /// </summary>
        public void track()
        {
            WATrackProxy.Instance.trackEvent(this);
        }


        /// <summary>
        /// WAEvent 构建类
        /// </summary>
        public class Builder
        {
            public string defaultEventName;
            public float defaultValue = 0.0f;

            public Dictionary<string, object> defaultEventValues = new Dictionary<string, object>();

            public Dictionary<string, bool> channelSwitchMap = new Dictionary<string, bool>();
            public Dictionary<string, string> eventNameMap = new Dictionary<string, string>();
            public Dictionary<string, float> valueMap = new Dictionary<string, float>();
            public Dictionary<string, Dictionary<string, object>> eventValueMap = new Dictionary<string, Dictionary<string, object>>();

            /// <summary>
            /// 设置默认的事件名称
            /// </summary>
            /// <param name="eventName">事件名称</param>
            /// <returns>当前builder对象</returns>
            public Builder setDefaultEventName(string eventName)
            {
                this.defaultEventName = eventName;
                return this;
            }

            /// <summary>
            /// 设置默认的统计数据值
            /// </summary>
            /// <param name="value">统计值</param>
            /// <returns>当前builder对象</returns>
            public Builder setDefaultValue(float value)
            {
                this.defaultValue = value;
                return this;
            }

            /// <summary>
            /// 设置默认的事件参数-值
            /// </summary>
            /// <param name="eventValues">事件参数</param>
            /// <returns>当前builder对象</returns>
            public Builder setDefaultEventValues(Dictionary<string, object> eventValues)
            {
                if (null != eventValues && eventValues.Count > 0)
                {
                    defaultEventValues = eventValues;
                }
                return this;
            }

            /// <summary>
            /// 增加一个默认的事件参数-值
            /// </summary>
            /// <param name="paramName">参数名</param>
            /// <param name="paramValue">参数值</param>
            /// <returns>当前builder对象</returns>
            public Builder addDefaultEventValue(string paramName, object paramValue)
            {
                if (defaultEventValues.ContainsKey(paramName))
                {
                    defaultEventValues.Remove(paramName);
                }
                defaultEventValues.Add(paramName, paramValue);
                return this;
            }

            /// <summary>
            /// 设置渠道事件参数-值
            /// </summary>
            /// <param name="eventChannel">渠道名称</param>
            /// <param name="eventValues">事件统计参数Map</param>
            /// <returns>当前builder对象</returns>
            public Builder setChannelEventValues(string eventChannel, Dictionary<string, object> eventValues)
            {
                if (null == eventChannel)
                {
                    return this;
                }
                if (eventValueMap.ContainsKey(eventChannel))
                {
                    eventValueMap.Remove(eventChannel);
                }
                eventValueMap.Add(eventChannel, eventValues);
                return this;
            }

            /// <summary>
            /// 新增渠道事件参数-值
            /// </summary>
            /// <param name="eventChannel">渠道名称</param>
            /// <param name="paramName">参数名</param>
            /// <param name="paramValue">参数值</param>
            /// <returns>当前builder对象</returns>
            public Builder addChannelEventValue(string eventChannel, string paramName, object paramValue)
            {
                if (null == eventChannel)
                {
                    return this;
                }

                Dictionary<string, object> valueMap;
                if (eventValueMap.TryGetValue(eventChannel, out valueMap))
                {
                    eventValueMap.Remove(eventChannel);
                    if (valueMap.ContainsKey(paramName))
                    {
                        valueMap.Remove(paramName);
                    }
                }
                else
                {
                    valueMap = new Dictionary<string, object>();
                }
                valueMap.Add(paramName, paramValue);
                eventValueMap.Add(eventChannel, valueMap);
                return this;
            }

            /// <summary>
            /// 设置渠道事件名称
            /// </summary>
            /// <param name="eventChannel">渠道名称</param>
            /// <param name="eventName">事件名称</param>
            /// <returns>当前builder对象</returns>
            public Builder setChannelEventName(string eventChannel, string eventName)
            {
                if (null == eventChannel)
                {
                    return this;
                }
                if (eventNameMap.ContainsKey(eventChannel))
                {
                    eventNameMap.Remove(eventChannel);
                }
                eventNameMap.Add(eventChannel, eventName);
                return this;
            }

            /// <summary>
            /// 设置渠道统计数据值
            /// </summary>
            /// <param name="eventChannel">渠道名称</param>
            /// <param name="value">统计值</param>
            /// <returns>当前builder对象</returns>
            public Builder setChannelValue(string eventChannel, float value)
            {
                if (null == eventChannel)
                {
                    return this;
                }
                if (valueMap.ContainsKey(eventChannel))
                {
                    valueMap.Remove(eventChannel);
                }
                valueMap.Add(eventChannel, value);
                return this;
            }

            /// <summary>
            /// 禁用渠道，禁用后，这个事件将不会发送到这个渠道
            /// </summary>
            /// <param name="eventChannel">渠道名称</param>
            /// <returns>当前builder对象</returns>
            public Builder disableChannel(string eventChannel)
            {
                if (null == eventChannel)
                {
                    return this;
                }
                if (channelSwitchMap.ContainsKey(eventChannel))
                {
                    channelSwitchMap.Remove(eventChannel);
                }
                channelSwitchMap.Add(eventChannel, false);
                return this;
            }

            /// <summary>
            /// 构建一个WAEvent对象
            /// </summary>
            /// <returns>WAEvent对象</returns>
            public WAEvent build()
            {
                return new WAEvent(this);
            }

        }
    }
}
