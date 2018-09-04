using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NGUIDemo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(){
        xdsdk.XDSDK.SetCallback(new XDSDKHandler());
        string[] entries = { "WX_LOGIN", "TAPTAP_LOGIN", "XD_LOGIN" };
        xdsdk.XDSDK.SetLoginEntries(entries);
        xdsdk.XDSDK.InitSDK("a4d6xky5gt4c80s", 0, "UnityXDSDK", "0.0.0", true);
    }

    public void Login(){
        xdsdk.XDSDK.Login();
    }

    public void Logout(){
        xdsdk.XDSDK.Logout();
    }
}
