using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 账户信息数据实体类
    /// </summary>
    public class WAAccount
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        public string platform
        {
            get;
            set;
        }

        /// <summary>
        /// 平台用户id
        /// </summary>
        public string platformUserId
        {
            get;
            set;
        }

    }
}
