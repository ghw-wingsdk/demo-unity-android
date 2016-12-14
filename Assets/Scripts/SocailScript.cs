using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SocailScript : MonoBehaviour
{

    void Start()
    {

        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 分享
        GameObject shareObj = GameObject.Find("BtnShare");
        Button btnShare = shareObj.GetComponent<Button>();
        btnShare.onClick.AddListener(delegate ()
        {
            OnShareClick();
        });

        // 邀请
        GameObject inviteObj = GameObject.Find("BtnInvite");
        Button btnInvite = inviteObj.GetComponent<Button>();
        btnInvite.onClick.AddListener(delegate ()
        {
            OnInviteClicked();
        });

        // 礼物
        GameObject gfitlObj = GameObject.Find("BtnGift");
        Button btnGfit = gfitlObj.GetComponent<Button>();
        btnGfit.onClick.AddListener(delegate ()
        {
            OnGiftClicked();
        });

        // 获取账户信息
        GameObject gameServiceObj = GameObject.Find("BtnGameService");
        Button btnGameService = gameServiceObj.GetComponent<Button>();
        btnGameService.onClick.AddListener(delegate ()
        {
            OnGameServiceClicked();
        });

        // 社区
        GameObject groupObj = GameObject.Find("BtnGroup");
        Button btnGroup = groupObj.GetComponent<Button>();
        btnGroup.onClick.AddListener(delegate ()
        {
            OnGroupClicked();
        });


    }


    void SetCameraColor()
    {

    }

    void OnGUI()
    {

    }


    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("main");
    }

    /// <summary>
    /// 分享
    /// </summary>
    void OnShareClick()
    {
        SceneManager.LoadScene("share");
    }

    /// <summary>
    /// 邀请
    /// </summary>
    void OnInviteClicked()
    {
        SceneManager.LoadScene("invite");
    }

    /// <summary>
    /// 礼物
    /// </summary>
    void OnGiftClicked()
    {
        SceneManager.LoadScene("gift");
    }

    /// <summary>
    /// 游戏服务
    /// </summary>
    void OnGameServiceClicked()
    {
        SceneManager.LoadScene("gameService");
    }

    /// <summary>
    /// 社区
    /// </summary>
    void OnGroupClicked()
    {
        SceneManager.LoadScene("group");
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