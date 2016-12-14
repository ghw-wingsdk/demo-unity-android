using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AlertDialog4Android
{


    /// <summary>
    /// The identifier for the positive button.
    /// </summary>
    public const int BUTTON_POSITIVE = -1;

    /// <summary>
    /// The identifier for the negative button. 
    /// </summary>
    public const int BUTTON_NEGATIVE = -2;

    public class Builder
    {
        AndroidJavaObject builderObject;

        public Builder()
        {
            AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
            builderObject = new AndroidJavaObject("android.app.AlertDialog$Builder", activity);
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="title"></param>
        /// <returns>Builder对象</returns>
        public Builder setTitle(string title)
        {
            builderObject.Call<AndroidJavaObject>("setTitle", title);
            return this;
        }

        /// <summary>
        /// 设置消息内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setMessage(string message)
        {
            builderObject.Call<AndroidJavaObject>("setMessage", message);
            return this;
        }

        /// <summary>
        /// 设置列表项
        /// </summary>
        /// <param name="items">列表数据</param>
        /// <param name="listener">列表数据点击监听</param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setItems(string [] items, OnClickListener listener)
        {
            builderObject.Call<AndroidJavaObject>("setItems", items, listener);
            return this;
        }

        /// <summary>
        /// 设置单选列表项
        /// </summary>
        /// <param name="items">列表数据</param>
        /// <param name="checkedItem">默认选中项，没有可以穿-1</param>
        /// <param name="listener">列表数据选中监听</param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setSingleChoiceItems(string [] items, int checkedItem, OnClickListener listener)
        {
            builderObject.Call<AndroidJavaObject>("setSingleChoiceItems", items, checkedItem, listener);
            return this;
        }

        /// <summary>
        /// 设置多选列表项
        /// </summary>
        /// <param name="items">列表数据</param>
        /// <param name="checkedItems">默认选中项</param>
        /// <param name="listener">选中监听</param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setMultiChoiceItems(string [] items, bool [] checkedItems, OnMultiChoiceClickListener listener)
        {
            builderObject.Call<AndroidJavaObject>("setMultiChoiceItems", items, checkedItems, listener);
            return this;
        }

        /// <summary>
        /// 设置Positive按钮
        /// </summary>
        /// <param name="positiveBtnStr">按钮文字</param>
        /// <param name="listener">点击监听</param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setPositiveButton(string positiveBtnStr, OnClickListener listener)
        {
            builderObject.Call<AndroidJavaObject>("setPositiveButton", positiveBtnStr, listener);
            return this;
        }

        /// <summary>
        /// 设置negative按钮
        /// </summary>
        /// <param name="negativeBtnStr">按钮文字</param>
        /// <param name="listener">点击监听</param>
        /// <returns>Android AlertDialog Builder对象</returns>
        public Builder setNegativeButton(string negativeBtnStr, OnClickListener listener)
        {
            builderObject.Call<AndroidJavaObject>("setNegativeButton", negativeBtnStr, listener);
            return this;
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <returns>Android AlertDialog对象</returns>
        public AndroidJavaObject show()
        {
            AndroidJavaObject alertDialogObject = builderObject.Call<AndroidJavaObject>("show");
            alertDialogObject.Call("setCanceledOnTouchOutside", false);
            return alertDialogObject;
        }

    }


    /// <summary>
    /// 对话框点击监听（对话框按钮、对话框列表项，对话框单选）
    /// </summary>
    public abstract class OnClickListener : AndroidJavaProxy
    {
        public OnClickListener() : base("android.content.DialogInterface$OnClickListener")
        {

        }

        public abstract void onClick(AndroidJavaObject dialog, int which);
    }

    /// <summary>
    /// 对话框多选列表选择监听
    /// </summary>
    public abstract class OnMultiChoiceClickListener : AndroidJavaProxy
    {
        public OnMultiChoiceClickListener() : base("android.content.DialogInterface$OnMultiChoiceClickListener")
        {

        }
        /**
         * This method will be invoked when an item in the dialog is clicked.
         * 
         * @param dialog The dialog where the selection was made.
         * @param which The position of the item in the list that was clicked.
         * @param isChecked True if the click checked the item, else false.
         */
        public abstract void onClick(AndroidJavaObject dialog, int which, bool isChecked);
    }
}
