using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace xdsdk.Unity.Service
{
    public static class Token
    {
        private readonly static string TOKEN_KEY = "com.xindong.xdsdk.unity.token";
        private readonly static string THIRD_PARTY_TOKEN_KEY = "com.xindong.xdsdk.unity.3rd.token";
        private readonly static string THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP = "com.xindong.xdsdk.unity.3rd.token.failed.timestamp";

        private readonly static double THIRD_PARTY_TOKEN_VERIFY_TIME = 30 * 60 * 1000;

        public enum ThirdPartyTokenType
        {
            Wechat,
            QQ,
            TapTap,
        }

        public static string GetToken(string appid)
        {
            return DataStorage.LoadString(TOKEN_KEY + "." + appid);
        }

        public static void SetToken(string appid, string token)
        {
            DataStorage.SaveString(TOKEN_KEY + "." + appid, token);
        }

        public static void ClearToken(string appid)
        {
            ClearThirdPartyToken(appid);
            DataStorage.SaveString(TOKEN_KEY + "." + appid, "");
        }

        public static void SetThirdPartyToken(string appid, ThirdPartyTokenType type, Dictionary<string, string> tokenDict)
        {
            try
            {
                ClearThirdPartyToken(appid);
                if (type == ThirdPartyTokenType.Wechat)
                {
                    tokenDict.Add("type", "wechat");
                }
                else if (type == ThirdPartyTokenType.QQ)
                {
                    tokenDict.Add("type", "qq");
                }
                else if (type == ThirdPartyTokenType.TapTap)
                {
                    tokenDict.Add("type", "taptap");
                }
                DataStorage.SaveString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid, "0");
                string s = appid + "?" + Net.DictToQueryString(tokenDict);
                DataStorage.SaveString(THIRD_PARTY_TOKEN_KEY + "." + appid, s);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static bool HasThirdPartyToken(string appid)
        {
            return !string.IsNullOrEmpty(DataStorage.LoadString(THIRD_PARTY_TOKEN_KEY + "." + appid));
        }

        private static void ClearThirdPartyToken(string appid)
        {
            DataStorage.SaveString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP, "0");
            DataStorage.SaveString(THIRD_PARTY_TOKEN_KEY + "." + appid, "");
        }


        public static void CheckThirdPartyToken(string appid, Action callback)
        {
            try
            {
                if (HasThirdPartyToken(appid))
                {
                    String s = DataStorage.LoadString(THIRD_PARTY_TOKEN_KEY + "." + appid);
                    NameValueCollection nvc = new NameValueCollection();
                    string id = "";
                    Net.ParseUrl(s, out id, out nvc);
                    string type = nvc["type"];
                    if (!string.IsNullOrEmpty(type) && type.Equals("wechat"))
                    {
                        Api.Instance.CheckWeChatToken(appid, nvc["refresh_token"], "web", () =>
                        {
                            if (callback != null)
                            {
                                callback();
                            }
                        }, (int code) =>
                        {
                            if (code == 403 || code == 500)
                            {
                                double lastCheckFailedTimestamp = Double.Parse(DataStorage.LoadString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid));
                                double currentTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                                if (Math.Abs(lastCheckFailedTimestamp - 0) < 1) {
                                    DataStorage.SaveString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid, currentTimestamp.ToString());
                                    if(callback != null){
                                        callback();
                                    }
                                } else if (Math.Abs(lastCheckFailedTimestamp - currentTimestamp) < THIRD_PARTY_TOKEN_VERIFY_TIME){
                                    if (callback != null)
                                    {
                                        callback();
                                    }
                                } else {
                                    ClearToken(appid);
                                    ClearThirdPartyToken(appid);
                                    if (callback != null)
                                    {
                                        callback();
                                    }
                                }

                            }
                            else if (code == 400)
                            {
                                ClearToken(appid);
                                ClearThirdPartyToken(appid);
                                if (callback != null)
                                {
                                    callback();
                                }
                            }
                            else
                            {
                                if (callback != null)
                                {
                                    callback();
                                }
                            }
                        });
                    }
                    else if (!string.IsNullOrEmpty(type) && type.Equals("qq"))
                    {
                        Api.Instance.CheckQQToken(appid, nvc["open_id"], nvc["token"], "web", () =>
                         {
                             if (callback != null)
                             {
                                 callback();
                             }
                         }, (int code) =>
                         {
                             if (code == 403 || code == 500)
                             {
                                 double lastCheckFailedTimestamp = Double.Parse(DataStorage.LoadString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid));
                                 double currentTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                                 if (Math.Abs(lastCheckFailedTimestamp - 0) < 1)
                                 {
                                     DataStorage.SaveString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid, currentTimestamp.ToString());
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                                 else if (Math.Abs(lastCheckFailedTimestamp - currentTimestamp) < THIRD_PARTY_TOKEN_VERIFY_TIME)
                                 {
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                                 else
                                 {
                                     ClearToken(appid);
                                     ClearThirdPartyToken(appid);
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                             }
                             else if (code == 400)
                             {
                                 ClearToken(appid);
                                 ClearThirdPartyToken(appid);
                                 if (callback != null)
                                 {
                                     callback();
                                 }
                             }
                             else
                             {
                                 if (callback != null)
                                 {
                                     callback();
                                 }
                             }
                         });
                    }
                    else if (!string.IsNullOrEmpty(type) && type.Equals("taptap"))
                    {
                        Api.Instance.CheckTapTapToken(appid, nvc["mac_key"],
                                                      nvc["kid"], nvc["token_type"],
                                                      nvc["access_token"], nvc["mac_algorithm"], "web", () =>
                         {
                             if (callback != null)
                             {
                                 callback();
                             }
                         }, (int code) =>
                         {
                             if (code == 403 || code == 500)
                             {
                                 double lastCheckFailedTimestamp = Double.Parse(DataStorage.LoadString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid));
                                 double currentTimestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
                                 if (Math.Abs(lastCheckFailedTimestamp - 0) < 1)
                                 {
                                     DataStorage.SaveString(THIRD_PARTY_TOKEN_CHECK_FAILED_TIMESTAMP + "." + appid, currentTimestamp.ToString());
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                                 else if (Math.Abs(lastCheckFailedTimestamp - currentTimestamp) < THIRD_PARTY_TOKEN_VERIFY_TIME)
                                 {
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                                 else
                                 {
                                     ClearToken(appid);
                                     ClearThirdPartyToken(appid);
                                     if (callback != null)
                                     {
                                         callback();
                                     }
                                 }
                             }
                             else if (code == 400)
                             {
                                 ClearToken(appid);
                                 ClearThirdPartyToken(appid);
                                 if (callback != null){
                                     callback();
                                }
                             }
                             else
                             {
                                 if (callback != null)
                                 {
                                     callback();
                                 }
                             }
                         });
                    }
                    else
                    {
                        ClearThirdPartyToken(appid);
                        if (callback != null){
                            callback();
                        }
                    }

                } else {
                    if(callback != null){
                        callback();
                    }
                }
            }
            catch (Exception e)
            {
                ClearThirdPartyToken(appid);
                ClearToken(appid);
                if (callback != null){
                    callback();
                }
            }
        }







    }
}
