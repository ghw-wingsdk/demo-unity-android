using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// Facebook 游戏请求结果数据实体类
    /// </summary>
    public class WAFBGameRequestResult
    {
        /// <summary>
        /// request数据
        /// </summary>
        public List<WAFBGameRequestData> requests
        {
            get;
            set;
        }

        public void addRequests(WAFBGameRequestData data)
        {
            if(null == requests)
            {
                requests = new List<WAFBGameRequestData>();
            }
            requests.Add(data);
        }
    }
}
