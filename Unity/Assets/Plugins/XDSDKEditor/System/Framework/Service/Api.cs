using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace xdsdk.Unity.Service
{
    public class Api
    {


        private static Api instance;

        private static GameObject netObject;


        private readonly static string BASE_URL = "https://api.xd.com/v1";
        private readonly static string BASE_URL_V2 = "https://api.xd.com/v2";

        private readonly static string INIT_SDK = BASE_URL_V2 + "/sdk/appid_info";

        private readonly static string XD_AUTH = BASE_URL + "/authorizations";
        private readonly static string QQ_AUTH = BASE_URL_V2 + "/authorizations/qq";
        private readonly static string WX_AUTH = BASE_URL_V2 + "/authorizations/weixin";
        private readonly static string TAP_AUTH = BASE_URL_V2 + "/authorizations/taptap";
        private readonly static string VERIFY_CODE = BASE_URL + "/authorizations/get_mobile_verify_code";

        private readonly static string WX_CHECK = BASE_URL_V2 + "/authorizations/refresh_wx_token";
        private readonly static string QQ_CHECK = BASE_URL_V2 + "/authorizations/check_qq_token";
        private readonly static string TAPTAP_CHECK = BASE_URL_V2 + "/authorizations/check_taptap_token";

        private readonly static string USER = BASE_URL + "/user";
        private readonly static string REALNAME = BASE_URL + "/user/user_fcm_set";
        private readonly static string REALNAME_INFO = BASE_URL + "/user/get_shiming_info";
        private readonly static string BIND_MOBILE_AND_REALNAME = BASE_URL + "/user/bind_mobile_and_shiming";
        private readonly static string BIND_MOBILE = BASE_URL + "/user/bind_mobile";
        private readonly static string LOGOUT = BASE_URL + "/users/get_auth_url";

        private readonly static string FCM_PLAY_LOG = BASE_URL_V2 + "/fcm/set_play_log";

        private readonly static string FCM_SERVER_TIME = BASE_URL_V2 + "/fcm/get_server_time";

 

        public static Api Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Api();

                }
                if (netObject == null)
                {
                    instance.Init();
                }
                return instance;
            }
        }

        private void Init()
        {
            netObject = new GameObject
            {
                name = "XDSDKNet"
            };
            netObject.AddComponent<Net>();
            UnityEngine.Object.DontDestroyOnLoad(netObject);
        }

        public void InitSDK(string appid, Action<AppInfo> methodForResult, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"client_id" , appid}
            };
            net.GetOnServer(INIT_SDK, parameters, (string data) =>
           {
               try
               {
                   Debug.Log("Init Success : " + data);
                   Dictionary<string, object> appinfoDict = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
                   AppInfo info = AppInfo.InitWithDict(appinfoDict);
                   methodForResult(info);
               }
               catch (Exception e)
               {
                   Debug.LogError("Init Failed : " + e.Message);
                   methodForError("Init Failed : " + e.Message);
               }

           }, (int code, string error) =>
           {

               Debug.LogError("Init Failed : " + error);
               methodForError("Init Failed : " + error);
           });
        }

        public void LoginQQ(string appid, string openID, string accessToken, Action<string> methodForResult, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            List<string> scopes = new List<string>
            {
                "user",
                "sdk"
            };
            Dictionary<string, object> paramaters = new Dictionary<string, object>
            {
                { "client_id", appid },
                { "qq_access_token", accessToken },
                { "qq_openid", openID },
                { "scopes", scopes},
                { "auth_type", "web"},
            };
            net.PostOnServer(QQ_AUTH, paramaters, (string data) =>
            {
                try
                {
                    Debug.Log("QQ Login Success : " + data);
                    Dictionary<string, object> resultDict = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
                    if (resultDict.ContainsKey("access_token"))
                    {
                        methodForResult(resultDict["access_token"] as string);
                    }
                    else
                    {
                        methodForError("Could not get QQ access token.");
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message);
                    }
                }

            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });


        }

        public void LoginWechat(string appid, string wechatCode, Action<string, string> methodForResult, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            List<string> scopes = new List<string>
            {
                "user",
                "sdk"
            };
            Dictionary<string, object> paramaters = new Dictionary<string, object>
            {
                { "client_id", appid },
                { "weixin_code", wechatCode },
                { "scopes", scopes},
                { "auth_type", "web"},
            };
            net.PostOnServer(WX_AUTH, paramaters, (string data) =>
            {
                try
                {
                    Debug.Log("Wechat Login Success : " + data);
                    Dictionary<string, object> resultDict = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
                    if (resultDict.ContainsKey("access_token") && resultDict.ContainsKey("refresh_token"))
                    {
                        methodForResult(resultDict["access_token"] as string, resultDict["refresh_token"] as string);
                    }
                    else
                    {
                        methodForError("Could not get Wechat access token.");
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message);
                    }
                }

            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void LoginTapTap(string appid, string kid, string accessToken, string tokenType, string macKey, string macAlgorithm,
                                Action<string> methodForResult, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            List<string> scopes = new List<string>
            {
                "user",
                "sdk"
            };
            Dictionary<string, object> paramaters = new Dictionary<string, object>
            {
                { "client_id", appid },
                { "kid", kid },
                { "access_token", accessToken },
                { "token_type", tokenType },
                { "mac_key", macKey },
                { "mac_algorithm", macAlgorithm },
                { "scopes", scopes},
                { "auth_type", "app"},
            };
            net.PostOnServer(TAP_AUTH, paramaters, (string data) =>
            {
                try
                {
                    Debug.Log("TapTap Login Success : " + data);
                    Dictionary<string, object> resultDict = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
                    if (resultDict.ContainsKey("access_token"))
                    {
                        methodForResult(resultDict["access_token"] as string);
                    }
                    else
                    {
                        methodForError("Could not get TapTap access token.");
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message);
                    }
                }

            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });

        }

        public void LoginXD(string appid, string username, string password, string captcha, string code,
                            Action<string> methodForResult, Action<string, object> methodForError)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(username + ":" + password);
            string encodedText = "Basic " + Convert.ToBase64String(bytesToEncode);
            Dictionary<string, string> headers = new Dictionary<string, string> {
                {"Authorization", encodedText},
            };
            List<string> scopes = new List<string>
            {
                "user",
                "sdk"
            };
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"client_id", appid},
                {"open_twoauth", true},
                { "scopes", scopes},
            };
            if (!string.IsNullOrEmpty(code))
            {
                parameters.Add("code", code);
            }
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }

            net.PostOnServer(XD_AUTH, headers, parameters, (string data) =>
            {
                try
                {
                    Dictionary<string, object> result = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;
                    if (result.ContainsKey("error")
                        && result.ContainsKey("needTwoauth")
                        && (Boolean)result["needTwoauth"]
                        && result.ContainsKey("user_id"))
                    {
                        String error = result["error"] as string;
                        if (!error.Equals("短信验证码错误"))
                        {
                            methodForError("need_verification_code", result);
                        }
                        else
                        {
                            methodForError("error", result);
                        }
                    }
                    else if (result.ContainsKey("error"))
                    {
                        methodForError("error", result["error"]);
                    }
                    else if (result.ContainsKey("access_token"))
                    {
                        methodForResult(result["access_token"] as string);
                    }
                    else
                    {
                        methodForError("error", data);
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError("error", e.Message + ":" + data);
                    }
                }
            }, (int errorCode, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError("error", error);
                }
            });
        }

        public void FetchVerificationCode(string appid,
            string userID,
            string token,
            string areaCode,
            string mobile,
            string type,
            Action<string> methodForResult, Action<string> methodForError)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"client_id", appid},
                {"user_id", userID},
                {"access_token", token},
                {"area_code", areaCode},
                {"mobile", mobile},
                {"type", type},
            };
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.PostOnServer(VERIFY_CODE, parameters, (string result) =>
            {
                if (methodForResult != null)
                {
                    methodForResult(result);
                }
            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    try
                    {
                        Dictionary<string, object> errorDict = MiniJSON.Json.Deserialize(error) as Dictionary<string, object>;
                        if (errorDict.ContainsKey("error_description"))
                        {
                            methodForError(errorDict["error_description"] as string);
                        }
                        else
                        {
                            methodForError(error);
                        }
                    }
                    catch (Exception e)
                    {
                        methodForError(error);
                    }
                }
            });
        }

        public void GetUser(string token, Action<User> methodForResult, Action<string> methodForError)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"access_token", token},
            };
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.GetOnServer(USER, parameters, (string result) =>
            {
                try
                {
                    Dictionary<string, object> userDict = MiniJSON.Json.Deserialize(result) as Dictionary<string, object>;
                    User user = User.InitWithDict(userDict);
                    if (methodForResult != null)
                    {
                        methodForResult(user);
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message + " : " + result);
                    }
                }
            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void GetRealnameInfo(string token, Action<Dictionary<string, object>> methodForResult, Action<string> methodForError)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"access_token", token},
            };
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.GetOnServer(REALNAME_INFO, parameters, (string result) =>
            {
                try
                {
                    Dictionary<string, object> realnameInfoDict = MiniJSON.Json.Deserialize(result) as Dictionary<string, object>;
                    
                    if (methodForResult != null)
                    {
                        if(realnameInfoDict.ContainsKey("name") && realnameInfoDict.ContainsKey("name"))
                        {
                            methodForResult(realnameInfoDict);
                        } else
                        {
                            if (methodForError != null)
                            {
                                methodForError("Unknown response" + " : " + result);
                            }
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message + " : " + result);
                    }
                }
            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void VerifyRealName(string token, string name, string id, Action<string> methodForResult, Action<string> methodForError)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"access_token", token},
                {"is_tmp", "0"},
                {"realname", name},
                {"realid", id},
            };
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.PostOnServer(REALNAME, parameters, (string result) =>
            {
                if (methodForResult != null)
                {
                    methodForResult(result);
                }
            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void Logout(Action method)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.GetOnServer(LOGOUT, (string result) =>
            {
                if (method != null)
                {
                    method();
                }
            }, (int code, string error) =>
            {
                if (method != null)
                {
                    method();
                }
            });
        }

        public void CheckWeChatToken(string appid, string refreshToken, string authType, Action success, Action<int> failed)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"client_id", appid},
                {"refresh_token", refreshToken},
                {"auth_type", authType},
            };
            net.PostOnServer(WX_CHECK, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }
            }, (int code, string error) =>
            {
                if (failed != null)
                {
                    failed(code);
                }
            });
        }

        public void CheckQQToken(string appid, string openid, string token, string authType, Action success, Action<int> failed)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"client_id", appid},
                {"qq_openid", openid},
                {"qq_access_token", token},
                {"auth_type", authType},
            };
            net.PostOnServer(QQ_CHECK, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }
            }, (int code, string error) =>
            {
                if (failed != null)
                {
                    failed(code);
                }
            });
        }

        public void CheckTapTapToken(string appid, string macKey, string kid, string tokenType,
                                             string accessToken, string macAlgorithm, string authType,
                                             Action success, Action<int> failed)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"client_id", appid},
                {"mac_key", macKey},
                {"kid", kid},
                {"token_type", authType},
                {"access_token",accessToken},
                {"mac_algorithm",macAlgorithm},
            };
            net.PostOnServer(TAPTAP_CHECK, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }

            }, (int code, string error) =>
            {
                if (failed != null)
                {
                    failed(code);
                }
            });
        }

        public void SetPlayLog(string accessToken, long[][] localOnlineTimes, long[][] serverOnlineTimes,
            Action success, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> playLogs = new Dictionary<string, object>
            {
                {"local_online_time", localOnlineTimes},
                {"server_online_time", serverOnlineTimes}
            };
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"access_token", accessToken},
                {"play_logs", playLogs},
                {"is_tmp", 0}
            };
            net.PostOnServer(FCM_PLAY_LOG, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }

            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void GetServerTime(Action<long> methodForResult, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            net.GetOnServer(FCM_SERVER_TIME, null, (string result) =>
            {
                try
                {
                    Dictionary<string, object> timestampDict = MiniJSON.Json.Deserialize(result) as Dictionary<string, object>;
                    long timestamp = (long)timestampDict["timestamp"];
                    if (methodForResult != null)
                    {
                        methodForResult(timestamp);
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message + " : " + result);
                    }
                }

            }, (int code, string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public void BindMobile(string token,
          string areaCode,
          string mobile,
          string code,
       Action success, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"area_code", areaCode},
                {"mobile", mobile},
                {"code", code},
                {"access_token", token}
            };
            net.PostOnServer(BIND_MOBILE, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }

            }, (int errCode, string error) =>
            {
                if (methodForError != null)
                {
                    try
                    {
                        Dictionary<string, object> errorDict = MiniJSON.Json.Deserialize(error) as Dictionary<string, object>;
                        if (errorDict.ContainsKey("error_description"))
                        {
                            methodForError(errorDict["error_description"] as string);
                        }
                        else
                        {
                            methodForError(error);
                        }
                    }
                    catch (Exception e)
                    {
                        methodForError(error);
                    }
                }
            });
        }

        public void BindMobileAndRealname(string token,
            string realname,
            string identityCode,
            string areaCode,
            string mobile,
            string code,
         Action success, Action<string> methodForError)
        {
            Net net = netObject.GetComponent<Net>();
            if (net == null)
            {
                net = netObject.AddComponent<Net>();
            }
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"realname", realname},
                {"realid", identityCode},
                {"area_code", areaCode},
                {"mobile", mobile},
                {"code", code},
                {"access_token", token}
            };
            net.PostOnServer(BIND_MOBILE_AND_REALNAME, parameters, (string result) =>
            {
                if (success != null)
                {
                    success();
                }

            }, (int errCode, string error) =>
            {
                if (methodForError != null)
                {
                    try
                    {
                        Dictionary<string, object> errorDict = MiniJSON.Json.Deserialize(error) as Dictionary<string, object>;
                        if (errorDict.ContainsKey("error_description"))
                        {
                            methodForError(errorDict["error_description"] as string);
                        } else
                        {
                            methodForError(error);
                        }
                    } catch (Exception e)
                    {
                        methodForError(error);
                    }
                }
            });
        }

    }
}
