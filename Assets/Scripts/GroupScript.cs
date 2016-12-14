using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GroupScript : MonoBehaviour
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

    void OnVKClicked()
    {
        SceneManager.LoadScene("vkGroup");
    }


}