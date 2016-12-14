using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WASdkAPI
{
    public class WASkuDetails
    {
        /// <summary>
        /// 
        /// </summary>
        public string itemType
        {
            get;
            set;
        }

        /// <summary>
        /// 商品id
        /// </summary>
        public string sku
        {
            get;
            set;
        }

        /// <summary>
        /// 商品类型
        /// </summary>
        public string type
        {
            get;
            set;
        }

        /// <summary>
        /// 商品价格，仅仅是展示价格，可能会带有格式化字符，不建议用来计算操作
        /// </summary>
        public string price
        {
            get;
            set;
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string title
        {
            get;
            set;
        }

        /// <summary>
        /// 商品描述
        /// </summary>
        public string description
        {
            get;
            set;
        }

        /// <summary>
        /// 原始json数据
        /// </summary>
        public string json
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
        public string priceAmountMicros
        {
            get;
            set;
        }

        /// <summary>
        /// 基准货币类型
        /// </summary>
        public string defaultCurrency
        {
            get;
            set;
        }

        /// <summary>
        /// 基准货币数量
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
    }
}
