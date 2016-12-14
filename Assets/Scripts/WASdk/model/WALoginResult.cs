using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 登录结果实体类
    /// </summary>
    public class WALoginResult : WAResult
    {
        public string waUserId
        {
            get;
            set;
        }
        public string waToken
        {
            get;
            set;
        }
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
