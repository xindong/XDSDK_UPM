using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace xdsdk.Unity
{
    [DisallowMultipleComponent]
    public class InputNavigator : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        EventSystem system;
        private bool _isSelect = false;

        public event Action OnEnter;

        void Start()
        {
            system = EventSystem.current;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && _isSelect)
            {

                Selectable next = null;
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                    if (next == null) next = system.lastSelectedGameObject.GetComponent<Selectable>();

                }
                else
                {
                    next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                    try
                    {
                        if (next == null) next = system.firstSelectedGameObject.GetComponent<Selectable>();
                    }
                    catch
                    {
                    }
                }
                if (next != null)
                {
                    InputField inputfield = next.GetComponent<InputField>();
                    system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                }
                else
                {
                    Debug.Log("Could not find next widget.");
                }
            }
            else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && _isSelect)
            {
                if (OnEnter != null)
                {
                    OnEnter();
                }
            }
        }

        public void OnSelect(BaseEventData eventData)
        {
            _isSelect = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            _isSelect = false;
        }
    }
}
