using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class VKGroupScript : MonoBehaviour
{
    private static Text txtNickname;
    private static Text txtId;

    static InputField searchGroupId;

    static InputField isMemberGroupId;

    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 获取账户信息
        GameObject getAccoutnInfoObj = GameObject.Find("BtnGetAccountInfo");
        Button btnGetAccountInfo = getAccoutnInfoObj.GetComponent<Button>();
        btnGetAccountInfo.onClick.AddListener(delegate ()
        {
            OnGetAccountInfoClicked();
        });


        GameObject txtNicknameObj = GameObject.Find("txtNickname");
        txtNickname = txtNicknameObj.GetComponent<Text>();

        GameObject txtIdObj = GameObject.Find("txtId");
        txtId = txtIdObj.GetComponent<Text>();

        // 搜索Group id
        GameObject searchGroupIdObj = GameObject.Find("SearchGroupId");
        searchGroupId = searchGroupIdObj.GetComponent<InputField>();

        // 搜索Group
        GameObject searchGroupObj = GameObject.Find("BtnSearchGroup");
        Button btnSearchGroup = searchGroupObj.GetComponent<Button>();
        btnSearchGroup.onClick.AddListener(delegate ()
        {
            OnSearchGroupClicked();
        });

        // App关联Group
        GameObject linkedGroupObj = GameObject.Find("BtnLinkedGroup");
        Button btnLinkedGroup = linkedGroupObj.GetComponent<Button>();
        btnLinkedGroup.onClick.AddListener(delegate ()
        {
            OnLinkedGroupClicked();
        });

        // 已加入的Group
        GameObject joinedGroupObj = GameObject.Find("BtnJoinedGroup");
        Button btnJoinedGroup = joinedGroupObj.GetComponent<Button>();
        btnJoinedGroup.onClick.AddListener(delegate ()
        {
            OnJoinedGroupClicked();
        });

        // 是否为成员Group id
        GameObject isMemberGroupIdObj = GameObject.Find("IsMemberGroupId");
        isMemberGroupId = isMemberGroupIdObj.GetComponent<InputField>();

        // 查询当前用户是否为成员
        GameObject isMemberGroupObj = GameObject.Find("BtnQuery");
        Button btnisMemberGroup = isMemberGroupObj.GetComponent<Button>();
        btnisMemberGroup.onClick.AddListener(delegate ()
        {
            OnisMemberClicked();
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
        SceneManager.LoadScene("group");
    }

    /// <summary>
    /// 获取账户信息
    /// </summary>
    void OnGetAccountInfoClicked()
    {
        WASdkDemo.Instance.showLoading("正在获取账户信息");
        WAUserProxy.Instance.getAccountInfo(WAChannel.CHANNEL_VK, new GetAccountInfoCallback());
    }


    /// <summary>
    /// 搜索Group
    /// </summary>
    void OnSearchGroupClicked()
    {
        if(null == searchGroupId)
        {
            return;
        }
        string id = searchGroupId.text;
        if(null == id || id.Length == 0)
        {
            return;
        }
        string [] ids = id.Split(',');
        WASdkDemo.Instance.showLoading("查询中");
        WASocialProxy.Instance.getGroupByGid(WAChannel.CHANNEL_VK, ids, "", new GetGroupCallback(WASdkDemo.TYPE_GROUP_SEARCH));
    }

    /// <summary>
    /// App关联的Group
    /// </summary>
    void OnLinkedGroupClicked()
    {
        WASdkDemo.Instance.showLoading("查询中");
        WASocialProxy.Instance.getCurrentAppLinkedGroup(WAChannel.CHANNEL_VK, "", new GetGroupCallback(WASdkDemo.TYPE_GROUP_APP_LINKED));
    }

    /// <summary>
    /// 加入的Group
    /// </summary>
    void OnJoinedGroupClicked()
    {
        WASdkDemo.Instance.showLoading("查询中");
        WASocialProxy.Instance.getCurrentUserGroup(WAChannel.CHANNEL_VK, "", new GetGroupCallback(WASdkDemo.TYPE_GROUP_JOINED));
    }

    /// <summary>
    /// 是否为Group的成员
    /// </summary>
    void OnisMemberClicked()
    {
        if (null == searchGroupId)
        {
            return;
        }
        string id = isMemberGroupId.text;
        if (null == id || id.Length == 0)
        {
            return;
        }
        string[] ids = id.Split(',');
        WASdkDemo.Instance.showLoading("查询中");
        WASocialProxy.Instance.isCurrentUserGroupMember(WAChannel.CHANNEL_VK, ids[0], "", new IsMemberCallback(ids[0]));
    }


    class GetAccountInfoCallback : WACallback<WAUser>
    {

        public override void onSuccess(int code, string message, WAUser result)
        {
            WASdkDemo.Instance.dismissLoading();
            string msg = "Get account info success!";
            msg += "\ncode: " + code;
            msg += "\nmessage: " + message;
            msg += "\nid: " + result.id;
            msg += "\nnickname: " + result.name;
            msg += "\nplatform: " + result.platform;
            msg += "\nwaUserId: " + result.waUserId;
            WASdkDemo.Instance.showShortToast(msg);

            if (null != txtNickname)
            {
                txtNickname.text = "昵称：" + result.name;
            }

            if (null != txtId)
            {
                txtId.text = "ID：" + result.id;
            }
        }

        /**
         * 取消回调
         */
        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Canceled");
        }

        /**
         * 错误回调
         * @param code 结果码
         * @param message 结果描述消息
         * @param result 结果数据，如果没有结果返回null
         * @param throwable 异常信息，没有返回null
         */
        public override void onError(int code, string message, WAUser result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Login failed!\n" + message);
            if (null != txtNickname)
            {
                txtNickname.text = "code：" + code;
            }

            if (null != txtId)
            {
                txtId.text = "message：" + message;
            }
        }

    }

    

    class GetGroupCallback : WACallback<WAGroupResult>
    {
        private int type;

        public GetGroupCallback(int type)
        {
            this.type = type;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Canceled");

        }

        public override void onError(int code, string message, WAGroupResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("onError: \ncode: " + code + "\nmessage: " + message);

        }

        public override void onSuccess(int code, string message, WAGroupResult result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.onGroupResult(WASdkDemo.TYPE_GROUP_SEARCH, result);
        }
    }

    class IsMemberCallback : WACallback<bool>
    {
        string id;

        public IsMemberCallback(string id)
        {
            this.id = id;
        }

        public override void onCancel()
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("Canceled");
        }

        public override void onError(int code, string message, bool result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.showShortToast("onError: \ncode: " + code + "\nmessage: " + message);
        }

        public override void onSuccess(int code, string message, bool result)
        {
            WASdkDemo.Instance.dismissLoading();
            WASdkDemo.Instance.onIsGroupMemberResult(id, result);
        }
    }
}