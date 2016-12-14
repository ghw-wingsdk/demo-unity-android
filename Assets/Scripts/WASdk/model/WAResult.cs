using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// WA结果数据实体类基类
    /// </summary>
    public class WAResult
    {
        public int code
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
    }
}
