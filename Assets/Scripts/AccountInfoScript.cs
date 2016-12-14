using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccountInfoScript : MonoBehaviour
{
    private static Text txtNickname;
    private static Text txtId;
    private static Text txtPlatform;

    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 切换账户
        GameObject switchAccountObj = GameObject.Find("BtnSwitchAccount");
        Button btnSwitchAccount = switchAccountObj.GetComponent<Button>();
        btnSwitchAccount.onClick.AddListener(delegate ()
        {
            switchAccount();
        });

        // Facebook账户
        GameObject FBAccountObj = GameObject.Find("BtnFacebookAccount");
        Button btnFBAccount = FBAccountObj.GetComponent<Button>();
        btnFBAccount.onClick.AddListener(delegate ()
        {
            OnGetAccountInfo(WAChannel.CHANNEL_FACEBOOK);
        });

        // Google账户
        GameObject GGAccountObj = GameObject.Find("BtnGoogleAccount");
        Button btnGGAccount = GGAccountObj.GetComponent<Button>();
        btnGGAccount.onClick.AddListener(delegate ()
        {
            OnGetAccountInfo(WAChannel.CHANNEL_GOOGLE);
        });

        // VK账户
        GameObject VKAccountObj = GameObject.Find("BtnVKAccount");
        Button btnVKAccount = VKAccountObj.GetComponent<Button>();
        btnVKAccount.onClick.AddListener(delegate ()
        {
            OnGetAccountInfo(WAChannel.CHANNEL_VK);
        });

        GameObject txtNicknameObj = GameObject.Find("txtNickname");
        txtNickname = txtNicknameObj.GetComponent<Text>();

        GameObject txtIdObj = GameObject.Find("txtId");
        txtId = txtIdObj.GetComponent<Text>();

        GameObject txtPlatformObj = GameObject.Find("txtPlatform");
        txtPlatform = txtPlatformObj.GetComponent<Text>();

    }


    void OnGUI()
    {

    }

    

    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("accountManager");
    }

    /// <summary>
    /// 切换账户
    /// </summary>
    void switchAccount()
    {
        SwitchAccountScript.FROM = SwitchAccountScript.FROM_ACCOUNT_INFO;
        SceneManager.LoadScene("switchAccount");
    }

    /// <summary>
    /// 获取账户信息
    /// </summary>
    /// <param name="platform">平台名称</param>
    void OnGetAccountInfo(string platform)
    {
        WASdkDemo.Instance.showLoading("正在获取" + platform + "账户信息");
        WAUserProxy.Instance.getAccountInfo(platform, new GetAccountInfoCallback());
    }


    class GetAccountInfoCallback : WACallback<WAUser>
    {

        public override void onSuccess(int code, string message, WAUser result)
        {
            WASdkDemo.Instance.dismissLoading();
            string msg = "Get account info success!";
            msg += "\ncode: " + code;
            msg += "\nmessage: " + message;
            msg += "\nid: " + result.id;
            msg += "\nnickname: " + result.name;
            msg += "\nplatform: " + result.platform;
            msg += "\nwaUserId: " + result.waUserId;
            WASdkDemo.Instance.showShortToast(msg);

            if(null != txtNickname)
            {
                txtNickname.text = "昵称：" + result.name;
            }

            if(null != txtId)
            {
                txtId.text = "ID：" + result.id;
            }

            if(null != txtPlatform)
            {
                txtPlatform.text = "平台：" + result.platform;
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
        public override void onError(int code, string message, WAUser result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Login failed!\n" + message);
            if (null != txtNickname)
            {
                txtNickname.text = "code：" + code;
            }

            if (null != txtId)
            {
                txtId.text = "message：" + message;
            }
        }

    }
}