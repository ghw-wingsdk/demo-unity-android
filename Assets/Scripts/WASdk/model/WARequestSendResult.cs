using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WARequestSendResult : WAResult
    {
        /// <summary>
        /// 请求id
        /// </summary>
        public string requestId
        {
            get;
            set;
        }

        /// <summary>
        /// 接收者id列表，可能为空
        /// </summary>
        public List<string> recipients
        {
            get;
            set;
        }

        /// <summary>
        /// 添加一个接收者id
        /// </summary>
        /// <param name="id"></param>
        public void AddRecipient(string id)
        {
            if(null == recipients)
            {
                recipients = new List<string>();
            }
            recipients.Add(id);
        }

    }
}
