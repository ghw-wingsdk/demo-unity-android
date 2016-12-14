using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WASdkAPI;
using System;
using System.Collections.Generic;

public class LoginScript : MonoBehaviour {

    bool userLoginCache = false;

	// Use this for initialization
	void Start () {

        if(WASdkDemo.Instance.getBoolFromConfig(WASdkDemo.SP_KEY_ENABLE_LOGCAT, true))
        {
            WACommonProxy.Instance.enableLogcat();
        } else
        {
            WACommonProxy.Instance.disableLogcat();
        }

        // 绑定设备
        GameObject toggleBindDeviceObj = GameObject.Find("ToggleBindDevice");
        Toggle toggleBindDevice = toggleBindDeviceObj.GetComponent<Toggle>();
        toggleBindDevice.onValueChanged.AddListener(delegate(bool value)
        {
            OnBindDeviceValueChanged(value);
        });
        toggleBindDevice.isOn = WASdkDemo.Instance.getBoolFromConfig(WASdkDemo.SP_KEY_REBIND_DEVICE_ON_LOGIN, false);

        // 匿名登录
        GameObject guestLoginObj = GameObject.Find("BtnGustLogin");
        Button btnGuestLogin = guestLoginObj.GetComponent<Button>();
        btnGuestLogin.onClick.AddListener(delegate ()
        {
            OnGuestLoginClicked();
        });

        // 应用内登录
        GameObject appLoginObj = GameObject.Find("BtnAppLogin");
        Button btnAppLogin = appLoginObj.GetComponent<Button>();
        btnAppLogin.onClick.AddListener(delegate ()
        {
            OnAppLoginClicked();
        });

        // Facebook登录
        GameObject fbLoginObj = GameObject.Find("BtnFBLogin");
        Button btnFBLogin = fbLoginObj.GetComponent<Button>();
        btnFBLogin.onClick.AddListener(delegate ()
        {
            OnFBLoginClicked();
        });

        // Google登录
        GameObject ggLoginObj = GameObject.Find("BtnGGLogin");
        Button btnGGLogin = ggLoginObj.GetComponent<Button>();
        btnGGLogin.onClick.AddListener(delegate ()
        {
            OnGGLoginClicked();
        });

        // VK登录
        GameObject vkLoginObj = GameObject.Find("BtnVKLogin");
        Button btnVKLogin = vkLoginObj.GetComponent<Button>();
        btnVKLogin.onClick.AddListener(delegate ()
        {
            OnVKLoginClicked();
        });

        // 启用登录缓存
        GameObject toggleUseLoginCacheObj = GameObject.Find("ToggleUseLoginCache");
        Toggle toggleUseLoginCache = toggleUseLoginCacheObj.GetComponent<Toggle>();
        userLoginCache = toggleUseLoginCache.isOn;
        toggleUseLoginCache.onValueChanged.AddListener(delegate (bool value)
        {
            OnUseLoginCacheValueChanged(value);
        });
        toggleUseLoginCache.isOn = WASdkDemo.Instance.getBoolFromConfig(WASdkDemo.SP_KEY_ENABLE_LOGIN_CACHE, false);
        

        // 登录对话框
        GameObject dialogLoginObj = GameObject.Find("BtnLoginForm");
        Button btnDialogLogin = dialogLoginObj.GetComponent<Button>();
        btnDialogLogin.onClick.AddListener(delegate ()
        {
            OnDialogLoginClicked();
        });

        // 清除登录缓存
        GameObject clearLoginCacheObj = GameObject.Find("BtnClearLoginCache");
        Button btnClearLoginCache = clearLoginCacheObj.GetComponent<Button>();
        btnClearLoginCache.onClick.AddListener(delegate ()
        {
            OnClearLoginCacheClicked();
        });

    }


    // Update is called once per frame
    void Update () {
	
	}

