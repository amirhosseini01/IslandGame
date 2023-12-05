using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts.Ui
{
    public class UiController : MonoBehaviour
    {
        public UiBaseState CurrentState;
        public UiMainMenuState UiMainMenuState;
        public List<Button> Buttons;
        public VisualElement Root;
        public int CurrentSelection = 0;

        private UIDocument _uiDocumentComponent;

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if(!context.performed)
            {
                return;
            }

            CurrentState.SelectButton();
        }

        private void Awake()
        {
            UiMainMenuState = new(this);

            _uiDocumentComponent = GetComponent<UIDocument>();
            Root = _uiDocumentComponent.rootVisualElement;
        }
        private void Start()
        {
            CurrentState = UiMainMenuState;
            CurrentState.EnterState();
        }
    }

}