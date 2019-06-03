using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace com.xdsdk.xdlive{
    public class XDLiveListener : MonoBehaviour
    {
		private Dictionary<string, Action<Dictionary<string, object>>> callbackDict
			= new Dictionary<string, Action<Dictionary<string, object>>>();

        public static volatile bool inited = false;

        public static void Init()
        {
            if (!inited)
            {
                inited = true;
                GameObject gameObject = new GameObject();
                gameObject.name = "XDLiveListener";
                gameObject.AddComponent<XDLiveListener>();
                GameObject.DontDestroyOnLoad(gameObject);
            }
        }

        void OnXDLiveClosed()
        {
            if (XDLive.Instance.GetCallback() != null)
            {
                XDLive.Instance.GetCallback().OnXDLiveClosed();
            }

        }

        void OnXDLiveOpen()
        {
            if (XDLive.Instance.GetCallback() != null)
            {
                XDLive.Instance.GetCallback().OnXDLiveOpen();
            }
        }

		void InvokeFuncCallback(string result) 
		{
			if (result != null) 
			{
				Dictionary<string,object> resultDict = MiniJSON.Json.Deserialize(result) as Dictionary<string,object>;
				if (resultDict.ContainsKey ("unity_callback_id")) 
				{
					string unityCallbackID = resultDict ["unity_callback_id"] as string;
					if (callbackDict.ContainsKey (unityCallbackID)) 
					{
						Action<Dictionary<string, object>> callback = callbackDict[unityCallbackID] as Action<Dictionary<string, object>>;
						callback (resultDict);
						callbackDict.Remove (unityCallbackID);
					}
				}
			}
			
		}

		public void AddCallback(string unityCallbackID, Action<Dictionary<string, object>> callback)
		{
			callbackDict.Add (unityCallbackID, callback);
		}

		public void RemoveAllCallback(string unityCallbackID, Action<Dictionary<string, object>> callback)
		{
			callbackDict.Clear ();
		}
    }
}
