using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PayScript : MonoBehaviour
{

    void Start()
    {

        WAPayProxy.Instance.initialize(new PayInitCallback());

        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 查询商品列表
        GameObject getSkuObj = GameObject.Find("BtnGetSku");
        Button btnGetSku = getSkuObj.GetComponent<Button>();
        btnGetSku.onClick.AddListener(delegate ()
        {
            OnGetSkuClicked();
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
    /// 查询商品列表
    /// </summary>
    void OnGetSkuClicked()
    {
        WASdkDemo.Instance.showLoading("加载中");
        WAPayProxy.Instance.queryInventory(new GetSkuCallback());
    }

    class PayInitCallback : WACallback<WAResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showLongToast("Payment initialize onCancel");
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showLongToast("Payment initialize onError: code\n" + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showLongToast("Payment initialize onSuccess: code\n" + code + "\nmessage: " + message);
        }
    }

    class GetSkuCallback : WACallback<WASkuResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("onCancel");
        }

        public override void onError(int code, string message, WASkuResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("onError \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WASkuResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("onSuccess \ncode: " + code + "\nmessage: " + message);
            WASdkDemo.Instance.onSkuResult(result);
        }
    }

}