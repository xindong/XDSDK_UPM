#define USE_UNITY_XDSDK

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace xdsdk
{
    public class XDSDKUtil
    {

        public static string DictionaryGetStringValue(string key, Dictionary<string, string> dic)
        {
            string result = "";
            dic.TryGetValue(key, out result);
            return result;
        }

    }
}