using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FBGiftScript : MonoBehaviour
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

        // 赠送礼物
        GameObject sendGiftObj = GameObject.Find("BtnSendGift");
        Button btnSendGift = sendGiftObj.GetComponent<Button>();
        btnSendGift.onClick.AddListener(delegate ()
        {
            OnSendGiftClicked();
        });

        // 索要礼物
        GameObject askforGiftObj = GameObject.Find("BtnAskforGift");
        Button btnAskforGift = askforGiftObj.GetComponent<Button>();
        btnAskforGift.onClick.AddListener(delegate ()
        {
            OnAskforGiftClicked();
        });

        // 查看收到的礼物
        GameObject receivedGiftObj = GameObject.Find("BtnReceived");
        Button btnReceivedGift = receivedGiftObj.GetComponent<Button>();
        btnReceivedGift.onClick.AddListener(delegate ()
        {
            OnReceivedGiftClicked();
        });

        // 查看收到的索要礼物的请求
        GameObject askforGiftRequestObj = GameObject.Find("BtnAskforRequest");
        Button btnAskforGiftRequest = askforGiftRequestObj.GetComponent<Button>();
        btnAskforGiftRequest.onClick.AddListener(delegate ()
        {
            OnAskforGiftRequestClicked();
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
        SceneManager.LoadScene("gift");
    }

    /// <summary>
    /// 赠送礼物
    /// </summary>
    void OnSendGiftClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.queryFriends(WAChannel.CHANNEL_FACEBOOK, new QueryFriendsCallback(WARequestType.REQUEST_GIFT_SEND));
    }

    /// <summary>
    /// 索要礼物
    /// </summary>
    void OnAskforGiftClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.queryFriends(WAChannel.CHANNEL_FACEBOOK, new QueryFriendsCallback(WARequestType.REQUEST_GIFT_ASK));
    }

    /// <summary>
    /// 查看收到的礼物
    /// </summary>
    void OnReceivedGiftClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.fbQueryReceivedGifts(new QueryGiftRequestCallback(WARequestType.REQUEST_GIFT_SEND));
    }

    /// <summary>
    /// 查看收到的索要礼物的请求
    /// </summary>
    void OnAskforGiftRequestClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WASocialProxy.Instance.fbQueryReceivedGifts(new QueryGiftRequestCallback(WARequestType.REQUEST_GIFT_ASK));
    }


    /// <summary>
    /// 查询可邀请好友回调
    /// </summary>
    class QueryFriendsCallback : WACallback<WAFriendsResult>
    {
        string type = WARequestType.REQUEST_GIFT_SEND;

        public QueryFriendsCallback(string type)
        {
            this.type = type;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query friends onCancel");
        }

        public override void onError(int code, string message, WAFriendsResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query friends onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAFriendsResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query friends onSuccess: " + message);
            WASdkDemo.Instance.onQueryGiftFriendsResult(WAChannel.CHANNEL_FACEBOOK, type, result);
        }
    }

    /// <summary>
    /// 查询礼物请求列表（收到的礼物、索要礼物请求）
    /// </summary>
    class QueryGiftRequestCallback : WACallback<WAFBGameRequestResult>
    {
        string type = WARequestType.REQUEST_GIFT_SEND;

        public QueryGiftRequestCallback(string type)
        {
            this.type = type;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query received gift onCancel");
        }

        public override void onError(int code, string message, WAFBGameRequestResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query received gift onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAFBGameRequestResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Query received gift onSuccess: " + message);
            WASdkDemo.Instance.onQueryGiftRequestResult(WAChannel.CHANNEL_FACEBOOK, type, result);
        }
    }

}