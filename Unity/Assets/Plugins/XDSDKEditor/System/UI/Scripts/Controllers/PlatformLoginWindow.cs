using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace xdsdk.Unity
{
    public class PlatformLoginWindow : UIElement
    {

        private AppInfo appInfo;

        public Button close;
        public Button back;
        public InputField username;
        public InputField password;
        public Button login;
        public Button forgot;
        public Button register;
        public Text error1;
        public Image error1Icon;
        public Text error2;
        public Image error2Icon;

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
                    if (extra.ContainsKey("app_info"))
                    {
                        appInfo = extra["app_info"] as AppInfo;
                    }
                }
            }
        }

        void Awake()
        {
            back.onClick.AddListener(() =>
            {
                OnCallback(SDKManager.RESULT_BACK, "Back button clicked.");
                GetSDKManager().Pop();
            });
            close.onClick.AddListener(() =>
            {
                OnCallback(SDKManager.RESULT_CLOSE, "Close button clicked.");
                GetSDKManager().PopAll();
            });
            forgot.onClick.AddListener(() =>
            {
                Application.OpenURL("http://xd.com/");
            });
            register.onClick.AddListener(() =>
            {
                Application.OpenURL("http://xd.com/");
            });
            login.onClick.AddListener(LoginXD);
            username.onValueChanged.AddListener((string value) =>
            {
                HideError1();
            });
            password.onValueChanged.AddListener((string value) =>
            {
                HideError2();
            });
            HideError1();
            HideError2();
        }

        private void LoginXD()
        {
            if (!string.IsNullOrEmpty(username.text) && !string.IsNullOrEmpty(password.text))
            {
                HideError1();
                HideError2();
                GetSDKManager().ShowLoading();
                Service.Login.XD(appInfo.Id, username.text, password.text, (Dictionary<string, object> dict) =>
                {
                    GetSDKManager().DismissLoading();
                    User user = dict["user"] as User;
                    String token = dict["token"] as string;
                    if (user.AuthorizationState == 0 && appInfo.NeedLoginRealName != 0)
                    {
                        Dictionary<string, object> config = new Dictionary<string, object>
                            {
                                {"token", token}
                            };
                        if (appInfo.NeedLoginRealName == 1)
                        {
                            config.Add("type", "required");
                        }
                        else
                        {
                            config.Add("type", "optional");
                        }
                        GetSDKManager().ShowRealName<RealNameWindow>(config, (int code, object result) =>
                        {
                            if (code == SDKManager.RESULT_SUCCESS)
                            {
                                Dictionary<string, object> resultDict = new Dictionary<string, object> {
                                        {"user" , user},
                                        {"token", token},
                                    };
                                OnCallback(SDKManager.RESULT_SUCCESS, resultDict);
                            }
                            else if (code == SDKManager.RESULT_CLOSE)
                            {
                                OnCallback(SDKManager.RESULT_CLOSE, result);
                            }
                            else
                            {
                                Debug.LogError(result.ToString());
                            }
                        });
                    }
                    else
                    {
                        OnCallback(SDKManager.RESULT_SUCCESS, dict);
                        GetSDKManager().PopAll();
                    }

                }, (string errorType, object errorContent) =>
                {
                    GetSDKManager().DismissLoading();
                    if (errorType != null && errorType.Equals("need_verification_code") && errorContent.GetType() == typeof(Dictionary<string, object>))
                    {
                        Dictionary<string, object> errorDic = errorContent as Dictionary<string, object>;
                        Dictionary<string, object> configs = new Dictionary<string, object>
                        {
                            {"app_info", appInfo},
                            {"user_id", (long)errorDic["user_id"]},
                            {"mobile", errorDic["mobile"] as string},
                            {"username", username.text},
                            {"password", password.text},
                        };
                        GetSDKManager().ShowTwoFactorAuth<TwoFactorAuthWindow>(configs, (int code, object data) =>
                        {
                            if (code == SDKManager.RESULT_SUCCESS)
                            {
                                OnCallback(SDKManager.RESULT_SUCCESS, data);
                            }
                            else if (code == SDKManager.RESULT_BACK)
                            {
                                Debug.Log(data);
                            }
                            else if (code == SDKManager.RESULT_CLOSE)
                            {
                                Debug.Log(data);
                                OnCallback(SDKManager.RESULT_CLOSE, data);
                            }
                            else
                            {
                                Debug.LogError(data);
                            }
                        });
                    } else if (errorContent != null){
                        ShowError2(errorContent.ToString());
                    }
                });
            } else if (string.IsNullOrEmpty(username.text)){
                ShowError1("请输入用户名");
            } else if (string.IsNullOrEmpty(password.text)){
                ShowError2("请输入密码");
            }
        }


        private void ShowError1(string error){
            error1.text = error;
            error1.gameObject.SetActive(true);
            error1Icon.gameObject.SetActive(true);
        }

        private void HideError1(){
            error1.gameObject.SetActive(false);
            error1Icon.gameObject.SetActive(false);
        }


        private void ShowError2(string error){
            error2.text = error;
            error2.gameObject.SetActive(true);
            error2Icon.gameObject.SetActive(true);
        }

        private void HideError2(){
            error2.gameObject.SetActive(false);
            error2Icon.gameObject.SetActive(false);
        }


    }
}
