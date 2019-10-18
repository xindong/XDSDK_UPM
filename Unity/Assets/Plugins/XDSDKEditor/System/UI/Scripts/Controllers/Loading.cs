using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class Loading : UIElement
    {
        public Loading()
        {
        }

        void Awake()
        {
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
