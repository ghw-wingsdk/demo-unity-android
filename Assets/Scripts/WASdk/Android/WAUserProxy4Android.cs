using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WASdkAPI
{
    

    internal class WAUserProxy4Android : WAUserProxy
    {
        private AndroidJavaClass androidJavaClass = null;
        private AndroidJavaClass userProxy = null;

        public WAUserProxy4Android()
        {
            androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            userProxy = new AndroidJavaClass("com.wa.sdk.user.WAUserProxy");
        }

        /// <summary>
        /// 设置登录流程类型
        /// </summary>
        /// <param name="flowType">登录流程类型</param>
        public override void setLoginFlowType(int flowType)
        {
            if (null != androidJavaClass && null != userProxy)
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    userProxy.CallStatic("setLoginFlowType", flowType);
                }));
            }
        }

        /// <summary>
        /// 登录账户
        /// </summary>
        /// <param name="platform">登录平台名称</param>
        /// <param name="callback">回调</param>
        /// <param name="extInfo">额外数据</param>
        public override void login(string platform, WACallback<WALoginResult> callback, string extInfo)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WALoginResult> onError = new OnError<WALoginResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("login", activity, platform, new InternalLoginCallback(callback), extInfo);
            }));
        }

        /// <summary>
        /// 带登录界面的登录
        /// </summary>
        /// <param name="enableCache">是否启用缓存</param>
        /// <param name="callback">登录回调</param>
        public override void loginUI(bool enableCache, WACallback<WALoginResult> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WALoginResult> onError = new OnError<WALoginResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("loginUI", activity, enableCache, new InternalLoginCallback(callback));
            }));
        }

        /// <summary>
        /// 清除登录缓存
        /// </summary>
        public override void clearLoginCache()
        {
            if (null != androidJavaClass && null != userProxy)
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    userProxy.CallStatic("clearLoginCache");
                }));
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        public override void logout()
        {
            if (null != androidJavaClass && null != userProxy)
            {
                AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    userProxy.CallStatic("logout");
                }));
            }
        }

        /// <summary>
        /// 绑定平台账号
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="extInfo">额外数据</param>
        /// <param name="callback">回调，结果返回</param>
        public override void bindingAccount(string platform, string extInfo, WABindCallback callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WABindResult> onError = new OnError<WABindResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("bindingAccount", activity, platform, extInfo, new InternalBindCallback(callback));
            }));
        }

        /// <summary>
        /// 查询已绑定的平台账号列表
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void queryBoundAccount(WACallback<WAAccountResult> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WAAccountResult> onError = new OnError<WAAccountResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("queryBoundAccount", new InternalQueryBoundAccountCallback(callback));
            }));
        }

        /// <summary>
        /// 解绑第三方平台账户
        /// </summary>
        /// <param name="platform">需要解绑的第三方账户的平台类型</param>
        /// <param name="platformUserId">第三方平台用户id</param>
        /// <param name="callback">回调，结果返回</param>
        public override void unBindAccount(string platform, string platformUserId, WACallback<WAResult> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WAResult> onError = new OnError<WAResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("unBindAccount", platform, platformUserId, new InternalUnbindCallback(callback));
            }));
        }

        /// <summary>
        /// 切换账号
        /// </summary>
        /// <param name="platform">需要切换的账号平台类型</param>
        /// <param name="WACallback">回调，结果返回</param>
        public override void switchAccount(string platform, WACallback<WALoginResult> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WALoginResult> onError = new OnError<WALoginResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("switchAccount", activity, platform, new InternalLoginCallback(callback));
            }));
        }

        /// <summary>
        /// 新建WA账户
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void createNewAccount(WACallback<WALoginResult> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WALoginResult> onError = new OnError<WALoginResult>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("createNewAccount", new InternalLoginCallback(callback));
            }));
        }

        /// <summary>
        /// 打开内置的账户管理页面
        /// </summary>
        /// <param name="callback">回调，结果返回</param>
        public override void openAccountManager(WAAccountCallback callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("openAccountManager", activity, new InternalAccountManbagerCallback(callback));
            }));
        }

        /// <summary>
        /// 获取当前用户的用户信息
        /// </summary>
        /// <param name="platform">平台名称</param>
        /// <param name="callback">回调，结果返回</param>
        public override void getAccountInfo(string platform, WACallback<WAUser> callback)
        {
            if (null == androidJavaClass && null == userProxy)
            {
                Debug.LogError("WASdk internal error： Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!");
                if (null != callback)
                {
                    OnError<WAUser> onError = new OnError<WAUser>(callback.onError);
                    onError(WAStatusCode.CODE_ERROR, "Get UnityPlayer class or com.wa.sdk.user.WAUserProxy failed!", null);
                }
                return;
            }
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                userProxy.CallStatic("getAccountInfo", activity, platform, new InternalGetAccountInfoCallback(callback));
            }));
        }
    }

    /// <summary>
    /// 登录内部回调，处理结果中转
    /// </summary>
    internal class InternalLoginCallback : AndroidCallback<WALoginResult>
    {
        public InternalLoginCallback(WACallback<WALoginResult> callback) : base(callback)
        {
        }

        protected override WALoginResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WALoginResult loginResult = new WALoginResult();
            loginResult.code = result.Get<int>("code");
            loginResult.message = result.Get<string>("message");
            loginResult.platform = result.Get<string>("platform");
            loginResult.waUserId = result.Get<string>("userId");
            loginResult.waToken = result.Get<string>("token");
            loginResult.platformUserId = result.Get<string>("platformUserId");
            loginResult.platformToken = result.Get<string>("platformToken");
            return loginResult;
        }
    }

    /// <summary>
    /// 绑定账号内部回调，内部调用，结果中转
    /// </summary>
    internal class InternalBindCallback : AndroidJavaProxy
    {
        WABindCallback callback;
        public InternalBindCallback(WABindCallback callback) : base("com.wa.sdk.user.model.WABindCallback")
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
                OnSuccess<WABindResult> onSuccess = new OnSuccess<WABindResult>(callback.onSuccess);
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
                OnSuccess<WABindResult> onSuccess = new OnSuccess<WABindResult>(callback.onSuccess);
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
                OnError<WABindResult> onError = new OnError<WABindResult>(callback.onError);
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
                OnError<WABindResult> onError = new OnError<WABindResult>(callback.onError);
                onError(code, null == message ? "" : message.ToString(), parseResult(result));
            }
        }

        /// <summary>
        /// 正在登录第三方平台
        /// </summary>
        /// <param name="platform">平台名称</param>
        public void onLoginAccount(string platform)
        {
            if(null != callback)
            {
                OnLoginAccount onLoginAccount = new OnLoginAccount(((WABindCallback)callback).onLoginAccount);
                onLoginAccount(platform);
            }
        }

        /// <summary>
        /// 绑定平台账户
        /// </summary>
        /// <param name="accessToken">平台accessToken</param>
        /// <param name="platform">平台名称</param>
        public void onBindingAccount(string accessToken, string platform)
        {
            if (null != callback)
            {
                OnBindingAccount onBindAccount = new OnBindingAccount(((WABindCallback)callback).onBindingAccount);
                onBindAccount(accessToken, platform);
            }
        }

        protected WABindResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WABindResult bindResult = new WABindResult();
            bindResult.code = result.Get<int>("code");
            bindResult.message = result.Get<string>("message");
            bindResult.platform = result.Get<string>("platform");
            bindResult.platformUserId = result.Get<string>("platformUserId");
            bindResult.platformToken = result.Get<string>("accessToken");
            return bindResult;
        }
    }

    /// <summary>
    /// 查询绑定账户列表内部回调，结果的中转处理
    /// </summary>
    internal class InternalQueryBoundAccountCallback : AndroidCallback<WAAccountResult>
    {
        public InternalQueryBoundAccountCallback(WACallback<WAAccountResult> callback) : base(callback)
        {
        }

        protected override WAAccountResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }

            WAAccountResult accountResult = new WAAccountResult();
            accountResult.code = result.Get<int>("code");
            accountResult.message = result.Get<string>("message");
            AndroidJavaObject accountListObject = result.Get<AndroidJavaObject>("accounts");
            if (null != accountListObject)
            {
                int size = accountListObject.Call<int>("size");
                AndroidJavaObject accountObject;
                WAAccount account;
                for (int i = 0; i < size; i++)
                {
                    accountObject = accountListObject.Call<AndroidJavaObject>("get", i);
                    if (null != accountObject)
                    {
                        account = new WAAccount();
                        account.platform = accountObject.Get<string>("platform");
                        account.platformUserId = accountObject.Get<string>("platformUserId");
                        accountResult.AddAccount(account);
                    }
                }
            }
            return accountResult;
        }
    }

    /// <summary>
    /// 解绑账户内部回调，用来中转结果
    /// </summary>
    internal class InternalUnbindCallback : AndroidCallback<WAResult>
    {
        public InternalUnbindCallback(WACallback<WAResult> callback) : base(callback)
        {
        }

        protected override WAResult parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAResult waResult = new WAResult();
            waResult.code = result.Get<int>("code");
            waResult.message = result.Get<string>("message");
            return waResult;
        }
    }

    /// <summary>
    /// 账户管理内部回调，用来中转结果
    /// </summary>
    internal class InternalAccountManbagerCallback : AndroidJavaProxy
    {
        WAAccountCallback waAccountCallback;

        public InternalAccountManbagerCallback(WAAccountCallback callback) : base("com.wa.sdk.user.model.WAAccountCallback")
        {
            this.waAccountCallback = callback;
        }

        /// <summary>
        /// 当前登录的账户发生改变
        /// </summary>
        /// <param name="currentAccount">当前登录账户</param>
        protected void onLoginAccountChanged(AndroidJavaObject currentAccount)
        {
            if(null != waAccountCallback)
            {
                WALoginResult loginResult = null;
                if (null != currentAccount)
                {
                    loginResult = new WALoginResult();
                    loginResult.code = currentAccount.Get<int>("code");
                    loginResult.message = currentAccount.Get<string>("message");
                    loginResult.platform = currentAccount.Get<string>("platform");
                    loginResult.waUserId = currentAccount.Get<string>("userId");
                    loginResult.waToken = currentAccount.Get<string>("token");
                    loginResult.platformUserId = currentAccount.Get<string>("platformUserId");
                    loginResult.platformToken = currentAccount.Get<string>("platformToken");
                }
                OnLoginAccountChanged onLoginAccountChanged = new OnLoginAccountChanged(waAccountCallback.onLoginAccountChanged);
                onLoginAccountChanged(loginResult);
            }
        }

        /// <summary>
        /// 账号绑定状态发生改变
        /// </summary>
        /// <param name="binding">是否为绑定账户</param>
        /// <param name="result">绑定结果</param>
        protected void onBoundAccountChanged(bool binding, AndroidJavaObject result)
        {
            if(null != waAccountCallback)
            {
                WABindResult bindResult = null;
                if (null != result)
                {
                    bindResult = new WABindResult();
                    bindResult.code = result.Get<int>("code");
                    bindResult.message = result.Get<string>("message");
                    bindResult.platform = result.Get<string>("platform");
                    bindResult.platformUserId = result.Get<string>("platformUserId");
                    bindResult.platformToken = result.Get<string>("platformToken");
                }
                OnBoundAccountChanged onBoundAccountChanged = new OnBoundAccountChanged(waAccountCallback.onBoundAccountChanged);
                onBoundAccountChanged(binding, bindResult);
            }
        }
    }

    /// <summary>
    /// 获取账户信息内部回调，用来中转结果
    /// </summary>
    internal class InternalGetAccountInfoCallback : AndroidCallback<WAUser>
    {
        public InternalGetAccountInfoCallback(WACallback<WAUser> callback) : base(callback)
        {

        }

        protected override WAUser parseResult(AndroidJavaObject result)
        {
            if (null == result)
            {
                return null;
            }
            WAUser waUser = new WAUser();
            waUser.id = result.Get<string>("id");
            waUser.name = result.Get<string>("name");
            waUser.picture = result.Get<string>("picture");
            waUser.platform = result.Get<string>("platform");
            waUser.waUserId = result.Get<string>("ghwUserId");
            waUser.phone = result.Get<string>("phone");
            waUser.isFriend = result.Get<bool>("isFriend");
            waUser.isInvited = result.Get<bool>("isInvited");
            waUser.lastInvitedTime = result.Get<long>("lastInvitedTime");
            waUser.isPlayedThisGame = result.Get<bool>("isPlayedThisGame");
            waUser.group = result.Get<string>("group");
            return waUser;
        }
    }
}