    // 绑定设备Toggle值改变
    void OnBindDeviceValueChanged(bool value)
    {
        if(value)
        {
            WASdkDemo.Instance.showShortToast("登录账号将重新跟当前设备绑定");
            WAUserProxy.Instance.setLoginFlowType(WALoginFLowType.LOGIN_FLOW_TYPE_REBIND);
        }
        else
        {
            WASdkDemo.Instance.showShortToast("登录账号不会跟当前设备绑定");
            WAUserProxy.Instance.setLoginFlowType(WALoginFLowType.LOGIN_FLOW_TYPE_DEFAULT);
        }
        WASdkDemo.Instance.setConfigBoolValue(WASdkDemo.SP_KEY_REBIND_DEVICE_ON_LOGIN, value);

    }

    /// <summary>
    /// 匿名登录
    /// </summary>
    void OnGuestLoginClicked()
    {
        WASdkDemo.Instance.showLoading("匿名登录");
        WAUserProxy.Instance.login(WAChannel.CHANNEL_WA, new LoginCallback(), "");
    }

    /// <summary>
    /// 应用内登录
    /// </summary>
    void OnAppLoginClicked()
    {
        WASdkDemo.Instance.showLoading("应用内登录");
        string appLoginExtInfo = "{\"appSelfLogin\":true, \"appUserId\":\"12345\", \"appToken\":\"o1akkfjia81FMvFSO8kxC96TgQYlhEEr\", \"extInfo\":\"extInfo String\"}";
        WAUserProxy.Instance.login(WAChannel.CHANNEL_WA, new LoginCallback(), appLoginExtInfo);
    }

    /// <summary>
    /// Facebook登录
    /// </summary>
    void OnFBLoginClicked()
    {
        WASdkDemo.Instance.showLoading("Facebook登录");
        WAUserProxy.Instance.login(WAChannel.CHANNEL_FACEBOOK, new LoginCallback(), "");
    }

    /// <summary>
    /// Google登录
    /// </summary>
    void OnGGLoginClicked()
    {
        WASdkDemo.Instance.showLoading("Google登录");
        WAUserProxy.Instance.login(WAChannel.CHANNEL_GOOGLE, new LoginCallback(), "");
    }

    /// <summary>
    /// VK登录
    /// </summary>
    void OnVKLoginClicked()
    {
        WASdkDemo.Instance.showLoading("VK登录");
        WAUserProxy.Instance.login(WAChannel.CHANNEL_VK, new LoginCallback(), "");
    }

    /// <summary>
    /// 启用/关闭登录缓存
    /// </summary>
    /// <param name="value"></param>
    void OnUseLoginCacheValueChanged(bool value)
    {
        userLoginCache = value;
        WASdkDemo.Instance.setConfigBoolValue(WASdkDemo.SP_KEY_ENABLE_LOGIN_CACHE, value);
    }

    /// <summary>
    /// 对话框登录
    /// </summary>
    void OnDialogLoginClicked()
    {
        WAUserProxy.Instance.loginUI(userLoginCache, new LoginCallback());
    }

    /// <summary>
    /// 清除登录缓存
    /// </summary>
    void OnClearLoginCacheClicked()
    {
        WASdkDemo.Instance.showShortToast("清除登录缓存");
        WAUserProxy.Instance.clearLoginCache();
    }

    class LoginCallback : WACallback<WALoginResult>
    {

        public override void onSuccess(int code, string message, WALoginResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            string msg = "Login success!";
            msg += "\ncode: " + code;
            msg += "\nmessage: " + message;
            msg += "\nplatform: " + result.platform;
            msg += "\nwaUserId: " + result.waUserId;
            msg += "\nwaToken: " + result.waToken;
            msg += "\nplatformUserId: " + result.platformUserId;
            msg += "\nplatformToken: " + result.platformToken;
            WASdkDemo.Instance.showShortToast(msg);
            WASdkDemo.Instance.setLoginResult(result);
            SceneManager.LoadScene("main");
        }

        /**
         * 取消回调
         */
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Login Canceled");
        }

        /**
         * 错误回调
         * @param code 结果码
         * @param message 结果描述消息
         * @param result 结果数据，如果没有结果返回null
         * @param throwable 异常信息，没有返回null
         */
        public override void onError(int code, string message, WALoginResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Login failed!\n" + message);
        }

    }
}
