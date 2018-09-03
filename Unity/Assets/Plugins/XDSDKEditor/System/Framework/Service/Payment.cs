using System;
using System.Collections.Generic;
using UnityEngine;

namespace xdsdk.Unity.Service
{
    public class Payment
    {
        public Payment()
        {
        }

        public static void URL(string appid, string token, Dictionary<string, string> info, Action<string> methodForResult, Action<string> methodForError)
        {
            if (info != null &&
               info.ContainsKey("Product_Name") &&
               info.ContainsKey("Product_Id") &&
               info.ContainsKey("Product_Price") &&
               info.ContainsKey("Sid") &&
               info.ContainsKey("Role_Id") &&
               info.ContainsKey("OrderId") &&
               info.ContainsKey("EXT"))
            {
                try
                {
                    string name = WWW.EscapeURL(info["Product_Name"]);
                    string productID = info["Product_Id"];
                    string roleID = info["Role_Id"];
                    string amount = Convert.ToString(Int32.Parse(info["Product_Price"]) / 100f);
                    Dictionary<string, string> finalInfo = new Dictionary<string, string>()
                {
                    {"client_id", appid},
                    {"access_token", token},
                    {"product_name", name},
                    {"product_id", productID},
                    {"role_id", roleID},
                    {"amount", amount}
                };
                    if(info.ContainsKey("EXT")){
                        finalInfo.Add("EXT", WWW.EscapeURL(info["EXT"]));
                    }
                    if (methodForResult != null)
                    {
                        methodForResult("http://www.xd.com/orders/pcsdk_create_order" + "?" +
                                                      Net.DictToQueryString(finalInfo));
                    }
                }
                catch (Exception e)
                {
                    if (methodForError != null)
                    {
                        methodForError(e.Message);
                    }
                }
            }
            else
            {
                if (methodForError != null)
                {
                    methodForError("Wrong paramaters.");
                }
            }

        }


    }
}
