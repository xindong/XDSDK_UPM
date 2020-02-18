using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace xdsdk.Unity
{

    public class XDSDK
    {
        public static string VERSION = "XDSDK-Unity-1.0.1";

        public static void SetCallback(Action<ResultCode, string> callback)
        {
            XDCore.Instance.Callback += callback;
        }

        public static void SetLoginEntries(string[] entries)
        {
            XDCore.Instance.SetLoginEntries(entries);
        }

        public static void Init(string appid)
        {
            XDCore.Instance.Init(appid);
        }

        public static void Login()
        {
            XDCore.Instance.Login();
        }

        public static void Logout()
        {
            XDCore.Instance.Logout();
        }

        public static void Pay(Dictionary<string, string> info)
        {
            XDCore.Instance.Pay(info);
        }

        public static void OpenRealName(){
            XDCore.Instance.OpenRealName();
        }


        public static string GetAccessToken(){
            return XDCore.Instance.GetAccessToken();
        }



    }

}


