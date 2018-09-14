using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace xdsdk.Unity
{
    public class Demo : MonoBehaviour
    {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
        public Text result;

        public void Init()
        {
            string[] entries = { "XD_LOGIN", "QQ_LOGIN", "WX_LOGIN", "TAPTAP_LOGIN" };
            XDSDK.SetLoginEntries(entries);


            XDSDK.SetCallback((ResultCode code, string data) =>
            {

                Debug.Log("Code : " + code + "  " + "Data : " + data);
                result.text = "Code : " + code + "  " + "Data : " + data;
            });
            XDSDK.Init("a4d6xky5gt4c80s");

        }

        public void Login()
        {
            XDSDK.Login();
        }

        public void Logout()
        {
            XDSDK.Logout();
        }

        public void Pay()
        {
            Dictionary<string, string> info = new Dictionary<string, string>
            {
                { "OrderId", "1234567890123456789012345678901234567890" },
                { "Product_Price", "101" },
                { "EXT", "abcd|efgh|1234|5678" },
                { "Sid", "2" },
                { "Role_Id", "3" },
                { "Product_Id", "4" },
                { "Product_Name", "648大礼包" },
            };
            XDSDK.Pay(info);
        }

        public void OpenRealName(){
            XDSDK.OpenRealName();
        }
#endif
    }

}

