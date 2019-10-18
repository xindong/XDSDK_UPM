using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace xdsdk.Unity
{
    public class ContainerWindow : UIElement
    {

        public Image backgroung;

        void Awake()
        {
            transitionDurationTime = 0.1f;

            CanvasScaler scaler = GetComponent<CanvasScaler>();
            if(Screen.height > 1080){
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            } else {
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
            }

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
