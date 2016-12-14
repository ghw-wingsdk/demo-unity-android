using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TrackingScript : MonoBehaviour
{

    static InputField InputSelf;

    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // ghw_login
        GameObject loginObj = GameObject.Find("BtnLogin");
        Button btnLogin = loginObj.GetComponent<Button>();
        btnLogin.onClick.AddListener(delegate ()
        {
            onGhwLogin();
        });

        // ghw_initiated_payment
        GameObject initPaymentObj = GameObject.Find("BtnInitPayment");
        Button btnInitPayment = initPaymentObj.GetComponent<Button>();
        btnInitPayment.onClick.AddListener(delegate ()
        {
            onGhwInitiatedPayment();
        });

        // ghw_payment
        GameObject paymentObj = GameObject.Find("BtnPayment");
        Button btnPayment = paymentObj.GetComponent<Button>();
        btnPayment.onClick.AddListener(delegate ()
        {
            onGhwPayment();
        });

        // ghw_initiated_purchase
        GameObject initPurchaseObj = GameObject.Find("BtnInitPurchase");
        Button btnInitPurchase = initPurchaseObj.GetComponent<Button>();
        btnInitPurchase.onClick.AddListener(delegate ()
        {
            onGhwInitiatedPurchase();
        });

        // ghw_purchase
        GameObject purchaseObj = GameObject.Find("BtnPurchase");
        Button btnPurchase = purchaseObj.GetComponent<Button>();
        btnPurchase.onClick.AddListener(delegate ()
        {
            onGhwPurchase();
        });

        // ghw_level_achieved
        GameObject levelAchievedObj = GameObject.Find("BtnLevelAchieved");
        Button btnLevelAchieved = levelAchievedObj.GetComponent<Button>();
        btnLevelAchieved.onClick.AddListener(delegate ()
        {
            onGhwLevelAchieved();
        });
        
        // ghw_user_create
        GameObject userCreateObj = GameObject.Find("BtnUserCreate");
        Button btnUserCreate = userCreateObj.GetComponent<Button>();
        btnUserCreate.onClick.AddListener(delegate ()
        {
            onGhwUserCreate();
        });

        // ghw_user_info_update
        GameObject userInfoUpdateObj = GameObject.Find("BtnUserInfoUpdate");
        Button btnUserInfoUpdate = userInfoUpdateObj.GetComponent<Button>();
        btnUserInfoUpdate.onClick.AddListener(delegate ()
        {
            onUserInfoUPdate();
        });

        // ghw_task_update
        GameObject taskUPdateObj = GameObject.Find("BtnTaskUpdate");
        Button btnTaskUpdate = taskUPdateObj.GetComponent<Button>();
        btnTaskUpdate.onClick.AddListener(delegate ()
        {
            onGhwTaskUpdate();
        });

        // ghw_gold_update
        GameObject goldUpdateObj = GameObject.Find("BtnGoldUpdate");
        Button btnGoldUpdate = goldUpdateObj.GetComponent<Button>();
        btnGoldUpdate.onClick.AddListener(delegate ()
        {
            onGhwGoldUpdate();
        });

        // ghw_user_import
        GameObject userImportObj = GameObject.Find("BtnUserImport");
        Button btnUserImport = userImportObj.GetComponent<Button>();
        btnUserImport.onClick.AddListener(delegate ()
        {
            onGhwUserImport();
        });

        // ghw_self_(自定义)
        GameObject selfObj = GameObject.Find("BtnSelf");
        Button btnSelf = selfObj.GetComponent<Button>();
        btnSelf.onClick.AddListener(delegate ()
        {
            onGhwSelf();
        });


        GameObject inputSelfObj = GameObject.Find("InputSelf");
        InputSelf = inputSelfObj.GetComponent<InputField>();


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
    /// ghw_login
    /// </summary>
    void onGhwLogin()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.LOGIN)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_initiated_payment
    /// </summary>
    void onGhwInitiatedPayment()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.INITIATED_PAYMENT)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_payment
    /// </summary>
    void onGhwPayment()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.COMPLETE_PAYMENT)
            .setDefaultValue(200)
            .addDefaultEventValue(WAEventParameterName.TRANSACTION_ID, "1234567890_0987654321")
            .addDefaultEventValue(WAEventParameterName.PAYMENT_TYPE, "GOOGLE")
            .addDefaultEventValue(WAEventParameterName.CURRENCY_TYPE, "RMB")
            .addDefaultEventValue(WAEventParameterName.CURRENCY_AMOUNT, 200)
            .addDefaultEventValue(WAEventParameterName.VERTUAL_COIN_CURRENCY, "COIN")
            .addDefaultEventValue(WAEventParameterName.VERTUAL_COIN_AMOUNT, 2000)
            .addDefaultEventValue(WAEventParameterName.IAP_ID, "1234")
            .addDefaultEventValue(WAEventParameterName.IAP_NAME, "GOLD")
            .addDefaultEventValue(WAEventParameterName.IAP_AMOUNT, 2000)
            .addDefaultEventValue(WAEventParameterName.LEVEL, 150)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_initiated_purchase
    /// </summary>
    void onGhwInitiatedPurchase()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.INITIATED_PURCHASE)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_purchase
    /// </summary>
    void onGhwPurchase()
    {
        Dictionary<string, object> values = new Dictionary<string, object>();
        values.Add(WAEventParameterName.ITEM_NAME, "BULLET");
        values.Add(WAEventParameterName.ITEM_AMOUNT, 100);
        values.Add(WAEventParameterName.PRICE, 10);
        values.Add(WAEventParameterName.LEVEL, 150);
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.COMPLETE_PURCHASE)
            .setDefaultValue(10)
            .setDefaultEventValues(values)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_level_achieved
    /// </summary>
    void onGhwLevelAchieved()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.LEVEL_ACHIEVED)
            .addDefaultEventValue(WAEventParameterName.LEVEL, 151)
            .addDefaultEventValue(WAEventParameterName.SCORE, 200000)
            .addDefaultEventValue(WAEventParameterName.FIGHTING, 5000000)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_user_create
    /// </summary>
    void onGhwUserCreate()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.USER_CREATED)
            .addDefaultEventValue(WAEventParameterName.REGISTER_TIME, DateTime.Now.Millisecond)
            .addDefaultEventValue(WAEventParameterName.ROLE_TYPE, 1)
            .addDefaultEventValue(WAEventParameterName.NICKNAME, "无极先生")
            .addDefaultEventValue(WAEventParameterName.VIP, 0)
            .addDefaultEventValue(WAEventParameterName.GAME_GOLD, 10000)
            .addDefaultEventValue(WAEventParameterName.GENDER, 0)
            .addDefaultEventValue(WAEventParameterName.BINDED_GAME_GOLD, 10000)
            .addDefaultEventValue(WAEventParameterName.LEVEL, 1)
            .addDefaultEventValue(WAEventParameterName.FIGHTING, 100)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_user_info_update
    /// </summary>
    void onUserInfoUPdate()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.USER_INFO_UPDATE)
            .addDefaultEventValue(WAEventParameterName.NICKNAME, "无极先生")
            .addDefaultEventValue(WAEventParameterName.ROLE_TYPE, 1)
            .addDefaultEventValue(WAEventParameterName.VIP, 1)
            .build()
            .track();
    }

    /// <summary>
    /// ghw_task_update
    /// </summary>
    void onGhwTaskUpdate()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.TASK_UPDATE)
            .addDefaultEventValue(WAEventParameterName.TASK_TYPE, "副本任务")
            .addDefaultEventValue(WAEventParameterName.TASK_STATUS, 2)
            .addDefaultEventValue(WAEventParameterName.TASK_NAME, "采药")
            .addDefaultEventValue(WAEventParameterName.TASK_ID, "2222")
            .build()
            .track();
    }

    /// <summary>
    /// ghw_gold_update
    /// </summary>
    void onGhwGoldUpdate()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.GOLD_UPDATE)
            .addDefaultEventValue(WAEventParameterName.APPROACH, "充值")
            .addDefaultEventValue(WAEventParameterName.AMOUNT, 1000)
            .addDefaultEventValue(WAEventParameterName.CURRENT_AMOUNT, 1100)
            .addDefaultEventValue(WAEventParameterName.GOLD_TYPE, 1)
            .build()
            .track();

    }

    /// <summary>
    /// ghw_user_import
    /// </summary>
    void onGhwUserImport()
    {
        new WAEvent.Builder()
            .setDefaultEventName(WAEventType.IMPORT_USER)
            .addDefaultEventValue(WAEventParameterName.IS_FIRST_ENTER, 0)
            .build()
            .track();
    }

    /// <summary>
    /// 自定义
    /// </summary>
    void onGhwSelf()
    {
        if(null == InputSelf)
        {
            return;
        }
        string eventName = InputSelf.text;
        if(null == eventName || eventName.Length == 0)
        {
            WASdkDemo.Instance.showShortToast("请输入自定义事件名称");
            return;
        }

        new WAEvent.Builder()
            .setDefaultEventName(eventName)
            .setDefaultValue(1)
            .build()
            .track();
    }

}