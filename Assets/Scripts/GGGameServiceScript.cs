using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GGGameServiceScript : MonoBehaviour
{
    const string SIGN_IN = "登入";
    const string SIGN_OUT = "登出";
    static Text txtName;
    static Text txtBtnSign;

    static InputField inputUnlock;

    static InputField increaseId;
    static InputField increaseStep;

    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        GameObject txtNameObj = GameObject.Find("TxtName");
        txtName = txtNameObj.GetComponent<Text>();
        txtName.text = "您还没有登录Google游戏";

        // 登入、登出按钮
        GameObject siginObj = GameObject.Find("BtnSignIn");
        Button btnSign = siginObj.GetComponent<Button>();
        btnSign.onClick.AddListener(delegate ()
        {
            OnSignInClick(SIGN_IN.Equals(txtBtnSign.text));
        });
        txtBtnSign = btnSign.GetComponentInChildren<Text>();
        txtBtnSign.text = SIGN_IN;

        // 显示成就
        GameObject showObj = GameObject.Find("BtnShowAchievement");
        Button btnShow = showObj.GetComponent<Button>();
        btnShow.onClick.AddListener(delegate ()
        {
            OnShowAchievement();
        });

        // 解锁成就
        GameObject inputUnlockObj = GameObject.Find("UnlockId");
        inputUnlock = inputUnlockObj.GetComponent<InputField>();
        GameObject unlockObj = GameObject.Find("BtnUnlock");
        Button btnUnlock = unlockObj.GetComponent<Button>();
        btnUnlock.onClick.AddListener(delegate ()
        {
            OnUnlockAchievement(inputUnlock.text);
        });

        // 增加成就进度
        GameObject inputIncreaseIdObj = GameObject.Find("IncreaseId");
        increaseId = inputIncreaseIdObj.GetComponent<InputField>();
        GameObject inputIncreaseStepObj = GameObject.Find("IncreaseStep");
        increaseStep = inputIncreaseStepObj.GetComponent<InputField>();
        GameObject increaseObj = GameObject.Find("BtnIncrease");
        Button btnIncrease = increaseObj.GetComponent<Button>();
        btnIncrease.onClick.AddListener(delegate ()
        {
            OnIncreaseAchievement(increaseId.text, int.Parse(increaseStep.text));
        });

        signInGame();

    }


    void OnGUI()
    {

    }

    

    /// <summary>
    /// 登出
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("gameService");
    }

    /// <summary>
    /// 登录、登出
    /// </summary>
    void OnSignInClick(bool signIn)
    {
        if(signIn)
        {
            // 登入 
            signInGame();
        }
        else
        {
            // 登出
            signOut();
        }
    }

    /// <summary>
    /// 显示成就
    /// </summary>
    void OnShowAchievement()
    {
        if(WASocialProxy.Instance.isGameSignedIn(WAChannel.CHANNEL_GOOGLE))
        {
            WASocialProxy.Instance.displayAchievement(WAChannel.CHANNEL_GOOGLE, new DisplayAchievementCallback());
        } else
        {
            WASdkDemo.Instance.showShortToast("您还没有登录游戏服务");
        }
    }

    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <param name="achievementId"></param>
    void OnUnlockAchievement(string achievementId)
    {
        if (WASocialProxy.Instance.isGameSignedIn(WAChannel.CHANNEL_GOOGLE))
        {
            if (null == achievementId || achievementId.Length == 0)
            {
                WASdkDemo.Instance.showShortToast("成就ID不能为空");
                return;
            }
            WASdkDemo.Instance.showLoading("解锁成就");
            WASocialProxy.Instance.unlockAchievement(WAChannel.CHANNEL_GOOGLE, achievementId, new AchievementUpdateCallback("解锁成就"));
        }
        else
        {
            WASdkDemo.Instance.showShortToast("您还没有登录游戏服务");
        }
        
    }

    /// <summary>
    /// 增加成就完成步骤
    /// </summary>
    /// <param name="achievementId"></param>
    /// <param name="step"></param>
     void OnIncreaseAchievement(string achievementId, int step)
    {
        if (WASocialProxy.Instance.isGameSignedIn(WAChannel.CHANNEL_GOOGLE))
        {
            if (null == achievementId || achievementId.Length == 0)
            {
                WASdkDemo.Instance.showShortToast("成就ID不能为空");
                return;
            }
            if (step < 1)
            {
                WASdkDemo.Instance.showShortToast("变更步数不能小于1");
                return;
            }
            WASdkDemo.Instance.showLoading("递增分步成就");
            WASocialProxy.Instance.increaseAchievement(WAChannel.CHANNEL_GOOGLE, achievementId, step, new AchievementUpdateCallback("递增分步成就"));
        }
        else
        {
            WASdkDemo.Instance.showShortToast("您还没有登录游戏服务");
        }
        
    }

    /// <summary>
    /// 登入Google Game Service
    /// </summary>
    void signInGame()
    {
        WASdkDemo.Instance.showLoading("正在登录Google游戏服务");
        WASocialProxy.Instance.signInGame(WAChannel.CHANNEL_GOOGLE, new SignCallback());
    }

    void signOut()
    {
        WASocialProxy.Instance.signOutGame(WAChannel.CHANNEL_GOOGLE);
        txtName.text = "您还没有登录Google游戏";
        txtBtnSign.text = SIGN_IN;
    }


    class SignCallback : WACallback<WAPlayer>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("取消登录");
        }

        public override void onError(int code, string message, WAPlayer result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("登录失败！\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAPlayer result)
        {
            WASdkDemo.Instance.dismissLoading();
            string msg = "登录成功！\ncode: " + code + "\nmessage: " + message;
            if(null != result)
            {
                msg += "\nplayerId: " + result.playerId;
                msg += "\nname: " + result.name;
                msg += "\ndisplayName: " + result.displayName;
                msg += "\nplatform: " + result.platform;
                txtName.text = result.displayName;
                txtBtnSign.text = SIGN_OUT;
            }
            WASdkDemo.Instance.showShortToast(msg);
        }
    }

    class DisplayAchievementCallback : WACallback<WAResult>
    {
        public override void onCancel()
        {
        }

        public override void onError(int code, string message, WAResult result)
        {
            WASdkDemo.Instance.showShortToast("查看成就错误：！\ncode: " + code + "\nmessage: " + message);
            if(code == WAStatusCode.CODE_GAME_NEED_SIGN)
            {
                // 成就页面登出了账户，需要重新登录
                txtName.text = "您还没有登录Google游戏";
                txtBtnSign.text = SIGN_IN;
            }
        }

        public override void onSuccess(int code, string message, WAResult result)
        {
        }
    }

    /// <summary>
    /// 成就更新回调
    /// </summary>
    class AchievementUpdateCallback : WACallback<WAUpdateAchievementResult>
    {
        string opt;

        public AchievementUpdateCallback(string opt)
        {
            this.opt = opt;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("取消" + opt + "操作");
        }

        public override void onError(int code, string message, WAUpdateAchievementResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast(opt + "操作失败！\ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, WAUpdateAchievementResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast(opt + "操作成功！\ncode: " + code + "\nmessage: " + message 
                + (null == result ? "" : ("\nachievementId: " + result.achievementId)));
            
        }
    }
}