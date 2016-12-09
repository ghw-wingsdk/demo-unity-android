using UnityEngine;
using System.Collections;
using WASdkAPI;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FBShareScript : MonoBehaviour
{

    private const int TYPE_IMAGE = 1;

    private const int TYPE_VIDEO = 2;

    private const int TYPE_FILE = 3;

    private static int shareType = 0;


    void Start()
    {
        // 返回
        GameObject backObj = GameObject.Find("BtnBack");
        Button btnBack = backObj.GetComponent<Button>();
        btnBack.onClick.AddListener(delegate ()
        {
            onBackClick();
        });

        // 对话框分享链接
        GameObject linkDialogObj = GameObject.Find("BtnLinkDialog");
        Button btnLinkDialog = linkDialogObj.GetComponent<Button>();
        btnLinkDialog.onClick.AddListener(delegate ()
        {
            OnShareLinkClicked(false);
        });
        // API分享链接
        GameObject linkAPIObj = GameObject.Find("BtnLinkAPI");
        Button btnLinkAPI = linkAPIObj.GetComponent<Button>();
        btnLinkAPI.onClick.AddListener(delegate ()
        {
            OnShareLinkClicked(true);
        });

        // 对话框分享图片
        GameObject imgDialogObj = GameObject.Find("BtnImgDialog");
        Button btnImgDialog = imgDialogObj.GetComponent<Button>();
        btnImgDialog.onClick.AddListener(delegate ()
        {
            OnShareImgClicked(false);
        });
        // API分享图片
        GameObject imgAPIObj = GameObject.Find("BtnImgAPI");
        Button btnImgAPI = imgAPIObj.GetComponent<Button>();
        btnImgAPI.onClick.AddListener(delegate ()
        {
            OnShareImgClicked(true);
        });

        // 对话框分享视频
        GameObject videoDialogObj = GameObject.Find("BtnVideoDialog");
        Button btnVideoDialog = videoDialogObj.GetComponent<Button>();
        btnVideoDialog.onClick.AddListener(delegate ()
        {
            OnShareVideoClicked(false);
        });
        // API分享视频
        GameObject videoAPIObj = GameObject.Find("BtnVideoAPI");
        Button btnVideoAPI = videoAPIObj.GetComponent<Button>();
        btnVideoAPI.onClick.AddListener(delegate ()
        {
            OnShareVideoClicked(true);
        });

        // 对话框分享Open Graph
        GameObject ogDialogObj = GameObject.Find("BtnOgDialog");
        Button btnOgDialog = ogDialogObj.GetComponent<Button>();
        btnOgDialog.onClick.AddListener(delegate ()
        {
            OnShareOgClicked(false);
        });
        // API分享Open Graph
        GameObject ogAPIObj = GameObject.Find("BtnOgAPI");
        Button btnOgAPI = ogAPIObj.GetComponent<Button>();
        btnOgAPI.onClick.AddListener(delegate ()
        {
            OnShareOgClicked(true);
        });

    }


    void OnGUI()
    {

    }

    

    /// <summary>
    /// 返回
    /// </summary>
    void onBackClick()
    {
        SceneManager.LoadScene("share");
    }

    /// <summary>
    /// 分享链接
    /// </summary>
    /// <param name="withAPI">是否通过API分享</param>
    void OnShareLinkClicked(bool withAPI)
    {
        WAShareLinkContent shareContent = new WAShareLinkContent.Builder()
            .setContentTitle("This is a link")
            .setContentDescription("This is a link about test")
            .setMessage("Click me")
            .setImageUri(new Uri("http://images.cnitblog.com/blog/542085/201306/26084327-aab644ec2a58492286461114e8a484e5.png"))
            .setContentUri(new Uri("https://www.baidu.com"))
            .build();
        WASocialProxy.Instance.share(WAChannel.CHANNEL_FACEBOOK, shareContent, withAPI, "", new ShareCallback());
    }

    /// <summary>
    /// 分享图片
    /// </summary>
    /// <param name="withAPI">通过API分享</param>
    void OnShareImgClicked(bool withAPI)
    {
        pickFile(TYPE_IMAGE, withAPI);
    }

    /// <summary>
    /// 分享视频
    /// </summary>
    /// <param name="withAPI">通过API分享</param>
    void OnShareVideoClicked(bool withAPI)
    {
        pickFile(TYPE_VIDEO, withAPI);
    }

    /// <summary>
    /// 分享Open Graph
    /// </summary>
    /// <param name="withAPI">通过API分享</param>
    void OnShareOgClicked(bool withAPI)
    {
        // 构建一个OpenGraphObject对象
        WAShareOpenGraphObject obj = new WAShareOpenGraphObject.Builder()
                .putString("og:type", "com_ghw_sdk:level")
                .putString("og:title", "A Game of Thrones")
                .putString("og:description", "In the frozen wastes to the north of Winterfell, sinister and supernatural forces are mustering.")
                .putString("og:image", "http://img.technews.tw/wp-content/uploads/2014/06/Softbank_Pepper-1_2--624x351.jpg")
                .build();

        // 构建一个OpenGraphAction对象
        WAShareOpenGraphAction action = new WAShareOpenGraphAction.Builder()
                .setActionType("com_ghw_sdk:reach")
                .putObject("level", obj)
                .build();

        // 构建一个OpenGraphContent对象
        WAShareOpenGraphContent shareContent = new WAShareOpenGraphContent.Builder()
                .setPreviewPropertyName("level")
                .setAction(action)
                .build();
        WASocialProxy.Instance.share(WAChannel.CHANNEL_FACEBOOK, shareContent, withAPI, "", new ShareCallback());
    }

    void pickFile(int type, bool shareWithAPI)
    {
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            shareType = type;
            activity.Call("pickFile", type, new PickFileCallback(shareWithAPI));
        }));
    }

    /// <summary>
    /// 分享回调
    /// </summary>
    class ShareCallback : WACallback<WAShareResult>
    {
        public override void onCancel()
        {
            WASdkDemo.Instance.showShortToast("Share onCancel");
        }

        public override void onError(int code, string message, WAShareResult result)
        {
            WASdkDemo.Instance.showShortToast("Share onError：" + message);
        }

        public override void onSuccess(int code, string message, WAShareResult result)
        {

            WASdkDemo.Instance.showShortToast("Share onSuccess: extra=" + (null == result ? "" : result.extra));
        }
    }

    class PickFileCallback : AndroidJavaProxy
    {
        private bool shareWithAPI = false;
        public PickFileCallback(bool shareWithAPI) : base("com.ghw.sdk2.model.PickFileCallback")
        {
            this.shareWithAPI = shareWithAPI;
        }

        void onCancel()
        {
            WASdkDemo.Instance.showShortToast("Pick file on cancel");
        }

        void onSuccess(int type, string path)
        {
            if (null == path || path.Length == 0)
            {
                WASdkDemo.Instance.showShortToast("Pick file failed: file path is empty");
                return;
            }
            WASdkDemo.Instance.showShortToast("Pick file on success! path=" + path);
            switch (shareType)
            {
                case TYPE_IMAGE:
                    WASharePhoto sharePhoto = new WASharePhoto.Builder().setImageUri(new Uri(path)).build();

                    WASharePhotoContent sharePhotoContent = new WASharePhotoContent.Builder().addPhoto(sharePhoto).build();

                    WASocialProxy.Instance.share(WAChannel.CHANNEL_FACEBOOK, sharePhotoContent, shareWithAPI, "", new ShareCallback());

                    break;
                case TYPE_VIDEO:
                    WAShareVideo shareVideo = new WAShareVideo.Builder().setLocalUri(new Uri(path)).build();
                    //WASharePhoto preViewPhoto = new WASharePhoto.Builder().setImageUri(new Uri("http://img.technews.tw/wp-content/uploads/2014/06/Softbank_Pepper-1_2--624x351.jpg")).build();

                    WAShareVideoContent shareVideoContent = new WAShareVideoContent.Builder()
                        //.setPreviewPhoto(preViewPhoto)
                        .setVideo(shareVideo)
                        .setContentTitle("Share video")
                        .setContentDescription("This is share video by using sdk for unity3d")
                        .build();

                    WASocialProxy.Instance.share(WAChannel.CHANNEL_FACEBOOK, shareVideoContent, shareWithAPI, "", new ShareCallback());
                    break;
                case TYPE_FILE:
                default:
                    WASdkDemo.Instance.showShortToast("Can't share file" + path);
                    break;
            }
            
        }

    }
}