using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace xdsdk.Unity
{
    public class RealNameWindow : UIElement
    {

        private static readonly Color NOT_INTERACTABLE_BACKGROUND_COLOR = new Color(49 / 255f, 33 / 255f, 31 / 255f);
        private static readonly Color NOT_INTERACTABLE_TEXT_COLOR = new Color(142 / 255f, 112 / 255f, 101 / 255f);
        private static readonly Color INTERACTABLE_TEXT_COLOR = new Color(51 / 255f, 51 / 255f, 51 / 255f);
        private static readonly Color INTERACTABLE_BUTTON_COLOR = new Color(225 / 255f, 102 / 255f, 0 / 255f);
        private static readonly Color NOT_INTERACTABLE_BUTTON_COLOR = new Color(252 / 255f, 223 / 255f, 213 / 255f);
        public Button close;
        public InputField realname;
        public InputField identifyNumber;
        public InputField areaCode;
        public InputField mobile;
        public InputField code;
        public Button send;
        public Button confirm;
        public Button tip;
        public Image tipPopup;
        public Text error1;
        public Image error1Icon;
        public Text error2;
        public Image error2Icon;
        public Text error3;
        public Image error3Icon;
        public Text error4;
        public Image error4Icon;

        private string clientId;
        private string userId;
        private string token;
        private string type;
        private string defaultRealname;
        private string defaultIdentifyNumber;
        private string defaultMobile;


        private static double lastFetchTime = -60000f;
        private static DateTime dateTime = new DateTime(1970, 1, 1);

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
                    if (extra.ContainsKey("client_id"))
                    {
                        clientId = extra["client_id"] as string;
                    }
                    if (extra.ContainsKey("user_id"))
                    {
                        userId = extra["user_id"] as string;
                    }
                    if (extra.ContainsKey("token"))
                    {
                        token = extra["token"] as string;
                    }
                    if (extra.ContainsKey("type"))
                    {
                        type = extra["type"] as string;
                    }
                    if (extra.ContainsKey("default_realname"))
                    {
                        defaultRealname = extra["default_realname"] as string;
                    }
                    if (extra.ContainsKey("default_identify_number"))
                    {
                        defaultIdentifyNumber = extra["default_identify_number"] as string;
                    }
                    if (extra.ContainsKey("default_mobile"))
                    {
                        defaultMobile = extra["default_mobile"] as string;
                    }
                }

                ChangeRealnameState(string.IsNullOrEmpty(defaultRealname) || string.IsNullOrEmpty(defaultIdentifyNumber));
                ChangeMobileState(string.IsNullOrEmpty(defaultMobile));


            }
        }


        void Awake()
        {
            close.onClick.AddListener(() =>
            {
                if (type.Equals("optional"))
                {
                    OnCallback(SDKManager.RESULT_SUCCESS, "Close button clicked");
                }
                else
                {
                    OnCallback(SDKManager.RESULT_CLOSE, "Close button clicked");
                }
                GetSDKManager().PopAll();
            });
            realname.onValueChanged.AddListener((string content) =>
            {
                ChangeErrorState(error1, error1Icon, false);
            });
            identifyNumber.onValueChanged.AddListener((string content) =>
            {
                ChangeErrorState(error2, error2Icon, false);
            });
            send.onClick.AddListener(() =>
            {
                if (string.IsNullOrEmpty(identifyNumber.text))
                {
                    ChangeErrorState(error2, error2Icon, true, "请输入身份证号");
                    return;
                }
                GetSDKManager().ShowLoading();
                Service.Login.VerificationCode(clientId, userId, token, GetAreaCode(), mobile.text, "bind_mobile", (string result) =>
                {
                    GetSDKManager().DismissLoading();
                    ChangeErrorState(error4, error4Icon, false);
                    lastFetchTime = (DateTime.UtcNow - dateTime).TotalMilliseconds;
                }, (string error) =>
                {
                    GetSDKManager().DismissLoading();
                    ChangeErrorState(error3, error3Icon, true, error);
                    lastFetchTime = (DateTime.UtcNow - dateTime).TotalMilliseconds;
                    Debug.Log(error);
                });

            });
            confirm.onClick.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(defaultRealname) &&
                    !string.IsNullOrEmpty(defaultIdentifyNumber) &&
                        !string.IsNullOrEmpty(defaultMobile))
                {
                    HandleConfirmResult(true);
                    return;
                }
                ChangeErrorState(error1, error1Icon, false);
                ChangeErrorState(error2, error2Icon, false);
                ChangeErrorState(error3, error3Icon, false);
                ChangeErrorState(error4, error4Icon, false);
                if (!string.IsNullOrEmpty(realname.text) &&
                !string.IsNullOrEmpty(identifyNumber.text) &&
                !string.IsNullOrEmpty(mobile.text))
                {
                    GetSDKManager().ShowLoading();
                    if (!string.IsNullOrEmpty(defaultRealname) &&
                    !string.IsNullOrEmpty(defaultIdentifyNumber))
                    {
                        if (!string.IsNullOrEmpty(code.text))
                        {
                            Service.Login.Mobile(token,
                             GetAreaCode(),
                             mobile.text,
                             code.text,
                             () =>
                             {
                                 HandleConfirmResult(true);
                             }, (string error) =>
                             {
                                 HandleConfirmResult(false, error);
                             });
                        } else
                        {
                            ChangeErrorState(error4, error4Icon, true, "请输入验证码");
                        }
                       
                    }
                    else if (!string.IsNullOrEmpty(defaultMobile))
                    {
                        Service.Login.RealName(token,
                           realname.text,
                           identifyNumber.text,
                           () =>
                           {
                               HandleConfirmResult(true);
                           }, (string error) =>
                           {
                               HandleConfirmResult(false, error);
                           });
                    } else
                    {
                        Service.Login.MobileAndRealName(token,
                         realname.text,
                         identifyNumber.text,
                         GetAreaCode(),
                         mobile.text,
                         code.text,
                         () =>
                         {
                             HandleConfirmResult(true);
                         }, (string error) =>
                         {
                             HandleConfirmResult(false, error);
                         });
                    }


                }
                else if (string.IsNullOrEmpty(realname.text))
                {
                    ChangeErrorState(error1, error1Icon, true, "请输入真实姓名");
                }
                else if (string.IsNullOrEmpty(identifyNumber.text))
                {
                    ChangeErrorState(error2, error2Icon, true, "请输入身份证号");
                }
                else if (string.IsNullOrEmpty(mobile.text))
                {
                    ChangeErrorState(error3, error3Icon, true, "请输入手机号");
                }
            });
            tip.onClick.AddListener(() =>
            {
                tipPopup.gameObject.SetActive(!tipPopup.IsActive());
            });
            code.onValueChanged.AddListener((string content) =>
            {
                ChangeErrorState(error4, error4Icon, false);
            });
            ChangeErrorState(error1, error1Icon, false);
            ChangeErrorState(error2, error2Icon, false);
            ChangeErrorState(error3, error3Icon, false);
            ChangeErrorState(error4, error4Icon, false);
            tipPopup.gameObject.SetActive(false);
        }

        void Update()
        {
            double current = (DateTime.UtcNow - dateTime).TotalMilliseconds;
            if (current - lastFetchTime < 60000)
            {
                send.transition = Selectable.Transition.None;
                send.GetComponent<Image>().color = NOT_INTERACTABLE_BUTTON_COLOR;
                Text text = send.GetComponentInChildren<Text>();
                text.text = (int)((60000 + lastFetchTime - current) / 1000) + "s重新获取";
                text.color = NOT_INTERACTABLE_TEXT_COLOR;
                send.interactable = false;
            }
            else if (!send.interactable)
            {

                send.transition = Selectable.Transition.ColorTint;
                send.GetComponent<Image>().color = INTERACTABLE_BUTTON_COLOR;
                Text text = send.GetComponentInChildren<Text>();
                text.text = "发送验证码";
                text.color = Color.white;
                send.interactable = true;
            }
        }

        private string GetAreaCode()
        {
            if(string.IsNullOrEmpty(areaCode.text))
            {
                return "86";
            } else
            {
                return areaCode.text.Replace("+", "");
            }
        }

        private void ChangeRealnameState(
          bool interactable
          )
        {
            realname.interactable = interactable;
            identifyNumber.interactable = interactable;
            if (interactable)
            {
                realname.GetComponent<Image>().color = Color.white;
                identifyNumber.GetComponent<Image>().color = Color.white;
                realname.transform.Find("Text").GetComponent<Text>().color = INTERACTABLE_TEXT_COLOR;
                identifyNumber.transform.Find("Text").GetComponent<Text>().color = INTERACTABLE_TEXT_COLOR;
                realname.transform.Find("Placeholder").GetComponent<Text>().text = "请输入真实姓名";
                identifyNumber.transform.Find("Placeholder").GetComponent<Text>().text = "请输入身份证号";
            }
            else
            {
                realname.GetComponent<Image>().color = NOT_INTERACTABLE_BACKGROUND_COLOR;
                identifyNumber.GetComponent<Image>().color = NOT_INTERACTABLE_BACKGROUND_COLOR;
                realname.transform.Find("Text").GetComponent<Text>().color = NOT_INTERACTABLE_TEXT_COLOR;
                identifyNumber.transform.Find("Text").GetComponent<Text>().color = NOT_INTERACTABLE_TEXT_COLOR;
                realname.transform.Find("Placeholder").GetComponent<Text>().text = "";
                identifyNumber.transform.Find("Placeholder").GetComponent<Text>().text = "";
                realname.text = defaultRealname;
                identifyNumber.text = defaultIdentifyNumber;
            }

        }

        private void ChangeMobileState(
          bool interactable
          )
        {
            mobile.interactable = interactable;
            if (interactable)
            {
                mobile.GetComponent<Image>().color = Color.white;
                mobile.transform.Find("Text").GetComponent<Text>().color = INTERACTABLE_TEXT_COLOR;
                RectTransform rectTransform = mobile.transform.Find("Text").GetComponent<Text>().GetComponent<RectTransform>();
                Vector2 targetV2 = new Vector3(
                    63,
                    rectTransform.anchoredPosition.y
                );
                rectTransform.anchoredPosition = targetV2;
                rectTransform = mobile.transform.parent.GetComponent<RectTransform>();
                targetV2 = new Vector3(
                    rectTransform.anchoredPosition.x,
                    -46
                );
                rectTransform.anchoredPosition = targetV2;
                mobile.transform.Find("Placeholder").GetComponent<Text>().text = "请输入手机号";
            }
            else
            {
                mobile.GetComponent<Image>().color = NOT_INTERACTABLE_BACKGROUND_COLOR;
                mobile.transform.Find("Text").GetComponent<Text>().color = NOT_INTERACTABLE_TEXT_COLOR;
                RectTransform rectTransform = mobile.transform.Find("Text").GetComponent<Text>().GetComponent<RectTransform>();
                Vector2 targetV2 = new Vector3(
                    10,
                    rectTransform.anchoredPosition.y
                );
                rectTransform.anchoredPosition = targetV2;
                rectTransform = mobile.transform.parent.GetComponent<RectTransform>();
                targetV2 = new Vector3(
                    rectTransform.anchoredPosition.x,
                    -70
                );
                rectTransform.anchoredPosition = targetV2;
                mobile.transform.Find("Placeholder").GetComponent<Text>().text = "";
                if(defaultMobile.Length > 8)
                {
                    mobile.text = defaultMobile.Remove(defaultMobile.Length - 8) + "****" + defaultMobile.Substring(defaultMobile.Length - 4);
                }
            }
            mobile.transform.Find("AreaCode").gameObject.SetActive(interactable);
            code.gameObject.transform.parent.gameObject.SetActive(interactable);
            error4.gameObject.transform.parent.gameObject.SetActive(interactable);
            error4Icon.gameObject.transform.parent.gameObject.SetActive(interactable);

        }


        private void ChangeErrorState(Text target,
            Image targetIcon,
            bool active,
            string content = null)
        {
            target.text = content;
            target.gameObject.SetActive(active);
            targetIcon.gameObject.SetActive(active);
        }

        private void HandleConfirmResult(bool success, string error = null)
        {
            if (success)
            {
                GetSDKManager().DismissLoading();
                OnCallback(SDKManager.RESULT_SUCCESS, "");
                GetSDKManager().PopAll();
            }
            else
            {
                GetSDKManager().DismissLoading();
                if (!string.IsNullOrEmpty(error))
                {
                    if (error.Contains("姓名"))
                    {
                        ChangeErrorState(error1, error1Icon, true, error);
                    }
                    else if (error.Contains("证件"))
                    {
                        ChangeErrorState(error2, error2Icon, true, error);
                    }
                    else if (error.Contains("手机号码"))
                    {
                        ChangeErrorState(error3, error3Icon, true, error);
                    }
                    else if (error.Contains("验证码"))
                    {
                        ChangeErrorState(error4, error4Icon, true, error);
                    }
                    else
                    {
                        ChangeErrorState(error3, error3Icon, true, error);
                    }
                }
                Debug.LogError(error);
            }
        }
    }
}
