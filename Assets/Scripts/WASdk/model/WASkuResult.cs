using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// 查询库存结果数据实体类
    /// </summary>
    public class WASkuResult : WAResult
    {
        /// <summary>
        /// 商品库存信息列表
        /// </summary>
        public List<WASkuDetails> skuList
        {
            get;
            set;
        }

        /// <summary>
        /// 增加一个WASkuDetails对象
        /// </summary>
        /// <param name="sku"></param>
        public void AddSku(WASkuDetails sku)
        {
            if (null == skuList)
            {
                skuList = new List<WASkuDetails>();
            }
            skuList.Add(sku);
        }

    }
}
