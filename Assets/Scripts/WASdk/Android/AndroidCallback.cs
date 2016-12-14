using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    /// <summary>
    /// Android平台的Callback
    /// </summary>
    internal abstract class AndroidCallback<T> : AndroidJavaProxy
    {
        protected WACallback<T> callback;
        public AndroidCallback(WACallback<T> callback) : base("com.wa.sdk.common.model.WACallback")
        {
            this.callback = callback;
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public void onSuccess(int code, string message, AndroidJavaObject result)
        {
            if (null != callback)
            {
                OnSuccess<T> onSuccess = new OnSuccess<T>(callback.onSuccess);
                onSuccess(code, message, parseResult(result));
            }
        }
        /// <summary>
        /// 成功回调，这个是防止message字段为null的时候，会当做Object来处理，导致找不到相应的回调方法
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public void onSuccess(int code, AndroidJavaObject message, AndroidJavaObject result)
        {
            if (null != callback)
            {
                OnSuccess<T> onSuccess = new OnSuccess<T>(callback.onSuccess);
                onSuccess(code, null == message ? "" : message.ToString(), parseResult(result));
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public void onCancel()
        {
            if (null != callback)
            {
                OnCancel onCancel = new OnCancel(callback.onCancel);
                onCancel();
            }
        }


        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public void onError(int code, string message, AndroidJavaObject result, AndroidJavaObject throwable)
        {
            if (null != callback)
            {
                OnError<T> onError = new OnError<T>(callback.onError);
                onError(code, message, parseResult(result));
            }
        }
        /// <summary>
        /// 错误回调，这个是防止message字段为null的时候，会当做Object来处理，导致找不到相应的回调方法
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public void onError(int code, AndroidJavaObject message, AndroidJavaObject result, AndroidJavaObject throwable)
        {
            if (null != callback)
            {
                OnError<T> onError = new OnError<T>(callback.onError);
                onError(code, null == message ? "" : message.ToString(), parseResult(result));
            }
        }

        protected abstract T parseResult(AndroidJavaObject result);
    }
}
