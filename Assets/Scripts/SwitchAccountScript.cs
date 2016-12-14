using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SwitchAccountScript : MonoBehaviour
{
    /// <summary>
    /// 由账户管理进入
    /// </summary>
    public const int FROM_ACCOUNT_MANAGER = 1;

    /// <summary>
    /// 由账户信息获取页面进入
    /// </summary>
    public const int FROM_ACCOUNT_INFO = 2;

    public static int FROM = FROM_ACCOUNT_MANAGER;

    void Start()
    {
        
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 切换Facebook账户
        GameObject FBAccountObj = GameObject.Find("BtnFacebookAccount");
        Button btnFBAccount = FBAccountObj.GetComponent<Button>();
        btnFBAccount.onClick.AddListener(delegate ()
        {
            OnLoginAccount(WAChannel.CHANNEL_FACEBOOK);
        });

        // 切换Google账户
        GameObject GGAccountObj = GameObject.Find("BtnGoogleAccount");
        Button btnGGAccount = GGAccountObj.GetComponent<Button>();
        btnGGAccount.onClick.AddListener(delegate ()
        {
            OnLoginAccount(WAChannel.CHANNEL_GOOGLE);
        });

        // 切换VK账户
        GameObject VKAccountObj = GameObject.Find("BtnVKAccount");
        Button btnVKAccount = VKAccountObj.GetComponent<Button>();
        btnVKAccount.onClick.AddListener(delegate ()
        {
            OnLoginAccount(WAChannel.CHANNEL_VK);
        });

    }


    void OnGUI()
    {

    }

    

    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        if(FROM_ACCOUNT_MANAGER == FROM)
        {
            SceneManager.LoadScene("accountManager");
        } else if(FROM_ACCOUNT_INFO == FROM)
        {
            SceneManager.LoadScene("accountInfo");
        }
    }

    /// <summary>
    /// 登陆账户
    /// </summary>
    /// <param name="platform">平台名称</param>
    void OnLoginAccount(string platform)
    {
        WASdkDemo.Instance.showLoading("正在登陆" + platform + "账户");
        WAUserProxy.Instance.login(platform, new LoginCallback(), "");
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
            if (FROM_ACCOUNT_MANAGER == FROM)
            {
                SceneManager.LoadScene("accountManager");
            }
            else if (FROM_ACCOUNT_INFO == FROM)
            {
                SceneManager.LoadScene("accountInfo");
            }
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