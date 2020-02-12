using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class Dialog : UIElement
    {
        private string titleText;
        private string contentText;
        private string positiveText;
        private string negativeText;

        public Text title;
        public Text content;
        public Button positive;
        public Button negative;
        public Button close;
        

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
                    if (extra.ContainsKey("title"))
                    {
                        titleText = extra["title"] as string;
                        title.text = titleText;
                    }
                    if (extra.ContainsKey("content"))
                    {
                        contentText = extra["content"] as string;
                        content.text = contentText;
                    }
                    if (extra.ContainsKey("positive"))
                    {
                        positiveText = extra["positive"] as string;
                        Text text = positive.GetComponentInChildren<Text>();
                        text.text = positiveText;
                    }
                    if (extra.ContainsKey("negative"))
                    {
                        negativeText = extra["negative"] as string;
                        Text text = negative.GetComponentInChildren<Text>();
                        text.text = negativeText;
                    }
                }
            }
        }

        void Awake()
        {
            positive.onClick.AddListener(() =>
            {
                OnCallback(SDKManager.RESULT_SUCCESS, "positive");
            });
            negative.onClick.AddListener(() =>
            {
                OnCallback(SDKManager.RESULT_FAILED, "negative");
            });
            close.onClick.AddListener(() =>
            {
                OnCallback(SDKManager.RESULT_CLOSE, "close");
            });

            transitionDurationTime = 0.2f;
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


