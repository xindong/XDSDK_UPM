﻿using UnityEngine;
using System.Collections;

namespace com.xdsdk.xdlive{
    public class XDLiveListener : MonoBehaviour
    {
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
    }
}