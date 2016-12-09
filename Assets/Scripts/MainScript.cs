using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{

    void Start()
    {
        //WASdkAPI.WACoreProxy.Instance.initialize();

        WACoreProxy.Instance.setServerId("165");

        // 绑定设备
        GameObject toggleLogcatObj = GameObject.Find("ToggleLogcat");
        Toggle toggleLogcat = toggleLogcatObj.GetComponent<Toggle>();
        toggleLogcat.onValueChanged.AddListener(delegate (bool value)
        {
            OnLogcatValueChanged(value);
        });
        toggleLogcat.isOn = WASdkDemo.Instance.getBoolFromConfig(WASdkDemo.SP_KEY_ENABLE_LOGCAT, true);

        // 开启应用墙
        GameObject toggleApwObj = GameObject.Find("ToggleApw");
        Toggle toggleApw = toggleApwObj.GetComponent<Toggle>();
        toggleApw.onValueChanged.AddListener(delegate (bool value)
        {
            OnApwValueChanged(value);
        });
        toggleApw.isOn = WASdkDemo.Instance.getBoolFromConfig(WASdkDemo.SP_KEY_ENABLE_APW, true);
        if(toggleApw.isOn)
        {
            WASdkAPI.WAApwProxy.Instance.showEntryFlowIcon();
        }

        // 登出
        GameObject logoutObj = GameObject.Find("BtnLogout");
        Button btnLogout = logoutObj.GetComponent<Button>();
        btnLogout.onClick.AddListener(delegate ()
        {
            OnLogoutClick();
        });

        // 账户管理
        GameObject accountManagerObj = GameObject.Find("BtnAccountManager");
        Button btnAccountManager = accountManagerObj.GetComponent<Button>();
        btnAccountManager.onClick.AddListener(delegate ()
        {
            OnAccountManagerClicked();
        });

        // 支付
        GameObject payObj = GameObject.Find("BtnPay");
        Button btnPay = payObj.GetComponent<Button>();
        btnPay.onClick.AddListener(delegate ()
        {
            OnPayClicked();
        }); 

         // 数据统计
         GameObject trackingObj = GameObject.Find("BtnTracking");
        Button btnTracking = trackingObj.GetComponent<Button>();
        btnTracking.onClick.AddListener(delegate ()
        {
            onTrackingClicked();
        });

        // 社交
        GameObject socialObj = GameObject.Find("BtnSocal");
        Button btnSocial = socialObj.GetComponent<Button>();
        btnSocial.onClick.AddListener(delegate ()
        {
            onSocialClicked();
        });

        // 更新
        GameObject updateObj = GameObject.Find("BtnUpdate");
        Button btnUpdate = updateObj.GetComponent<Button>();
        btnUpdate.onClick.AddListener(delegate ()
        {
            OnUpdateClicked();
        });

        // 闪退测试
        GameObject crashObj = GameObject.Find("BtnCrashTest");
        Button btnCrash = crashObj.GetComponent<Button>();
        btnCrash.onClick.AddListener(delegate ()
        {
            OnCrashClicked();
        });

    }


    void SetCameraColor()
    {

    }

    void OnGUI()
    {
        //调用Jar包的方法
        //if (GUILayout.Button("调用Jar包的方法", GUILayout.Height(60)))
        //{
        //    //获取Android的Java接口
        //    WASdkAPI.WAUserProxy.Instance.login("WINGA", new LoginCallback(), "");
        //}

    }

    /// <summary>
    /// Logcat开关
    /// </summary>
    /// <param name="value"></param>
    void OnLogcatValueChanged(bool value)
    {
        if(value)
        {
            WACommonProxy.Instance.enableLogcat();
        } else
        {
            WACommonProxy.Instance.disableLogcat();
        }
        WASdkDemo.Instance.setConfigBoolValue(WASdkDemo.SP_KEY_ENABLE_LOGCAT, value);
    }

    /// <summary>
    /// Apw开关
    /// </summary>
    /// <param name="value"></param>
    void OnApwValueChanged(bool value)
    {
        if (value)
        {
            WAApwProxy.Instance.showEntryFlowIcon();
        }
        else
        {
            WAApwProxy.Instance.hideEntryFlowIcon();
        }
        WASdkDemo.Instance.setConfigBoolValue(WASdkDemo.SP_KEY_ENABLE_APW, value);
    }

    /// <summary>
    /// 登出
    /// </summary>
    void OnLogoutClick()
    {
        WASdkDemo.Instance.logout();
        WAApwProxy.Instance.hideEntryFlowIcon();
        SceneManager.LoadScene("login");
    }

    /// <summary>
    /// 账户管理
    /// </summary>
    void OnAccountManagerClicked()
    {
        SceneManager.LoadScene("accountManager");
    }

    /// <summary>
    /// 支付
    /// </summary>
    void OnPayClicked()
    {
        SceneManager.LoadScene("pay");
    }

    /// <summary>
    /// 数据收集
    /// </summary>
    void onTrackingClicked()
    {
        SceneManager.LoadScene("tracking");
    }

    /// <summary>
    /// 社交点击
    /// </summary>
    void onSocialClicked()
    {
        SceneManager.LoadScene("socail");
    }


    /// <summary>
    /// 更新点击
    /// </summary>
    void OnUpdateClicked()
    {
        SceneManager.LoadScene("update");
    }

    /// <summary>
    /// 闪退测试
    /// </summary>
    void OnCrashClicked()
    {
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            activity.Call("testCrash");
        }));
    }


    class ShareCallback : WACallback<WAShareResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast("onCancel");
        }

        public override void onError(int code, string message, WAShareResult result)
        {
            WASdkDemo.Instance.showShortToast("onError：" + message);
        }

        public override void onSuccess(int code, string message, WAShareResult result)
        {

            WASdkDemo.Instance.showShortToast("onSuccess: extra=" + (null == result ? "" : result.extra));
        }
    }



    class GetSkuCallback : WACallback<WASkuResult>
    {

        public override void onSuccess(int code, string message, WASkuResult result)
        {
            List<WASkuDetails> skus = result.skuList;

            string msg = message;

            if (null != skus)
            {
                foreach(WASkuDetails sku in skus)
                {
                    msg += "\n" + sku.sku + "---" + sku.title;
                }
            }
            showDialog("Success", msg);
        }

        public override void onCancel()
        {
            
        }

        public override void onError(int code, string message, WASkuResult result)
        {
            
        }
        void showDialog(string title, string message)
        {
            //获取Android的Java接口
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

            activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaClass jcc = new AndroidJavaClass("com.ghw.sdk2.utils.Utils");
                jcc.CallStatic("showMessageDialog", activity, title, message);
            }));
        }

    }

}