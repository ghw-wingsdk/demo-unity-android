using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// Android平台支付接口代理类
    /// </summary>
    internal class WAPayProxy4Android : WAPayProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass payProxy = null;

        public WAPayProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            payProxy = new AndroidJavaClass("com.wa.sdk.pay.WAPayProxy");
        }

        /// <summary>
        /// 支付服务是否可用
        /// </summary>
        /// <returns>支付服务是否可用，true为可用，false为不可用</returns>
        public override bool isPayServiceAvailable()
        {
            if (null == androidJavaClass && null == payProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class com.wa.sdk.appwall.WAApwProxy failed!");
                return false ;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            return payProxy.CallStatic<bool>("isPayServiceAvailable", activity);
        }

        /// <summary>
        /// 对所有已经选择的支付渠道初始化
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void initialize(WACallback<WAResult> callback)
        {
            if (null == androidJavaClass && null == payProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                payProxy.CallStatic("initialize", activity, new InternalInitCallback(callback));
            }));
        }

        /// <summary>
        /// 查询库存
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void queryInventory(WACallback<WASkuResult> callback)
        {
            if (null == androidJavaClass && null == payProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!");
                if (null != callback)
                {
                    OnError<WASkuResult> onError = new OnError<WASkuResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                payProxy.CallStatic("queryInventory", new InternalQuerySkuCallback(callback));
            }));
        }

        /// <summary>
        /// 有支付页面的支付接口，可能包含原生支付和webview支付
        /// </summary>
        /// <param name="waProductId">WA商品id</param>
        /// <param name="extInfo">额外信息</param>
        /// <param name="callback">回调，结果返回</param>
        public override void payUI(string waProductId, string extInfo, WACallback<WAPurchaseResult> callback)
        {
            if (null == androidJavaClass && null == payProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!");
                if (null != callback)
                {
                    OnError<WAPurchaseResult> onError = new OnError<WAPurchaseResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.pay.WAPayProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                payProxy.CallStatic("payUI", activity, waProductId, extInfo, new InternalPayCallback(callback));
            }));
        }


        /// <summary>
        /// 清理所有已选择的支付渠道
        /// </summary>
        public override void onDestroy()
        {
            if (null != androidJavaClass && null != payProxy)
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    payProxy.CallStatic("onDestroy");
                }));
            }
        }
    }


    /// <summary>
    /// 初始化内部回调，中转结果
    /// </summary>
    internal class InternalInitCallback : AndroidCallback<WAResult>
    {
        public InternalInitCallback(WACallback<WAResult> callback) : base(callback)
        {
        }

        protected override WAResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAResult initResult = new WAResult();
            initResult.code = result.Get<int>("code");
            initResult.message = result.Get<string>("message");
            return initResult;
        }
    }


    /// <summary>
    /// 查询商品库存列表回调，内部使用，中转结果
    /// </summary>
    internal class InternalQuerySkuCallback : AndroidCallback<WASkuResult>
    {
        public InternalQuerySkuCallback(WACallback<WASkuResult> callback) : base(callback)
        {
        }

        protected override WASkuResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }

            WASkuResult skuResult = new WASkuResult();
            skuResult.code = result.Get<int>("code");
            skuResult.message = result.Get<string>("message");
            AndroidJavaObject skuListObject = result.Get<AndroidJavaObject>("skuList");
            if (null != skuListObject)
            {
                int size = skuListObject.Call<int>("size");
                AndroidJavaObject skuObject;
                WASkuDetails sku;
                for (int i = 0; i < size; i++)
                {
                    skuObject = skuListObject.Call<AndroidJavaObject>("get", i);
                    if (null != skuObject)
                    {
                        sku = new WASkuDetails();
                        sku.itemType = skuObject.Get<string>("mItemType");
                        sku.sku = skuObject.Get<string>("mSku");
                        sku.type = skuObject.Get<string>("mType");
                        sku.price = skuObject.Get<string>("mPrice");
                        sku.title = skuObject.Get<string>("mTitle");
                        sku.description = skuObject.Get<string>("mDescription");
                        sku.json = skuObject.Get<string>("mJson");
                        sku.priceCurrencyCode = skuObject.Get<string>("mPriceCurrencyCode");
                        sku.priceAmountMicros = skuObject.Get<string>("mPriceAmountMicros");
                        sku.defaultCurrency = skuObject.Get<string>("mDefaultCurrency");
                        sku.defaultAmountMicro = skuObject.Get<long>("mDefaultAmountMicro");
                        sku.virtualCoinAmount = skuObject.Get<long>("mVirtualCoinAmount");
                        sku.virtualCurrency = skuObject.Get<string>("mVirtualCurrency");
                        skuResult.AddSku(sku);
                    }
                }
            }
            return skuResult;
        }
    }

    /// <summary>
    /// 支付回调，内部使用，结果中转
    /// </summary>
    internal class InternalPayCallback : AndroidCallback<WAPurchaseResult>
    {

        public InternalPayCallback(WACallback<WAPurchaseResult> callback) : base(callback)
        {
        }

        protected override WAPurchaseResult parseResult(AndroidJavaObject result)
        {
            if(null == result)
            {
                return null;
            }
            WAPurchaseResult purchaseResult = new WAPurchaseResult();
            purchaseResult.code = result.Get<int>("code");
            purchaseResult.message = result.Get<string>("message");
            purchaseResult.itemType = result.Get<string>("mItemType");
            purchaseResult.orderId = result.Get<string>("mOrderId");
            purchaseResult.orderId = result.Get<string>("mPackageName");
            purchaseResult.packageName = result.Get<string>("mSku");
            purchaseResult.purchaseTime = result.Get<long>("mPurchaseTime");
            purchaseResult.purchaseState = result.Get<int>("mPurchaseState");
            purchaseResult.developerPayload = result.Get<string>("mDeveloperPayload");
            purchaseResult.token = result.Get<string>("mToken");
            purchaseResult.originalJson = result.Get<string>("mOriginalJson");
            purchaseResult.signature = result.Get<string>("mSignature");
            purchaseResult.priceCurrencyCode = result.Get<string>("mPriceCurrencyCode");
            purchaseResult.priceAmountMicros = result.Get<long>("mPriceAmountMicros");
            purchaseResult.defaultCurrency = result.Get<string>("mDefaultCurrency");
            purchaseResult.defaultAmountMicro = result.Get<long>("mDefaultAmountMicro");
            purchaseResult.virtualCoinAmount = result.Get<long>("mVirtualCoinAmount");
            purchaseResult.virtualCurrency = result.Get<string>("mVirtualCurrency");
            purchaseResult.platform = result.Get<string>("mPlatform");
            purchaseResult.wAProductId = result.Get<string>("mWAProductId");
            purchaseResult.extInfo = result.Get<string>("mExtInfo");
            return purchaseResult;
        }
    }
}
