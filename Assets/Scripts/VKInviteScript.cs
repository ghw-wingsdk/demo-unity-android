using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VKInviteScript : MonoBehaviour
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
    /// 游戏邀请
    /// </summary>
    void OnGameInviteClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.queryFriends(WAChannel.CHANNEL_VK, new QueryInvitableFriendsCallback());
    }

    /// <summary>
    /// 邀请安装奖励
    /// </summary>
    void OnInstallRewardClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");

        WASocialProxy.Instance.inviteInstallReward(WAChannel.CHANNEL_VK, new InviteInstallCallbck());
    }

    /// <summary>
    /// 邀请事件奖励
    /// </summary>
    void OnEventRewardClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.inviteEventReward(WAChannel.CHANNEL_VK, "purchase500", new InviteEventCallbck());
    }


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
            WASdkDemo.Instance.onInvitableFriendsResult(WAChannel.CHANNEL_VK, result);
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