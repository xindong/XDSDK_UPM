using UnityEngine;
using System.Collections;


namespace xdsdk.Unity
{
    public class UIBase : MonoBehaviour
    {
        public virtual void OnEnter()
        {
            Debug.Log(name + " : " + "OnEnter");
        }

        public virtual void OnPause()
        {
            Debug.Log(name + " : " + "OnPause");
            gameObject.SetActive(false);
        }

        public virtual void OnResume()
        {
            Debug.Log(name + " : " + "OnResume");
            gameObject.SetActive(true);
        }

        public virtual void OnExit()
        {
            Debug.Log(name + " : " + "OnExit");
            Destroy(gameObject);
        }
    }
}

