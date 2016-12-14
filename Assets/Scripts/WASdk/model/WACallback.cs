using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{

    /// <summary>
    /// 成功回调方法委托
    /// </summary>
    /// <param name="code">标识码</param>
    /// <param name="message">描述文字</param>
    /// <param name="result">结果数据</param>
    delegate void OnSuccess<T>(int code, string message, T result);

    /// <summary>
    /// 取消回调方法委托
    /// </summary>
    delegate void OnCancel();

    /// <summary>
    /// 失败回调方法委托
    /// </summary>
    /// <param name="code">标识码</param>
    /// <param name="message">描述文字</param>
    /// <param name="result">结果数据，大多数情况下为null</param>
    delegate void OnError<T>(int code, string message, T result);

    public abstract class WACallback<T>
    {
        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public abstract void onSuccess(int code, string message, T result);

        /// <summary>
        /// 取消
        /// </summary>
        public abstract void onCancel();


        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public abstract void onError(int code, string message, T result);
    }

    /// <summary>
    /// 登录账号发生改变回调方法委托
    /// </summary>
    /// <param name="currentAccount"></param>
    delegate void OnLoginAccountChanged(WALoginResult currentAccount);
    /// <summary>
    /// 绑定账户发生改变回调方法委托
    /// </summary>
    /// <param name="binding">是否绑定账户操作</param>
    /// <param name="result">结果</param>
    delegate void OnBoundAccountChanged(bool binding, WABindResult result);

    /// <summary>
    /// 账户管理回调
    /// </summary>
    public interface WAAccountCallback
    {

        /// <summary>
        /// 当前登录的账户发生改变
        /// </summary>
        /// <param name="currentAccount">当前登录账户</param>
        void onLoginAccountChanged(WALoginResult currentAccount);

        /// <summary>
        /// 账号绑定状态发生改变
        /// </summary>
        /// <param name="binding">是否为绑定账户</param>
        /// <param name="result">绑定结果</param>
        void onBoundAccountChanged(bool binding, WABindResult result);
    }

    /// <summary>
    /// 登陆账号回调委托
    /// </summary>
    /// <param name="platform">平台名称</param>
    delegate void OnLoginAccount(string platform);

    /// <summary>
    /// 绑定账户回调委托
    /// </summary>
    /// <param name="accessToken">平台token</param>
    /// <param name="platform">平台名称</param>
    delegate void OnBindingAccount(string accessToken, string platform);

    /// <summary>
    /// 绑定平台账户回调
    /// </summary>
    public abstract class WABindCallback : WACallback<WABindResult>
    {

        /// <summary>
        /// 正在登录第三方平台
        /// </summary>
        /// <param name="platform">平台名称</param>
        public abstract void onLoginAccount(string platform);

        /// <summary>
        /// 绑定平台账户
        /// </summary>
        /// <param name="accessToken">平台accessToken</param>
        /// <param name="platform">平台名称</param>
        public abstract void onBindingAccount(string accessToken, string platform);
    }

    /// <summary>
    /// 权限申请结果回调委托
    /// </summary>
    /// <param name="permissions">申请权限列表</param>
    /// <param name="grantedResults">权限授权结果</param>
    delegate void OnRequestPermissionResult(string[] permissions, bool[] grantedResults);

    /// <summary>
    /// 权限申请回调
    /// </summary>
    public interface WAPermissionCallback
    {
        /// <summary>
        /// 取消申请权限
        /// </summary>
        void onCancel();

        /// <summary>
        /// 权限申请结果
        /// </summary>
        /// <param name="permissions">申请的权限列表</param>
        /// <param name="grantedResults">权限申请结果</param>
        void onRequestPermissionResult(string [] permissions, bool [] grantedResults);
    }
}
