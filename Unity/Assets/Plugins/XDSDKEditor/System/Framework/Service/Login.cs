using System;
using System.Collections.Generic;

namespace xdsdk.Unity.Service
{
    public static class Login
    {
        public static void QQ(string appid, string openID, string accessToken, Action<Dictionary<string, object>> methodForResult, Action<string> methodForError)
        {
            Api.Instance.LoginQQ(appid, openID, accessToken, (string token) =>
            {
                User(token, (User user) =>
                {
                    Dictionary<string, object> resultDict = new Dictionary<string, object>
                    {
                        {"user", user},
                        {"token", token},
                    };
                    Dictionary<string, string> saveToken = new Dictionary<string, string>{
                        {"open_id", openID},
                        {"token", accessToken},
                    };
                    Token.SetThirdPartyToken(appid, Token.ThirdPartyTokenType.QQ, saveToken);
                    if (methodForResult != null)
                    {
                        methodForResult(resultDict);
                    }
                }, (string error) =>
                {
                    if (methodForError != null)
                    {
                        methodForError(error);
                    }
                });
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public static void Wechat(string appid, string code, Action<Dictionary<string, object>> methodForResult, Action<string> methodForError)
        {
            Api.Instance.LoginWechat(appid, code, (string token, string refreshToken) =>
            {
                User(token, (User user) =>
                {
                    Dictionary<string, object> resultDict = new Dictionary<string, object>
                    {
                        {"user", user},
                        {"token", token},
                    };
                    Dictionary<string, string> saveToken = new Dictionary<string, string>{
                        {"refresh_token", refreshToken},
                    };
                    Token.SetThirdPartyToken(appid, Token.ThirdPartyTokenType.Wechat, saveToken);
                    if (methodForResult != null)
                    {
                        methodForResult(resultDict);
                    }
                }, (string error) =>
                {
                    if (methodForError != null)
                    {
                        methodForError(error);
                    }
                });
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public static void TapTap(string appid, string kid, string accessToken, string tokenType, string macKey, string macAlgorithm,
                                  Action<Dictionary<string, object>> methodForResult, Action<string> methodForError)
        {

            Api.Instance.LoginTapTap(appid, kid, accessToken, tokenType, macKey, macAlgorithm, (string token) =>
            {
                User(token, (User user) =>
                {
                    Dictionary<string, object> resultDict = new Dictionary<string, object>
                    {
                        {"user", user},
                        {"token", token},
                    };
                    Dictionary<string, string> saveToken = new Dictionary<string, string>{
                        {"mac_key", macKey},
                        {"kid", kid},
                        {"token_type", tokenType},
                        {"access_token", accessToken},
                        {"mac_algorithm",  macAlgorithm},
                    };
                    Token.SetThirdPartyToken(appid, Token.ThirdPartyTokenType.TapTap, saveToken);
                    if (methodForResult != null)
                    {
                        methodForResult(resultDict);
                    }
                }, (string error) =>
                {
                    if (methodForError != null)
                    {
                        methodForError(error);
                    }
                });
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public static void XD(string appid, string username, string password, string code, Action<Dictionary<string, object>> methodForResult, Action<string, object> methodForError)
        {
            Api.Instance.LoginXD(appid, username, password, null, code, (string token) =>
            {
                User(token, (User user) =>
                {
                    Dictionary<string, object> resultDict = new Dictionary<string, object>
                    {
                        {"user", user},
                        {"token", token},
                    };
                    if (methodForResult != null)
                    {
                        methodForResult(resultDict);
                    }
                }, (string error) =>
                {
                    if (methodForError != null)
                    {
                        methodForError("error", error);
                    }
                });
            }, (string errorType, object errorContent) =>
            {
                if (methodForError != null)
                {
                    methodForError(errorType, errorContent);
                }
            });
        }

        public static void XD(string appid, string username, string password, Action<Dictionary<string, object>> methodForResult, Action<string, object> methodForError)
        {
            Api.Instance.LoginXD(appid, username, password, null, null, (string token) =>
            {
                User(token, (User user) =>
                {
                    Dictionary<string, object> resultDict = new Dictionary<string, object>
                    {
                        {"user", user},
                        {"token", token},
                    };
                    if (methodForResult != null)
                    {
                        methodForResult(resultDict);
                    }
                }, (string error) =>
                {
                    if (methodForError != null)
                    {
                        methodForError("error", error);
                    }
                });
            }, (string errorType, object errorContent) =>
            {
                if (methodForError != null)
                {
                    methodForError(errorType, errorContent);
                }
            });
        }

        public static void VerificationCode(string appid, string userID, string token, string areaCode, string mobile, string type, Action<string> methodForResult, Action<string> methodForError)
        {
            Api.Instance.FetchVerificationCode(appid, userID, token, areaCode, mobile, type, (string result) =>
            {
                if (methodForResult != null)
                {
                    methodForResult(result);
                }
            }, (string errorType) =>
            {
                if (methodForError != null)
                {
                    methodForError(errorType);
                }
            });
        }

        public static void User(string accessToken, Action<User> methodForResult, Action<string> methodForError)
        {
            Api.Instance.GetUser(accessToken, (User result) =>
            {
                Api.Instance.GetRealnameInfo(accessToken,
                    (Dictionary<string, object> realnameResult) =>
                    {
                        if (methodForResult != null)
                        {
                            result.Realname = realnameResult["name"] as string;
                            result.IdentifyNumber = realnameResult["id"] as string;
                            methodForResult(result);
                        }
                    },
                    (string error) =>
                    {
                        if (methodForResult != null)
                        {
                            methodForResult(result);
                        }
                    });
               
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public static void RealName(string accessToken, string name, string id, Action success, Action<string> methodForError)
        {
            Api.Instance.VerifyRealName(accessToken, name, id, (string result) =>
             {
                 if (success != null)
                 {
                     success();
                 }
             }, (string error) =>
             {
                 if (methodForError != null)
                 {
                     methodForError(error);
                 }
             });
        }

        public static void MobileAndRealName(string token,
            string realname,
            string identityCode,
            string areaCode,
            string mobile,
            string code,
            Action success,
            Action<string> methodForError)
        {
            Api.Instance.BindMobileAndRealname(token, realname, identityCode, areaCode, mobile, code, () =>
            {
                if (success != null)
                {
                    success();
                }
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }


        public static void Mobile(string token,
            string areaCode,
            string mobile,
            string code,
            Action success,
            Action<string> methodForError)
        {
            Api.Instance.BindMobile(token, areaCode, mobile, code, () =>
            {
                if (success != null)
                {
                    success();
                }
            }, (string error) =>
            {
                if (methodForError != null)
                {
                    methodForError(error);
                }
            });
        }

        public static void Logout(Action method)
        {
            Api.Instance.Logout(method);
        }



        private static string Md5Sum(string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }
    }
}
