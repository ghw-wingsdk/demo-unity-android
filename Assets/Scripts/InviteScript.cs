﻿using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class InviteScript : MonoBehaviour
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

        // Facebook平台
        GameObject fbObj = GameObject.Find("BtnFacebook");
        Button btnFB = fbObj.GetComponent<Button>();
        btnFB.onClick.AddListener(delegate ()
        {
            OnFBClicked();
        });

        // VK平台
        GameObject vkObj = GameObject.Find("BtnVK");
        Button btnVK = vkObj.GetComponent<Button>();
        btnVK.onClick.AddListener(delegate ()
        {
            OnVKClicked();
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
        SceneManager.LoadScene("socail");
    }

    /// <summary>
    /// Facebook分享
    /// </summary>
    void OnFBClicked()
    {
        SceneManager.LoadScene("fbInvite");
    }

    void OnVKClicked()
    {
        SceneManager.LoadScene("vkInvite");
    }


}