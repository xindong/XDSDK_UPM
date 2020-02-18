using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class TwoFactorAuthWindow : UIElement
    {

        private AppInfo appInfo;
        private long userID;
        private string username;
        private string password;


        public Button close;
        public Button back;
        public Text mobile;
        public InputField code;
        public Button send;
        public Button confirm;
        public Text errorText;
        public Image errorIcon;

        private static double lastFetchTime = -60000f;
        private static DateTime dateTime = new DateTime(1970, 1, 1);



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
                    if (extra.ContainsKey("user_id")){
                        userID = (long)extra["user_id"];
                    }
                    if (extra.ContainsKey("username"))
                    {
                        username = extra["username"] as string;
                    }
                    if (extra.ContainsKey("password")){
                        password = extra["password"] as string;
                    }
                    if (extra.ContainsKey("mobile")){
                        mobile.text = extra["mobile"] as string;
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

            send.onClick.AddListener(() =>
            {
                GetSDKManager().ShowLoading();
                Service.Login.VerificationCode(appInfo.Id, userID.ToString(), null, null, null, "login", (string result) => {
                    GetSDKManager().DismissLoading();
                    lastFetchTime = (DateTime.UtcNow - dateTime).TotalMilliseconds;
                }, (string error) => {
                    GetSDKManager().DismissLoading();
                    Debug.Log(error);
                });

            });

            confirm.onClick.AddListener(() =>
            {
                if(!string.IsNullOrEmpty(code.text)){
                    HideError();
                    GetSDKManager().ShowLoading();
                    Service.Login.XD(appInfo.Id, username, password, code.text, (Dictionary<string, object> dict) => {
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
                    }, (string errorType, object errorContent) => {
                        GetSDKManager().DismissLoading();
                        if (errorContent != null){
                            if(errorContent.GetType() == typeof(Dictionary<string, object>) && 
                               (errorContent as Dictionary<string, object>).ContainsKey("error")){
                                ShowError((errorContent as Dictionary<string, object>)["error"].ToString());
                            } else {
                                ShowError(errorContent.ToString());
                            }

                        }

                        Debug.LogError(errorType + ":" + errorContent);
                    });
                } else {
                    ShowError("请填写验证码");
                }
            });

            code.onValueChanged.AddListener((string content) =>
            {
                HideError();
            });
            HideError();
        }

        void Update()
        {
            double current = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            if (current - lastFetchTime < 60000){
                send.interactable = false;
                Text text = send.GetComponentInChildren<Text>();
                text.text = (int)((60000 + lastFetchTime - current)/1000) + "s";
            } else if(!send.interactable)
            {
                send.interactable = true;
                Text text = send.GetComponentInChildren<Text>();
                text.text = "发送验证码";
            }
        }

        private void ShowError(string error)
        {
            errorText.text = error;
            errorText.gameObject.SetActive(true);
            errorIcon.gameObject.SetActive(true);
        }

        private void HideError()
        {
            errorText.gameObject.SetActive(false);
            errorIcon.gameObject.SetActive(false);
        }
    }
}
