using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    /// <summary>
    /// 支付结果数据实体类
    /// </summary>
    public class WAPurchaseResult : WAResult
    {
        /// <summary>
        /// ITEM_TYPE_INAPP or ITEM_TYPE_SUBS
        /// </summary>
        public string itemType
        {
            get;
            set;
        }
        public string orderId
        {
            get;
            set;
        }
        public string packageName
        {
            get;
            set;
        }
        public string sku
        {
            get;
            set;
        }
        public long purchaseTime
        {
            get;
            set;
        }

        /// <summary>
        /// 支付平台返回的状态
        /// </summary>
        public int purchaseState
        {
            get;
            set;
        }
        public string developerPayload
        {
            get;
            set;
        }
        public string token
        {
            get;
            set;
        }
        public string originalJson
        {
            get;
            set;
        }
        public string signature
        {
            get;
            set;
        }

        /// <summary>
        /// 商品信息货币类型（本地货币）
        /// </summary>
        public string priceCurrencyCode
        {
            get;
            set;
        }

        /// <summary>
        /// 商品信息货币数量（本地货币）
        /// </summary>
        public long priceAmountMicros
        {
            get;
            set;
        }

        /// <summary>
        /// 商品信息货币类型（基准货币）
        /// </summary>
        public string defaultCurrency
        {
            get;
            set;
        }

        /// <summary>
        /// 商品信息货币数量（基准货币）
        /// </summary>
        public long defaultAmountMicro
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟货币数量
        /// </summary>
        public long virtualCoinAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟货币币种
        /// </summary>
        public string virtualCurrency
        {
            get;
            set;
        }

        /// <summary>
        /// 支付平台名称
        /// </summary>
        public string platform
        {
            get;
            set;
        }

        /// <summary>
        /// WA平台商品id
        /// </summary>
        public string wAProductId
        {
            get;
            set;
        }

        public string extInfo
        {
            get;
            set;
        }
    }
}
