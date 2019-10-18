using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


namespace xdsdk.Unity
{


    public class XDCore
    {
        enum SDK_State
        {
            Initialize,
            Initializing,
            Initialized,
            LoggingIn,
            LoggedIn,
            Paying,
            RealName,
        };

        private volatile SDK_State state = SDK_State.Initialize;

        public event Action<ResultCode, string> Callback;

        private AppInfo appInfo;

        private User user;

        private GameObject managerObject;

        private readonly LoginEntry loginEntry = new LoginEntry();


        static XDCore instance;

        public static XDCore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new XDCore();
                }
                return instance;
            }
        }

        public void ClearCallback(){
            Callback = null;
        }

        public void SetLoginEntries(string[] entries)
        {
            loginEntry.SetEntries(entries);
        }

        public void HideGuest()
        {
            loginEntry.HideGuest();
        }

        public void HideWX()
        {
            loginEntry.HideWX();
        }

        public void HideQQ()
        {
            loginEntry.HideQQ();
        }

        public void HideTapTap()
        {
            loginEntry.HideTapTap();
        }

        public void Init(string appid)
        {

            if (Callback == null)
            {
                Debug.LogError("Please set callback first.");
                return;
            }
            if (state != SDK_State.Initialize)
            {
                if (state == SDK_State.Initialized)
                {
                    Callback(ResultCode.InitSucceed, null);

                }
                else
                {
                    Callback(ResultCode.InitFailed, "Do not call init while SDK was at state:" + state);
                }
            }
            else if (!string.IsNullOrEmpty(appid))
            {
                state = SDK_State.Initializing;

                ZenFulcrum.EmbeddedBrowser.UserAgent.
                          SetUserAgent("(Macintosh; Intel Mac OS X 10_9_5) Chrome/45.0.2454.93" + " "
                                      + "XDCustomUA/1" + " "
                                       + "XDUnitySDK " + XDSDK.VERSION);
                Service.Api.Instance.InitSDK(appid,
                                     (AppInfo info) =>
                                     {
                                         appInfo = info;
                                         managerObject = new GameObject
                                         {
                                             name = "XDUIManager"
                                         };
                                         managerObject.AddComponent<SDKManager>();
                                         managerObject.AddComponent<ZenFulcrum.EmbeddedBrowser.Browser>();


                                         UnityEngine.Object.DontDestroyOnLoad(managerObject);
                                         state = SDK_State.Initialized;
                                         Callback(ResultCode.InitSucceed, null);
                                     },
                                     (string error) =>
                                     {
                                         state = SDK_State.Initialize;
                                         Callback(ResultCode.InitFailed, error);
                                     });
            }
            else
            {
                Callback(ResultCode.InitFailed, "a re???");
            }


        }

        public void Login()
        {
            if (state == SDK_State.Initialized || state == SDK_State.LoggedIn)
            {
                state = SDK_State.LoggingIn;
                Service.Token.CheckThirdPartyToken(appInfo.Id, () =>
                {
                    if (!string.IsNullOrEmpty(Service.Token.GetToken(appInfo.Id)))
                    {

                        string token = Service.Token.GetToken(appInfo.Id);
                        Service.Login.User(Service.Token.GetToken(appInfo.Id), (User user) =>
                        {
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
                                SDKManager manager = managerObject.GetComponent<SDKManager>();

                                manager.ShowRealName<RealNameWindow>(config, (int code, object result) =>
                                {
                                    if (code == SDKManager.RESULT_SUCCESS) {
                                        Service.Login.User(token, (User userAfterRealName) => {
                                            this.user = userAfterRealName;
                                            state = SDK_State.LoggedIn;
                                            Callback(ResultCode.LoginSucceed, token);
                                        }, (String err) => {
                                            this.user = user;
                                            state = SDK_State.LoggedIn;
                                            Callback(ResultCode.LoginSucceed, token);
                                        });
                                    }
                                    else if (code == SDKManager.RESULT_CLOSE)
                                    {
                                        state = SDK_State.Initialized;
                                        Callback(ResultCode.LoginCanceled, result.ToString());
                                    }
                                    else
                                    {
                                        state = SDK_State.Initialized;
                                        Callback(ResultCode.LoginFailed, result.ToString());
                                        Debug.LogError(result.ToString());
                                    }
                                });
                            }
                            else
                            {
                                this.user = user;
                                state = SDK_State.LoggedIn;
                                Callback(ResultCode.LoginSucceed, token);
                            }
                        }, (string error) =>
                        {
                            Service.Token.ClearToken(appInfo.Id);
                            state = SDK_State.Initialized;
                            Debug.LogError(error);
                            Login();
                        });
                    }
                    else
                    {
                        SDKManager manager = managerObject.GetComponent<SDKManager>();
                        if (manager != null)
                        {
                            Dictionary<string, object> configs = new Dictionary<string, object>
                        {
                            { "app_info", appInfo },
                            { "login_entry", loginEntry}
                        };
                            manager.ShowLogin<MainLoginWindow>(configs, (int code, object data) =>
                            {
                                if (code == SDKManager.RESULT_SUCCESS)
                                {
                                    if (data.GetType() == typeof(Dictionary<string, object>))
                                    {
                                        Dictionary<string, object> resultDict = data as Dictionary<string, object>;
                                        Service.Token.SetToken(appInfo.Id, resultDict["token"] as string);
                                        user = resultDict["user"] as User;
                                        state = SDK_State.LoggedIn;
                                        Callback(ResultCode.LoginSucceed, resultDict["token"] as string);
                                    }
                                    else
                                    {
                                        state = SDK_State.Initialized;
                                        Callback(ResultCode.LoginFailed, data.ToString());
                                    }
                                }
                                else if (code == SDKManager.RESULT_BACK || code == SDKManager.RESULT_CLOSE)
                                {
                                    state = SDK_State.Initialized;
                                    Callback(ResultCode.LoginCanceled, data.ToString());
                                }
                                else
                                {
                                    state = SDK_State.Initialized;
                                    Callback(ResultCode.LoginFailed, data.ToString());
                                }
                            });
                        }
                        else
                        {
                            state = SDK_State.Initialized;
                            Callback(ResultCode.LoginFailed, "Could not open login window.");
                        }
                    }
                });
            }
            else
            {
                Callback(ResultCode.LoginFailed, "Pease do not call login in incorrect status (" + state + "" +
                         ").");
            }
        }

        public string GetAccessToken(){
            if(state == SDK_State.LoggedIn){
                return Service.Token.GetToken(appInfo.Id);
            } else {
                return null;
            }
        }


        public void Logout()
        {
            Service.Token.ClearToken(appInfo.Id);
            state = SDK_State.Initialized;
            Callback(ResultCode.LogoutSucceed, "");
        }

        public void Pay(Dictionary<string, string> info)
        {
            if (state == SDK_State.LoggedIn)
            {
                if (user.AuthorizationState == 0 && appInfo.NeedChargeRealName != 0)
                {
                    SDKManager manager = managerObject.GetComponent<SDKManager>();
                    Dictionary<string, object> config = new Dictionary<string, object>
                            {
                        {"token", Service.Token.GetToken(appInfo.Id)}
                            };
                    if (appInfo.NeedChargeRealName == 1)
                    {
                        config.Add("type", "required");
                    }
                    else
                    {
                        config.Add("type", "optional");
                    }
                    state = SDK_State.RealName;
                    manager.ShowRealName<RealNameWindow>(config, (int code, object result) =>
                    {
                        state = SDK_State.LoggedIn;
                        if (code == SDKManager.RESULT_SUCCESS)
                        {
                            Service.Login.User(Service.Token.GetToken(appInfo.Id), (User userAfterRealName) => {
                                this.user = userAfterRealName;
                                if (string.IsNullOrEmpty((string)result) || !result.ToString().Equals("Close button clicked"))
                                {
                                    Callback(ResultCode.RealNameSucceed, "");
                                }
                                PayWithoutRealNameCheck(info);
                            }, (String err) => {
                                if (string.IsNullOrEmpty((string)result) || !result.ToString().Equals("Close button clicked"))
                                {
                                    Callback(ResultCode.RealNameSucceed, "");
                                }
                                PayWithoutRealNameCheck(info);
                            });

                        }
                        else
                        {
                            Callback(ResultCode.PayCanceled, "RealName canceled.");
                        }
                    });
                }
                else
                {
                    PayWithoutRealNameCheck(info);
                }
            }
        }

        public void PayWithoutRealNameCheck(Dictionary<string, string> info)
        {
            if (state == SDK_State.LoggedIn)
            {
                state = SDK_State.Paying;
                SDKManager manager = managerObject.GetComponent<SDKManager>();
                Dictionary<string, object> confirmConfig = new Dictionary<string, object>
                    {
                        {"title","温馨提示"},
                        {"content", "即将跳转至外部浏览器进行支付，支付完成后请回到游戏进行下一步操作。"},
                        {"positive", "去支付"},
                        {"negative", "取消"}
                    };
                manager.ShowDialog(confirmConfig, (int code, object data) =>
                {
                    if (code == SDKManager.RESULT_SUCCESS)
                    {
                        Service.Payment.URL(appInfo.Id, Service.Token.GetToken(appInfo.Id), info, (string result) =>
                        {
                            Debug.Log(result);
                            Application.OpenURL(result);
                        }, (string error) =>
                        {

                        });
                        Dictionary<string, object> completeConfig = new Dictionary<string, object>
                        {
                        {"title","温馨提示"},
                        {"content", "请在新开页面中完成支付后提交。"},
                        {"positive", "已完成"},
                        {"negative", "返回修改"}
                        };
                        manager.ShowDialog(completeConfig, (int resultCode, object resultData) =>
                        {

                            if (resultCode == SDKManager.RESULT_SUCCESS)
                            {
                                state = SDK_State.LoggedIn;
                                Callback(ResultCode.PayCompleted, "");
                                manager.DismissDialog();
                            }
                            else
                            {
                                state = SDK_State.LoggedIn;
                                Callback(ResultCode.PayCanceled, "User canceled.");
                                manager.DismissDialog();
                            }
                        });
                    }
                    else
                    {
                        state = SDK_State.LoggedIn;
                        Callback(ResultCode.PayCanceled, "User canceled.");
                        manager.DismissDialog();
                    }
                });
            }
        }

        public void OpenRealName()
        {
            if (state == SDK_State.LoggedIn)
            {
                state = SDK_State.RealName;
                SDKManager manager = managerObject.GetComponent<SDKManager>();
                Dictionary<string, object> config = new Dictionary<string, object>
                            {
                    {"token", Service.Token.GetToken(appInfo.Id)},
                        {"type", "required"},
                            };
                manager.ShowRealName<RealNameWindow>(config, (int code, object result) =>
                {
                    state = SDK_State.LoggedIn;
                    if (code == SDKManager.RESULT_SUCCESS)
                    {
                        Callback(ResultCode.RealNameSucceed, "");
                    }
                    else
                    {
                        Callback(ResultCode.RealNameFailed, result.ToString());
                    }
                });
            }
            else
            {
                Callback(ResultCode.RealNameFailed, "Pease do not call realname in incorrect status (" + state + "" +
                         ").");
            }
        }
    }
}