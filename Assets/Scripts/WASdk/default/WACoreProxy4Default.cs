using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WACoreProxy4Default : WACoreProxy
    {

        
        public override void initialize()
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置ClientId，这是人工干扰模式，一旦设置，只有清除缓存才能恢复原来的clientId
        /// </summary>
        /// <param name="clientId">clientId 客户端id</param>
        public override void setClientId(string clientId)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置WA SDK用户id（如果使用WA SDK的登录（含第三方平台登录），不需要手动设置）
        /// </summary>
        /// <param name="userId">用户id</param>
        public override void setUserId(string userId)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置游戏用户id
        /// </summary>
        /// <param name="gameUserId">游戏用户id</param>
        public override void setGameUserId(string gameUserId)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置server id
        /// </summary>
        /// <param name="serverId">服务器id</param>
        public override void setServerId(string serverId)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置用户等级
        /// </summary>
        /// <param name="level">用户等级</param>
        public override void setLevel(int level)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 设置调试模式，如果开启调试模式，可以在logcat中查看日志输出，默认关闭
        /// </summary>
        /// <param name="debugMode">是否开启调试模式</param>
        public override void setDebugMode(bool debugMode)
        {
            throw new NotImplementedException("Not supported");
        }

        /// <summary>
        /// 获取client id，根据特定的生成算法生成client id。
        /// </summary>
        /// <returns>当前设备的设备id（根据SDK的算法生成）</returns>
        public override string getClientId()
        {
            throw new NotImplementedException("Not supported");
        }
    }
}
