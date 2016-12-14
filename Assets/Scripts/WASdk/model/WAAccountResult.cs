using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 账户列表结果数据实体类（查询绑定账户等）
    /// </summary>
    public class WAAccountResult : WAResult
    {
        public List<WAAccount> accounts
        {
            get;
            set;
        }

        public void AddAccount(WAAccount account)
        {
            if(null == accounts)
            {
                accounts = new List<WAAccount>();
            }
            accounts.Add(account);
        }
    }
}
