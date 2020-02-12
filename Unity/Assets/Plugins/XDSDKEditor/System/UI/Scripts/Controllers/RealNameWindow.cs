using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class RealNameWindow : UIElement
    {

        public Button close;
        public InputField realname;
        public InputField identifyNumber;
        public Button confirm;
        public Text error1;
        public Image error1Icon;
        public Text error2;
        public Image error2Icon;

        private string token;
        private string type;

        public override Dictionary<string, object> Extra
        {
            get
            {
                return extra;
            }

            set
            {
                extra = value;
                if (extra != null)
                {
                    if (extra.ContainsKey("token"))
                    {
                        token = extra["token"] as string;
                    }
                    if (extra.ContainsKey("type")){
                        type = extra["type"] as string;
                    }
                }
            }
        }


        void Awake()
        {
            close.onClick.AddListener(() =>
            {
                if(type.Equals("optional")){
                    OnCallback(SDKManager.RESULT_SUCCESS, "Close button clicked");
                } else {
                    OnCallback(SDKManager.RESULT_CLOSE, "Close button clicked");
                }
                GetSDKManager().PopAll();
            });
            realname.onValueChanged.AddListener((string content) =>
            {
                HideError1();
            });
            identifyNumber.onValueChanged.AddListener((string content) =>
            {
                HideError2();
            });
            confirm.onClick.AddListener(() =>
            {
                HideError1();
                HideError2();
                if(!string.IsNullOrEmpty(realname.text) && !string.IsNullOrEmpty(identifyNumber.text)){
                    GetSDKManager().ShowLoading();
                    Service.Login.RealName(token, realname.text, identifyNumber.text, (string result) => {
                        GetSDKManager().DismissLoading();
                        OnCallback(SDKManager.RESULT_SUCCESS, result);
                        GetSDKManager().PopAll();
                    }, (string error) => {
                        GetSDKManager().DismissLoading();
                        HideError1();
                        ShowError2(error);
                        Debug.LogError(error);
                    });
                } else if (string.IsNullOrEmpty(realname.text)){
                    ShowError1("请输入真实姓名");
                } else if (string.IsNullOrEmpty(identifyNumber.text)){
                    ShowError2("请输入身份证号");
                }
            });
            HideError1();
            HideError2();
        }


        private void ShowError1(string error)
        {
            error1.text = error;
            error1.gameObject.SetActive(true);
            error1Icon.gameObject.SetActive(true);
        }

        private void HideError1()
        {
            error1.gameObject.SetActive(false);
            error1Icon.gameObject.SetActive(false);
        }


        private void ShowError2(string error)
        {
            error2.text = error;
            error2.gameObject.SetActive(true);
            error2Icon.gameObject.SetActive(true);
        }

        private void HideError2()
        {
            error2.gameObject.SetActive(false);
            error2Icon.gameObject.SetActive(false);
        }


    }
}
