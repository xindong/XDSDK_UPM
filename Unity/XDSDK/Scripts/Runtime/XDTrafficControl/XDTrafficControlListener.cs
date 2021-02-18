using UnityEngine;
using System.Collections;

namespace com.xdsdk.xdtrafficcontrol{
    public class XDTrafficControlListener : MonoBehaviour
    {
        public static volatile bool inited = false;

        public static void Init()
        {
            if (!inited)
            {
                inited = true;
                GameObject gameObject = new GameObject();
                gameObject.name = "XDTrafficControlListener";
                gameObject.AddComponent<XDTrafficControlListener>();
                GameObject.DontDestroyOnLoad(gameObject);
            }
        }

        void XDTrafficControlFinished()
        {
            if (XDTrafficControl.Instance.GetCallback() != null)
            {
                XDTrafficControl.Instance.GetCallback().OnQueueingFinished();
            }

        }

        void XDTrafficControlFailed(string msg)
        {
            if (XDTrafficControl.Instance.GetCallback() != null)
            {
                XDTrafficControl.Instance.GetCallback().OnQueueingFailed(msg);
            }
        }

        void XDTrafficControlCanceled()
        {
            if (XDTrafficControl.Instance.GetCallback() != null)
            {
                XDTrafficControl.Instance.GetCallback().OnQueueingCanceled();
            }
        }
    }
}
