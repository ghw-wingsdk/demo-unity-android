using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 检查更新（热更新）结果
    /// </summary>
    public class WACheckUpdateResult : WAResult
    {
        /// <summary>
        /// 是否有新补丁(true/false)
        /// </summary>
        public bool hasUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool forceUpdate
        {
            get;
            set;
        }

        /// <summary>
        /// 补丁包MD5
        /// </summary>
        public string md5
        {
            get;
            set;
        }

        /// <summary>
        /// 补丁ID
        /// </summary>
        public int patchId
        {
            get;
            set;
        }

        /// <summary>
        /// 补丁版本
        /// </summary>
        public int patchVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public string moduleId
        {
            get;
            set;
        }

        /// <summary>
        /// 下载链接地址
        /// </summary>
        public string downloadUrl
        {
            get;
            set;
        }
    }
}
