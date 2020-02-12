using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace xdsdk.Unity
{
    [DisallowMultipleComponent]
    public class SDKManager : MonoBehaviour
    {

        public static readonly int RESULT_FAILED = -1;
        public static readonly int RESULT_SUCCESS = 0;
        public static readonly int RESULT_BACK = 1;
        public static readonly int RESULT_CLOSE = 2;


        private GameObject containerObj;
        private GameObject dialogObj;
        private GameObject dialogContentObj;
        private GameObject loadingObj;
        private GameObject loadingContentObj;
        private readonly List<UIElement> uiElements = new List<UIElement>();

        void Awake()
        {
            ZenFulcrum.EmbeddedBrowser.Browser browser = gameObject.AddComponent<ZenFulcrum.EmbeddedBrowser.Browser>();
            Destroy(browser);
        }


        public void Pop()
        {
            PopUIElement(null);
        }

        public void Pop(string targetName){
            PopUIElement(targetName);
        }

        public void PopAll()
        {
            if(containerObj != null){
                UIElement element = null;
                if(uiElements.Count > 0){
                    element = uiElements[uiElements.Count - 1];
                }
                UIAnimator animator = UI.GetComponent<UIAnimator>(containerObj);
                animator.DoExitAnimation(element, null, () =>
                {
                    for (int i = uiElements.Count - 1; i >= 0; i--)
                    {
                        uiElements[i].OnExit();
                    }
                    uiElements.Clear();
                    DestoryContainer();
                });
            }
        }

        public void ShowLogin<T>(Dictionary<string, object> configs, Action<int, object> callback) where T : UIElement
        {
            PushUIElement<T>("XDMainLoginWindow", configs, callback);
        }

        public void ShowPlatformLogin<T>(Dictionary<string, object> configs, Action<int, object> callback) where T : UIElement{
            PushUIElement<T>("XDPlatformLoginWindow", configs, callback);
        }

        public void ShowWebView<T>(Dictionary<string, object> configs, Action<int, object> callback) where T : UIElement{
            PushUIElement<T>("XDWebWindow", configs, callback);
        }

        public void ShowTwoFactorAuth<T>(Dictionary<string, object> configs, Action<int, object> callback) where T : UIElement {
            PushUIElement<T>("XDTwoFactorAuthWindow", configs, callback);
        }

        public void ShowRealName<T>(Dictionary<string, object> configs, Action<int, object> callback) where T : UIElement {
            PushUIElement<T>("XDRealNameWindow", configs, callback);
        }

        public void ShowDialog(Dictionary<string, object> extra,
                                      Action<int, object> callback)
        {
            if(dialogObj == null){
                dialogObj = Instantiate(Resources.Load("Prefabs/XDSDK")) as GameObject;
                dialogObj.name = "XDSDKDialog";
                UnityEngine.Object.DontDestroyOnLoad(dialogObj);
                Canvas sprite = dialogObj.GetComponent<Canvas>();
                sprite.sortingOrder = 9999;
                UIElement containerElement = UI.GetComponent<ContainerWindow>(dialogObj);
                UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(dialogObj);
                containerElement.OnEnter();
                containerAnimator.DoEnterAnimation(null, containerElement, () =>
                {

                });
            }
            if(dialogContentObj == null){
                dialogContentObj = Instantiate(Resources.Load("Prefabs/XDDialog")) as GameObject;
                dialogContentObj.name = "DialogContent";
                UnityEngine.Object.DontDestroyOnLoad(dialogContentObj);
                dialogContentObj.transform.SetParent(dialogObj.transform, false);
            }
            UIElement element = UI.GetComponent<Dialog>(dialogContentObj);
            UIAnimator animator = UI.GetComponent<UIAnimator>(dialogContentObj);
            element.ClearCallback();
            element.Callback += callback;
            element.Extra = extra;
            element.OnEnter();
            animator.DoEnterAnimation(null, element, () =>
            {

            });

        }

        public void DismissDialog(){
            if(dialogContentObj != null){
                UIElement element = UI.GetComponent<Dialog>(dialogContentObj);
                UIAnimator animator = UI.GetComponent<UIAnimator>(dialogContentObj);
                animator.DoExitAnimation(element, null, () =>
                {
                    element.OnExit();
                    dialogContentObj = null;
                    UIElement containerElement = UI.GetComponent<ContainerWindow>(dialogObj);
                    UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(dialogObj);
                    containerElement.OnEnter();
                    containerAnimator.DoExitAnimation(containerElement, null, () =>
                    {
                        Destroy(dialogObj);
                        dialogObj = null;
                    });
                });
            } else if (dialogObj != null){
                UIElement containerElement = UI.GetComponent<ContainerWindow>(dialogObj);
                UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(dialogObj);
                containerElement.OnEnter();
                containerAnimator.DoExitAnimation(containerElement, null, () =>
                {
                    Destroy(dialogObj);
                    dialogObj = null;
                });
            }
        }


        public void ShowLoading(){
            if (loadingObj == null)
            {
                loadingObj = Instantiate(Resources.Load("Prefabs/XDSDK")) as GameObject;
                loadingObj.name = "XDSDKLoading";
                UnityEngine.Object.DontDestroyOnLoad(loadingObj);
                Canvas sprite = loadingObj.GetComponent<Canvas>();
                sprite.sortingOrder = 10000;
                UIElement containerElement = UI.GetComponent<ContainerWindow>(loadingObj);
                UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(loadingObj);
                containerElement.OnEnter();
                containerAnimator.DoEnterAnimation(null, containerElement, () =>
                {

                });
            }
            if (loadingContentObj == null)
            {
                loadingContentObj = Instantiate(Resources.Load("Prefabs/XDLoading")) as GameObject;
                loadingContentObj.name = "LoadingContent";
                UnityEngine.Object.DontDestroyOnLoad(loadingContentObj);
                loadingContentObj.transform.SetParent(loadingObj.transform, false);
            }
            UIElement element = UI.GetComponent<Loading>(loadingContentObj);
            UIAnimator animator = UI.GetComponent<UIAnimator>(loadingContentObj);
            element.OnEnter();
            animator.DoEnterAnimation(null, element, () =>
            {

            });

        }

        public void DismissLoading(){
            if (loadingContentObj != null)
            {
                UIElement element = UI.GetComponent<Loading>(loadingContentObj);
                UIAnimator animator = UI.GetComponent<UIAnimator>(loadingContentObj);
                animator.DoExitAnimation(element, null, () =>
                {
                    element.OnExit();
                    loadingContentObj = null;
                    UIElement containerElement = UI.GetComponent<ContainerWindow>(loadingObj);
                    UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(loadingObj);
                    containerElement.OnEnter();
                    containerAnimator.DoExitAnimation(containerElement, null, () =>
                    {
                        Destroy(loadingObj);
                        loadingObj = null;
                    });
                });
            }
            else if (loadingObj != null)
            {
                UIElement containerElement = UI.GetComponent<ContainerWindow>(loadingObj);
                UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(loadingObj);
                containerElement.OnEnter();
                containerAnimator.DoExitAnimation(containerElement, null, () =>
                {
                    Destroy(loadingObj);
                    loadingObj = null;
                });
            }
        }


    

        private void CreateContainer()
        {
            containerObj = Instantiate(Resources.Load("Prefabs/XDSDK")) as GameObject;
            containerObj.name = "XDSDKContainer";
            UnityEngine.Object.DontDestroyOnLoad(containerObj);
            UIElement containerElement = UI.GetComponent<ContainerWindow>(containerObj);
            UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(containerObj);
            containerElement.OnEnter();
            containerAnimator.DoEnterAnimation(null, containerElement, () =>
            {

            });
        }

        private void DestoryContainer()
        {
            if (containerObj != null)
            {
                UIElement containerElement = UI.GetComponent<ContainerWindow>(containerObj);
                UIAnimator containerAnimator = UI.GetComponent<UIAnimator>(containerObj);
                containerElement.OnEnter();
                containerAnimator.DoExitAnimation(containerElement, null, () =>
                {
                    Destroy(containerObj);
                    containerObj = null;
                });
            }
        }

        private void PushUIElement<T>(string prefabName, 
                                      Dictionary<string, object> extra, 
                                      Action<int, object> callback) where T : UIElement
        {
            GameObject gameObj = Instantiate(Resources.Load("Prefabs/" + prefabName)) as GameObject;
            if (gameObj == null)
            {
                Debug.LogError("Could not find prefab named \"" + prefabName + "\"");
            }
            else
            {
                if (uiElements.Count == 0 && containerObj == null)
                {
                    CreateContainer();
                }
                gameObj.name = prefabName;
                UnityEngine.Object.DontDestroyOnLoad(gameObj);
                UIElement element = UI.GetComponent<T>(gameObj);
                element.Extra = extra;
                element.Callback += callback;
                element.transform.SetParent(containerObj.transform, false);

                UIElement lastElement = null;
                if (uiElements.Count > 0)
                {
                    lastElement = uiElements[uiElements.Count - 1];
                }

                uiElements.Add(element);

                UIAnimator animator = UI.GetComponent<UIAnimator>(containerObj);
                element.OnEnter();
                animator.DoEnterAnimation(lastElement, element, () =>
                {
                    if (lastElement != null)
                    {
                        lastElement.OnPause();
                    }
                });
            }
        }

        private void PopUIElement(string targetName)
        {
            if (containerObj == null || uiElements.Count == 0)
            {
                Debug.LogError("No UIElement can be popped.");
            }
            else
            {
                UIElement element = uiElements[uiElements.Count - 1];

                if (targetName != null && !targetName.Equals(element.name)) {
                    Debug.LogError("Could not find specify UIElement : " + targetName);
                    return;
                }
                uiElements.RemoveAt(uiElements.Count - 1);

                UIElement lastElement = null;
                if (uiElements.Count > 0)
                {
                    lastElement = uiElements[uiElements.Count - 1];
                }

                UIAnimator animator = UI.GetComponent<UIAnimator>(containerObj);
                if(lastElement != null){
                    lastElement.OnResume();
                }
                animator.DoExitAnimation(element, lastElement, () =>
                {
                    element.OnExit();
                    if (lastElement != null)
                    {
                    }
                    if (uiElements.Count == 0)
                    {
                        DestoryContainer();
                    }
                });

            }

        }
    }
}
