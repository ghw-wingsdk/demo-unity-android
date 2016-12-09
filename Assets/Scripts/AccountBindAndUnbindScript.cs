using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccountBindAndUnbindScript : MonoBehaviour
{

    private static Button btnFBAccount;
    private static Text txtBtnFBText;
    private static Text txtFBId;
    private static Button btnGGAccount;
    private static Text txtBtnGGText;
    private static Text txtGGId;
    private static Button btnVKAccount;
    private static Text txtBtnVKText;
    private static Text txtVKId;

    private const string BIND = "绑定";
    private const string UNBIND = "解绑";

    void Start()
    {
        
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // Facebook账户
        GameObject txtFBIdObj = GameObject.Find("TxtFBUid");
        txtFBId = txtFBIdObj.GetComponent<Text>();
        GameObject FBAccountObj = GameObject.Find("BtnFBAccount");
        btnFBAccount = FBAccountObj.GetComponent<Button>();
        btnFBAccount.onClick.AddListener(delegate ()
        {
            OnBtnClicked(WAChannel.CHANNEL_FACEBOOK);
        });
        txtBtnFBText = btnFBAccount.GetComponentInChildren<Text>();
        txtBtnFBText.text = BIND;
        btnFBAccount.enabled = false;

        // Google账户
        GameObject txtGGIdObj = GameObject.Find("TxtGGUid");
        txtGGId = txtGGIdObj.GetComponent<Text>();
        GameObject bindGGAccountObj = GameObject.Find("BtnGGAccount");
        btnGGAccount = bindGGAccountObj.GetComponent<Button>();
        btnGGAccount.onClick.AddListener(delegate ()
        {
            OnBtnClicked(WAChannel.CHANNEL_GOOGLE);
        });
        txtBtnGGText = btnGGAccount.GetComponentInChildren<Text>();
        txtBtnGGText.text = BIND;
        btnGGAccount.enabled = false;

        // VK账户
        GameObject txtVKIdObj = GameObject.Find("TxtVKUid");
        txtVKId = txtVKIdObj.GetComponent<Text>();
        GameObject VKAccountObj = GameObject.Find("BtnVKAccount");
        btnVKAccount = VKAccountObj.GetComponent<Button>();
        btnVKAccount.onClick.AddListener(delegate ()
        {
            OnBtnClicked(WAChannel.CHANNEL_VK);
        });
        txtBtnVKText = btnVKAccount.GetComponentInChildren<Text>();
        txtBtnVKText.text = BIND;
        btnVKAccount.enabled = false;

        queryBoundAccount();

    }


    void OnGUI()
    {

    }


    void queryBoundAccount()
    {
        WASdkDemo.Instance.showLoading("正在查询已绑定的第三方平台账户");
        WAUserProxy.Instance.queryBoundAccount(new QueryBoundAccountCallback());
    }
    

    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("accountManager");
    }

    /// <summary>
    /// 按钮点击
    /// </summary>
    /// <param name="platform"></param>
    void OnBtnClicked(string platform)
    {
        string btnText = "";
        string platformUserId = "";
        if (WAChannel.CHANNEL_FACEBOOK.Equals(platform))
        {
            btnText = txtBtnFBText.text;
            platformUserId = txtFBId.text;
        }
        else if (WAChannel.CHANNEL_GOOGLE.Equals(platform))
        {
            btnText = txtBtnGGText.text;
            platformUserId = txtGGId.text;
        }
        else if (WAChannel.CHANNEL_VK.Equals(platform))
        {
            btnText = txtBtnVKText.text;
            platformUserId = txtVKId.text;
        }
        if(BIND.Equals(btnText))
        {
            // 未绑定状态，绑定账户
            WASdkDemo.Instance.showLoading("正在绑定" + platform + "账户");
            WAUserProxy.Instance.bindingAccount(platform, "", new BindAccountCallback(platform));
        }
        else if(UNBIND.Equals(btnText))
        {
            // 绑定状态，解绑账户
            WASdkDemo.Instance.showLoading("正在解绑" + platform + "账户" + platformUserId);
            WAUserProxy.Instance.unBindAccount(platform, platformUserId, new UnbindAccountCallback(platform));
        }
    }

    /// <summary>
    /// 查询已绑定账户列表
    /// </summary>
    class QueryBoundAccountCallback : WACallback<WAAccountResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("操作取消");
        }

        public override void onError(int code, string message, WAAccountResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("查询已绑定账户列表失败\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAAccountResult result)
        {
            btnFBAccount.enabled = true;
            btnGGAccount.enabled = true;
            btnVKAccount.enabled = true;

            WASdkDemo.Instance.dismissLoading();
            if(null == result || null == result.accounts || result.accounts.Count < 1)
            {
                WASdkDemo.Instance.showShortToast("没有绑定任何第三方平台账户");
                return;
            }
            WASdkDemo.Instance.showShortToast("查询完成");

            foreach(WAAccount account in result.accounts)
            {
                if(WAChannel.CHANNEL_FACEBOOK.Equals(account.platform))
                {
                    txtBtnFBText.text = UNBIND;
                    txtFBId.text = account.platformUserId;
                }
                 else if (WAChannel.CHANNEL_GOOGLE.Equals(account.platform))
                {
                    txtBtnGGText.text = UNBIND;
                    txtGGId.text = account.platformUserId;
                }
                else if (WAChannel.CHANNEL_VK.Equals(account.platform))
                {
                    txtBtnVKText.text = UNBIND;
                    txtVKId.text = account.platformUserId;
                }
            }
        }
    }

    /// <summary>
    /// 绑定账户回调
    /// </summary>
    class BindAccountCallback : WABindCallback
    {
        private string platform;

        public BindAccountCallback(string platform)
        {
            this.platform = platform;
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public override void onSuccess(int code, string message, WABindResult result)
        {

            if (WAChannel.CHANNEL_FACEBOOK.Equals(result.platform))
            {
                txtBtnFBText.text = UNBIND;
                txtFBId.text = result.platformUserId;
            }
            else if (WAChannel.CHANNEL_GOOGLE.Equals(result.platform))
            {
                txtBtnGGText.text = UNBIND;
                txtGGId.text = result.platformUserId;
            }
            else if (WAChannel.CHANNEL_VK.Equals(result.platform))
            {
                txtBtnVKText.text = UNBIND;
                txtVKId.text = result.platformUserId;
            }

            WASdkDemo.Instance.dismissLoading();

            WASdkDemo.Instance.showShortToast("绑定成功\ncode: " + code
                + "\nmessage: " + message
                + (null == result ? "" : ("\nplatform: " + result.platform
                + "\nplatformUserId" + result.platformUserId
                + "\nplatformToken" + result.platformToken)));
        }

        /// <summary>
        /// 取消
        /// </summary>
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("绑定操作取消");
        }


        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        /// <param name="throwable">异常信息，可能为空</param>
        public override void onError(int code, string message, WABindResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("绑定失败\ncode: " + code + "\nmessage: " + message);

        }

        /// <summary>
        /// 正在登录第三方平台
        /// </summary>
        /// <param name="platform">平台名称</param>
        public override void onLoginAccount(string platform)
        {
            WASdkDemo.Instance.showLoading("正在登陆" + platform + "账户");
        }

        /// <summary>
        /// 绑定平台账户
        /// </summary>
        /// <param name="accessToken">平台accessToken</param>
        /// <param name="platform">平台名称</param>
        public override void onBindingAccount(string accessToken, string platform)
        {
            WASdkDemo.Instance.showLoading("开始绑定" + platform + "账户");
        }
    }

    /// <summary>
    /// 解绑账户回调
    /// </summary>
    class UnbindAccountCallback : WACallback<WAResult>
    {
        string platform;

        public UnbindAccountCallback(string platform)
        {
            this.platform = platform;
        }

        /// <summary>
        /// 成功回调
        /// </summary>
        /// <param name="code">成功标识</param>
        /// <param name="message">成功说明文字</param>
        /// <param name="result">结果数据</param>
        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();

            WASdkDemo.Instance.showShortToast("解绑成功" + platform + "账户\ncode: " + code 
                + "\nmessage: " + message);

            if (WAChannel.CHANNEL_FACEBOOK.Equals(platform))
            {
                txtFBId.text = "未绑定";
                txtBtnFBText.text = BIND;
            }
            else if (WAChannel.CHANNEL_GOOGLE.Equals(platform))
            {
                txtGGId.text = "未绑定";
                txtBtnGGText.text = BIND;
            }
            else if (WAChannel.CHANNEL_VK.Equals(platform))
            {
                txtVKId.text = "未绑定";
                txtBtnVKText.text = BIND;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("解绑操作取消");
        }


        /// <summary>
        /// 错误回调
        /// </summary>
        /// <param name="code">错误标识</param>
        /// <param name="message">错误说明文字</param>
        /// <param name="result">结果数据，可能为空</param>
        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("解绑失败\ncode: " + code + "\nmessage: " + message);

        }
    }
}