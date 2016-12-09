using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameServiceScript : MonoBehaviour
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

        // Google平台
        GameObject ggObj = GameObject.Find("BtnGoogle");
        Button btnGG = ggObj.GetComponent<Button>();
        btnGG.onClick.AddListener(delegate ()
        {
            OnGGClicked();
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
    /// Google Game Service
    /// </summary>
    void OnGGClicked()
    {
        SceneManager.LoadScene("ggGameService");
    }


}