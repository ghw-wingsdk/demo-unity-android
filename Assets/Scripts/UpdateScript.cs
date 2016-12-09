using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UpdateScript : MonoBehaviour
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

        // 检查热更新
        GameObject checkUpdateObj = GameObject.Find("BtnCheckUpdate");
        Button btnCheckUpdate = checkUpdateObj.GetComponent<Button>();
        btnCheckUpdate.onClick.AddListener(delegate ()
        {
            OnCheckUpdateClicked();
        });

        // 获取更新链接
        GameObject getUpdateLinkObj = GameObject.Find("BtnGetUpdateLink");
        Button btnGetUpdateLink = getUpdateLinkObj.GetComponent<Button>();
        btnGetUpdateLink.onClick.AddListener(delegate ()
        {
            OnGetUpdateLinkClicked();
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
        SceneManager.LoadScene("main");
    }

    /// <summary>
    /// 检查热更新
    /// </summary>
    void OnCheckUpdateClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WAHupProxy.Instance.checkUpdate(new CheckHupUpdateCallback());
    }

    /// <summary>
    /// 获取下载链接
    /// </summary>
    void OnGetUpdateLinkClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WACommonProxy.Instance.getAppUpdateLink(new GetAppUpdateLinkCallback());
    }


    class CheckHupUpdateCallback : WACallback<WACheckUpdateResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Check hot update onCancel");
        }

        public override void onError(int code, string message, WACheckUpdateResult result)
        {

            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Check hot update onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WACheckUpdateResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.onCheckHupUpdateResult(result);
        }
    }

    class GetAppUpdateLinkCallback : WACallback<string>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Get app update link onCancel");
        }

        public override void onError(int code, string message, string result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Get app update link onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, string result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.onGetAppUpdateLinkResult(result);
        }
    }

}