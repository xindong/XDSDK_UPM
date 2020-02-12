using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZenFulcrum.EmbeddedBrowser;
namespace xdsdk.Unity
{
    public class WebWindow : UIElement
    {

        public Button close;

        public Button back;

        public Browser browser;

        private string url = "";

        private string lastUrl = "";

        private string containsUrl = "";

        public override Dictionary<string, object> Extra
        {
            get
            {
                return extra;
            }

            set
            {
                extra = value;
                if (extra != null)
                {
                    if (extra.ContainsKey("url"))
                    {
                        url = extra["url"] as string;
                        browser.Url = url;
                    }
                    if (extra.ContainsKey("contains_url"))
                    {
                        containsUrl = extra["contains_url"] as string;
                    }
                }
            }
        }


        private void Awake()
        {
            browser.onNavStateChange += () =>
            {
                Debug.Log("onNavStateChange : " + browser.Url);
                HandleUrl(browser.Url);
            };

            browser.onFetchError += (error) =>
            {
                String json = error.AsJSON;
                Dictionary<string, object> dict =  MiniJSON.Json.Deserialize(json) as Dictionary<string,object>;
                if(dict.ContainsKey("url")){
                    Debug.Log("onFetchError : " + dict["url"]);
                    HandleUrl(dict["url"] as string);
                }
            };

            close.onClick.AddListener(OnCloseClicked);

            back.onClick.AddListener(()=>{
                string a = browser.Url;
                if(browser.CanGoBack){
                    browser.GoBack();
                } else {
                    OnCloseClicked();
                }
            });

            transitionDurationTime = 0.2f;

        }

        private void HandleUrl(string urlStr){
            if (containsUrl != null && urlStr != null && (lastUrl == null || !lastUrl.Equals(urlStr)))
            {
                if (urlStr.Contains(containsUrl))
                {
                    OnCallback(SDKManager.RESULT_SUCCESS, urlStr);
                    GetSDKManager().Pop(name);
                }
            }
            lastUrl = browser.Url;
        }

        private void OnCloseClicked()
        {

            OnCallback(SDKManager.RESULT_BACK, "Close button clicked");
            GetSDKManager().Pop(name);
        }

        public override IEnumerator PlayExit()
        {
            if (!animationLaunched)
            {
                animationLaunched = true;
                float startTime = Time.time;
                float endTime = startTime + transitionDurationTime;
                CanvasGroup canvasGroup = UI.GetComponent<CanvasGroup>(gameObject);
                canvasGroup.alpha = 1f;
                while (Time.time < endTime)
                {
                    yield return new WaitForEndOfFrame();
                    float delta = (Time.time - startTime) / transitionDurationTime;
                    canvasGroup.alpha = 1 - delta;
                }
            }
            animationLaunched = false;
            yield return null;
        }

        public override IEnumerator PlayEnter()
        {
            if (!animationLaunched)
            {
                animationLaunched = true;
                float startTime = Time.time;
                float endTime = startTime + transitionDurationTime;
                CanvasGroup canvasGroup = UI.GetComponent<CanvasGroup>(gameObject);
                canvasGroup.alpha = 0f;
                while (Time.time < endTime)
                {
                    yield return new WaitForEndOfFrame();
                    float delta = (Time.time - startTime) / transitionDurationTime;
                    canvasGroup.alpha = delta;
                }
            }
            animationLaunched = false;
            yield return null;
        }
    }
}
