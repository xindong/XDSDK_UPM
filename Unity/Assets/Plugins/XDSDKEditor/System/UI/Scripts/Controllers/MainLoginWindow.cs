using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class MainLoginWindow : UIElement
    {
        private AppInfo appInfo;
        private LoginEntry loginEntry;

        public Button loginButton1st;
        public Button loginButton2nd;
        public Button loginButton3rd;
        public Button loginButton4th;

        public Button close;


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
                    if (extra.ContainsKey("login_entry"))
                    {
                        loginEntry = extra["login_entry"] as LoginEntry;
                        ChangeLoginEntries();
                    }
                }
            }
        }


        void Awake()
        {
            loginButton1st.onClick.AddListener(OnFirstEntryClicked);
            loginButton2nd.onClick.AddListener(OnSecondEntryClicked);
            loginButton3rd.onClick.AddListener(OnThirdEntryClicked);
            loginButton4th.onClick.AddListener(OnFourthEntryClicked);
            close.onClick.AddListener(OnCloseClicked);
        }

        private void ChangeLoginEntries()
        {
            if (loginEntry != null)
            {
                ChangeLoginEntry(loginButton1st, loginEntry.First, "center");
                ChangeLoginEntry(loginButton2nd, loginEntry.Second, "center");
                ChangeLoginEntry(loginButton3rd, loginEntry.Third, "bottom");
                ChangeLoginEntry(loginButton4th, loginEntry.Fourth, "bottom");
            }

        }

        private void ChangeLoginEntry(Button button, LoginEntry.Type type, string position)
        {
            if (type != LoginEntry.Type.None)
            {
                button.gameObject.SetActive(true);
                switch (type)
                {
                    case LoginEntry.Type.QQ:
                        ChangeButtonSprite(button, "xdsdk_unity_login_" + position + "_qq");
                        break;
                    case LoginEntry.Type.Tap:
                        ChangeButtonSprite(button, "xdsdk_unity_login_" + position + "_taptap");
                        break;
                    case LoginEntry.Type.WX:
                        ChangeButtonSprite(button, "xdsdk_unity_login_" + position + "_wechat");
                        break;
                    case LoginEntry.Type.Guest:
                        ChangeButtonSprite(button, "xdsdk_unity_login_" + position + "_guest");
                        break;
                    case LoginEntry.Type.XD:
                        ChangeButtonSprite(button, "xdsdk_unity_login_" + position + "_xindong");
                        break;
                    default:
                        button.gameObject.SetActive(false);
                        break;
                }
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }

        private void ChangeButtonSprite(Button button, string spriteName)
        {
            button.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
            SpriteState spriteState = button.spriteState;
            spriteState.pressedSprite = Resources.Load<Sprite>("Sprites/" + spriteName + "_pressed");
            button.spriteState = spriteState;
        }

        private void OnFirstEntryClicked()
        {
            LoginByType(loginEntry.First);
            Debug.Log(name + " " + "OnFirstEntryClicked");
        }

        private void OnSecondEntryClicked()
        {
            LoginByType(loginEntry.Second);
            Debug.Log(name + " " + "OnSecondEntryClicked");
        }
        private void OnThirdEntryClicked()
        {
            LoginByType(loginEntry.Third);
            Debug.Log(name + " " + "OnThirdEntryClicked");
        }
        private void OnFourthEntryClicked()
        {
            LoginByType(loginEntry.Fourth);
            Debug.Log(name + " " + "OnFourthEntryClicked");
        }

        private void LoginByType(LoginEntry.Type type)
        {
            Dictionary<string, object> config = null;
            switch (type)
            {
                case LoginEntry.Type.WX:
                    config = new Dictionary<string, object>
                    {
                        { "url", "https://www.xd.com/oauth/sdk_weixin_login?client_id=" + appInfo.Id },
                        { "contains_url", "oauth/sdk_weixin_callback"}
                    };
                    GetSDKManager().ShowWebView<WebWindow>(config, (code, data) =>
                    {
                        if (code == SDKManager.RESULT_SUCCESS)
                        {
                            HandleWXLogin((string)data);
                        }
                        else if (code == SDKManager.RESULT_BACK)
                        {
                            Debug.Log(data);
                        }
                        else
                        {
                            Debug.LogError(data);
                        }

                    });
                    break;
                case LoginEntry.Type.QQ:
                    config = new Dictionary<string, object>
                    {
                        { "url", "https://www.xd.com/oauth/sdk_qq_login" },
                        { "contains_url", "oauth/sdk_qq_token"}
                    };
                    GetSDKManager().ShowWebView<WebWindow>(config, (code, data) =>
                    {
                        if (code == SDKManager.RESULT_SUCCESS)
                        {
                            HandleQQLogin((string)data);
                        }
                        else if (code == SDKManager.RESULT_BACK)
                        {
                            Debug.Log(data);
                        }
                        else
                        {
                            Debug.LogError(data);
                        }
                    });
                    break;
                case LoginEntry.Type.Tap:
                    if (string.IsNullOrEmpty(appInfo.TaptapClientID))
                    {
                        Debug.LogError("TapTap client id has not been set.");
                        return;
                    }
                    String tapBaseUrl = "https://www.taptap.com/oauth2/v1/authorize";
                    Dictionary<string, string> tapParameters = new Dictionary<string, string>
                    {
                        {"client_id", appInfo.TaptapClientID},
                        {"response_type" , "token"},
                        {"version" , XDSDK.VERSION},
#if UNITY_STANDALONE_OSX
                        {"platform" , "OSX"},
#endif

#if UNITY_STANDALONE_WIN
                        {"platform" , "Windows"},
#endif
                        {"scope", "public_profile"},
                        {"redirect_uri","tapoauth://authorize"},
                        {"state", DataStorage.GetUniqueID()},
                        {"secret_type", "hmac-sha-1"},
                        {"info", "{}"},
                        {"region","cn"}
                    };

                    config = new Dictionary<string, object>
                    {
                        { "url", tapBaseUrl + "?" + Net.DictToQueryString(tapParameters) },
                        { "contains_url", "tapoauth://authorize#"}
                    };
                    GetSDKManager().ShowWebView<WebWindow>(config, (code, data) =>
                    {
                        if (code == SDKManager.RESULT_SUCCESS)
                        {
                            HandleTapLogin((string)data);
                        }
                        else if (code == SDKManager.RESULT_BACK)
                        {
                            Debug.Log(data);
                        }
                        else
                        {
                            Debug.LogError(data);
                        }
                    });
                    break;
                case LoginEntry.Type.XD:
                    config = new Dictionary<string, object>
                        {
                            { "app_info", appInfo },
                        };
                    GetSDKManager().ShowPlatformLogin<PlatformLoginWindow>(config, (code, data) =>
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
                    break;
                default:
                    break;
            }

        }

        private void HandleQQLogin(string url)
        {
            try
            {
                NameValueCollection collection = new NameValueCollection();
                string baseUrl;
                Net.ParseUrl(url, out baseUrl, out collection);
                if (collection.Get("openid") != null && collection.Get("access_token") != null)
                {
                    GetSDKManager().ShowLoading();
                    Service.Login.QQ(appInfo.Id,
                                    collection.Get("openid") as string,
                                    collection.Get("access_token") as string,
                             (Dictionary<string, object> resultDict) =>
                                    {
                                        GetSDKManager().DismissLoading();
                                        OnCallback(SDKManager.RESULT_SUCCESS, resultDict);
                                        GetSDKManager().PopAll();
                                    },
                                    (string error) =>
                                    {
                                        GetSDKManager().DismissLoading();
                                        Debug.LogError("QQ login failed." + error);
                                    });
                }
            }
            catch (Exception e)
            {
                Debug.LogError("QQ login failed." + e.Message);
            }
        }

        private void HandleWXLogin(string url)
        {
            try
            {
                NameValueCollection collection = new NameValueCollection();
                string baseUrl;
                Net.ParseUrl(url, out baseUrl, out collection);
                if (collection.Get("code") != null)
                {
                    GetSDKManager().ShowLoading();
                    Service.Login.Wechat(appInfo.Id,
                                    collection.Get("code") as string,
                             (Dictionary<string, object> resultDict) =>
                             {
                                 GetSDKManager().DismissLoading();
                                 OnCallback(SDKManager.RESULT_SUCCESS, resultDict);
                                 GetSDKManager().PopAll();
                             },
                                    (string error) =>
                                    {
                                        GetSDKManager().DismissLoading();
                                        Debug.LogError("Wechat login failed." + error);
                                    });
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Wechat login failed." + e.Message);
            }
        }

        private void HandleTapLogin(string url)
        {
            try
            {
                NameValueCollection collection = new NameValueCollection();
                string baseUrl;
                Net.ParseUrl(url.Replace("#", "?"), out baseUrl, out collection);
                if (collection.Get("kid") != null &&
                    collection.Get("access_token") != null &&
                    collection.Get("token_type") != null &&
                    collection.Get("mac_key") != null &&
                    collection.Get("mac_algorithm") != null)
                {
                    GetSDKManager().ShowLoading();
                    Service.Login.TapTap(appInfo.Id,
                                        collection.Get("kid") as string,
                                       collection.Get("access_token") as string,
                                       collection.Get("token_type") as string,
                                       collection.Get("mac_key") as string,
                                       collection.Get("mac_algorithm") as string,
                                       (Dictionary<string, object> resultDict) =>
                                       {
                                           GetSDKManager().DismissLoading();
                                           OnCallback(SDKManager.RESULT_SUCCESS, resultDict);
                                           GetSDKManager().PopAll();
                                       }, (string error) =>
                                        {
                                            GetSDKManager().DismissLoading();
                                            Debug.LogError("TapTap login failed." + error);
                                        });
                }
            }
            catch (Exception e)
            {
                Debug.LogError("TapTap login failed." + e.Message);
            }
        }


        private void OnCloseClicked()
        {
            OnCallback(SDKManager.RESULT_CLOSE, "Close button clicked");
            GetSDKManager().PopAll();
        }

    }
}
