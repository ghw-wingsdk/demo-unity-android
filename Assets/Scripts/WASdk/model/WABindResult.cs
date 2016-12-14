using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 绑定平台账户结果实体类
    /// </summary>
    public class WABindResult : WAResult
    {
        public string platformUserId
        {
            get;
            set;
        }
        public string platformToken
        {
            get;
            set;
        }
        public string platform
        {
            get;
            set;
        }
    }
}
