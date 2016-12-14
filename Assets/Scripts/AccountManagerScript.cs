using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AccountManagerScript : MonoBehaviour
{

    private const int WINDOW_ID_NEW_ACCOUNT_WARNING = 0;

    private bool showNewAccountDialog = false;
    private Rect newAccountWindowRect = new Rect(200, 200, 400, 200);

    private Canvas canvas;
    private Rect canvasRect;

    void Start()
    {

        GameObject canvasObj = GameObject.Find("Canvas");
        canvas = canvasObj.GetComponent<Canvas>();
        canvasRect = canvas.pixelRect;

        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 账户绑定与解绑
        GameObject bindAndUnbindAccountObj = GameObject.Find("BtnBindAndUnbindAccount");
        Button btnBindAndUnbindAccount = bindAndUnbindAccountObj.GetComponent<Button>();
        btnBindAndUnbindAccount.onClick.AddListener(delegate ()
        {
            OnBindAndUnbindAccountClick();
        });

        // 新建账户
        GameObject createAccountObj = GameObject.Find("BtnCreateAccount");
        Button btnCreateAccount = createAccountObj.GetComponent<Button>();
        btnCreateAccount.onClick.AddListener(delegate ()
        {
            OnCreateAccountClicked();
        });

        // 切换账户
        GameObject switchAccountlObj = GameObject.Find("BtnSwitchAccount");
        Button btnSwitchAccount = switchAccountlObj.GetComponent<Button>();
        btnSwitchAccount.onClick.AddListener(delegate ()
        {
            OnSwitchAccountClicked();
        });

        // 获取账户信息
        GameObject getAccountInfoObj = GameObject.Find("BtnGetAccountInfo");
        Button btnGetAccountInfo = getAccountInfoObj.GetComponent<Button>();
        btnGetAccountInfo.onClick.AddListener(delegate ()
        {
            OnGetAccountInfoClicked();
        });

        // 打开内置账户管理页面
        GameObject openAccountManagerObj = GameObject.Find("BtnOpenAccountManager");
        Button btnOpenAccountManager = openAccountManagerObj.GetComponent<Button>();
        btnOpenAccountManager.onClick.AddListener(delegate ()
        {
            OnOpenAccountManagerClicked();
        });


    }


    void SetCameraColor()
    {

    }

    void OnGUI()
    {

        GUI.skin.button.fontSize = 18;
        GUI.skin.window.fontSize = 18;
        GUI.skin.label.fontSize = 18;

        if (showNewAccountDialog)
        {
            newAccountWindowRect.width = 500;
            newAccountWindowRect.height = 300;
            newAccountWindowRect.x = (canvasRect.width - 500) / 2;
            newAccountWindowRect.y = (canvasRect.height - 300) / 2;
            newAccountWindowRect = GUI.Window(WINDOW_ID_NEW_ACCOUNT_WARNING, newAccountWindowRect, WindowFun, "警告");
            canvas.enabled = false;
        }

    }


    /// <summary>
    /// 窗口回调
    /// </summary>
    /// <param name="windowId"></param>
    void WindowFun(int windowId)
    {
        // 新建账户警告框
        if(WINDOW_ID_NEW_ACCOUNT_WARNING == windowId)
        {
            Rect msgRect = new Rect(30, 30, 440, 180);
            GUI.Label(msgRect, "如果没有绑定第三方平台账户，新建用户会导致数据丢失，且无法找回；如果已经绑定了第三方平台账户，可以使用第三方平台账户登录。");
            if (GUI.Button(new Rect(80, 230, 140, 50), "确定"))
            {
                showNewAccountDialog = false;
                canvas.enabled = true;
                createNewAccount();
            }
            if (GUI.Button(new Rect(280, 230, 140, 50), "取消"))
            {
                showNewAccountDialog = false;
                canvas.enabled = true;
            }
        }
    }



    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("main");
    }

    /// <summary>
    /// 解绑账户
    /// </summary>
    void OnBindAndUnbindAccountClick()
    {
        SceneManager.LoadScene("accountBindAndUnbind");
    }

    /// <summary>
    /// 创建新用户
    /// </summary>
    void OnCreateAccountClicked()
    {
        showNewAccountDialog = true;
    }

    /// <summary>
    /// 切换账户
    /// </summary>
    void OnSwitchAccountClicked()
    {
        SwitchAccountScript.FROM = SwitchAccountScript.FROM_ACCOUNT_MANAGER;
        SceneManager.LoadScene("switchAccount");
    }

    /// <summary>
    /// 获取账户详情
    /// </summary>
    void OnGetAccountInfoClicked()
    {
        SceneManager.LoadScene("accountInfo");
    }

    /// <summary>
    /// 打开内置的账户管理页面
    /// </summary>
    void OnOpenAccountManagerClicked()
    {
        WAUserProxy.Instance.openAccountManager(new AccountManagerCallback());
    }

    /// <summary>
    /// 调用创建新用户接口，创建新用户
    /// </summary>
    void createNewAccount()
    {
        WASdkDemo.Instance.showLoading("新建账户");
        WAUserProxy.Instance.createNewAccount(new NewAccountCallback());
    }


    /// <summary>
    /// 新建账户
    /// </summary>
    class NewAccountCallback : WACallback<WALoginResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("操作取消");
        }

        public override void onError(int code, string message, WALoginResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("新建账号失败\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WALoginResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.setLoginResult(result);
            string text = "Login account changed->"
                                + "\nplatform:" + result.platform
                                + "\nuserId:" + result.waUserId
                                + "\ntoken:" + result.waToken
                                + "\nplatformUserId:" + result.platformUserId
                                + "\nplatformToken:" + result.platformToken;

            WASdkDemo.Instance.showShortToast(text);
            SceneManager.LoadScene("main");
        }
    }


    /// <summary>
    /// 内置账户管理页面的监听回调
    /// </summary>
    class AccountManagerCallback : WAAccountCallback
    {
        void WAAccountCallback.onBoundAccountChanged(bool binding, WABindResult result)
        {
            string text = "-------onBoundAccountChanged------\n"
                + (binding ? "Bind Account\n" : "Unbind Account\n")
                + "code: " + result.code
                + "\nmessage: " + result.message
                + "\nplatform: " + result.platform
                + "\nplatformUserId: " + result.platformUserId
                + "\nplatformToken: " + result.platformToken;

            WASdkDemo.Instance.showShortToast(text);

            if (binding && WAStatusCode.CODE_SUCCESS == result.code)
            {
                if(WAChannel.CHANNEL_FACEBOOK.Equals(result.platform))
                {
                    WASocialProxy.Instance.inviteInstallReward(WAChannel.CHANNEL_FACEBOOK, new InstallRewardCallback(WAChannel.CHANNEL_FACEBOOK));
                }
            }
        }

        void WAAccountCallback.onLoginAccountChanged(WALoginResult currentAccount)
        {
            WASdkDemo.Instance.setLoginResult(currentAccount);
            string text = "Login account changed->"
                                + "\nplatform:" + currentAccount.platform
                                + "\nuserId:" + currentAccount.waUserId
                                + "\ntoken:" + currentAccount.waToken
                                + "\nplatformUserId:" + currentAccount.platformUserId
                                + "\nplatformToken:" + currentAccount.platformToken;

            WASdkDemo.Instance.showShortToast(text);

            // 数据收集
            WACoreProxy.Instance.setServerId("165");
            WACoreProxy.Instance.setGameUserId("gUid01");

            new WAEvent.Builder()
                .setDefaultEventName(WAEventType.LOGIN)
                .addDefaultEventValue(WAEventParameterName.LEVEL, 140)
                .build()
                .track();
        }
    }

    /// <summary>
    /// 安装奖励监听回调
    /// </summary>
    class InstallRewardCallback : WACallback<WAResult>
    {
        string platform;

        public InstallRewardCallback(string platform)
        {
            this.platform = platform;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast(platform + " invite install reward send canceled");
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showShortToast(platform + " invite install reward send error");
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showShortToast(platform + " invite install reward send succeed");
        }
    }
}