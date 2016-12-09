using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FBInviteScript : MonoBehaviour
{

    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 应用邀请
        GameObject appInviteObj = GameObject.Find("BtnAppInvite");
        Button btnAppInvite = appInviteObj.GetComponent<Button>();
        btnAppInvite.onClick.AddListener(delegate ()
        {
            OnAppClicked();
        });

        // 游戏邀请
        GameObject gameInviteObj = GameObject.Find("BtnGameServiceInvite");
        Button btnGameInvite = gameInviteObj.GetComponent<Button>();
        btnGameInvite.onClick.AddListener(delegate ()
        {
            OnGameInviteClicked();
        });

        // 安装奖励
        GameObject installRewardObj = GameObject.Find("BtnInviteInstallReward");
        Button btnInstallRewoard = installRewardObj.GetComponent<Button>();
        btnInstallRewoard.onClick.AddListener(delegate ()
        {
            OnInstallRewardClicked();
        });

        // 事件奖励
        GameObject eventRewardObj = GameObject.Find("BtnInviteEventReward");
        Button btnEventReward = eventRewardObj.GetComponent<Button>();
        btnEventReward.onClick.AddListener(delegate ()
        {
            OnEventRewardClicked();
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
        SceneManager.LoadScene("invite");
    }

    /// <summary>
    /// 应用邀请
    /// </summary>
    void OnAppClicked()
    {
        string appLinkUrl = "https://fb.me/1666843733570117?userId=11111111111";
        string previewImageUrl = "http://img.technews.tw/wp-content/uploads/2014/06/Softbank_Pepper-1_2--624x351.jpg";
        WASocialProxy.Instance.appInvite(WAChannel.CHANNEL_FACEBOOK, appLinkUrl, previewImageUrl, new AppInviteCallbck());
    }

    /// <summary>
    /// 游戏邀请
    /// </summary>
    void OnGameInviteClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.queryInvitableFriends(WAChannel.CHANNEL_FACEBOOK, 0, new QueryInvitableFriendsCallback());

        //WASocialProxy.Instance.queryFriends(WAChannel.CHANNEL_FACEBOOK, new QueryInvitableFriendsCallback());
    }

    /// <summary>
    /// 邀请安装奖励
    /// </summary>
    void OnInstallRewardClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");

        WASocialProxy.Instance.inviteInstallReward(WAChannel.CHANNEL_FACEBOOK, new InviteInstallCallbck());
    }

    /// <summary>
    /// 邀请事件奖励
    /// </summary>
    void OnEventRewardClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.inviteEventReward(WAChannel.CHANNEL_FACEBOOK, "purchase500", new InviteEventCallbck());
    }

    /// <summary>
    /// 应用邀请回调
    /// </summary>
    class AppInviteCallbck : WACallback<WAResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast("App invite on cancel");
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showShortToast("App invite on error!\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showShortToast("App invite on success!\ncode: " + code + "\nmessage: " + message);
        }
    }


    /// <summary>
    /// 查询可邀请好友回调
    /// </summary>
    class QueryInvitableFriendsCallback : WACallback<WAFriendsResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query invitable friends onCancel");
        }

        public override void onError(int code, string message, WAFriendsResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query invitable friends onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAFriendsResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query invitable friends onSuccess: " + message);
            WASdkDemo.Instance.onInvitableFriendsResult(WAChannel.CHANNEL_FACEBOOK, result);
        }
    }


    class InviteInstallCallbck : WACallback<WAResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite install reward on cancel");
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite install reward on error!\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite install reward on success!\ncode: " + code + "\nmessage: " + message);
        }
    }

    class InviteEventCallbck : WACallback<WAResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite event reward on cancel");
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite event reward on error!\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Invite event reward on success!\ncode: " + code + "\nmessage: " + message);
        }
    }
}