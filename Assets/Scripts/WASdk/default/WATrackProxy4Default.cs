using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    class WATrackProxy4Default : WATrackProxy
    {
        /// <summary>
        /// 心跳开始，在Activity中调用, Unity忽略该接口
        /// </summary>
        public override void startHeartBeat()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 心跳结束，在Activity中调用, Unity忽略该接口
        /// </summary>
        public override void stopHeartBeat()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 数据收集
        /// </summary>
        /// <param name="event">事件数据实体类对象</param>
        public override void trackEvent(WAEvent waEvent)
        {
            throw new NotImplementedException();
        }
    }
}
