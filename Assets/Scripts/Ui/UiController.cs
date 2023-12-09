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

        public void HandleNavigate(InputAction.CallbackContext context)
        {
            if(!context.performed || Buttons.Count == 0)
            {
                return;
            }

            Buttons[CurrentSelection].RemoveFromClassList("active");
            Buttons[CurrentSelection].AddToClassList("bg-sky-blue");

            var input = context.ReadValue<Vector2>();
            CurrentSelection += input.x > 0? 1: -1;
            CurrentSelection = Mathf.Clamp(CurrentSelection, 0, Buttons.Count - 1);

            Buttons[CurrentSelection].AddToClassList("active");
            Buttons[CurrentSelection].RemoveFromClassList("bg-sky-blue");
            Debug.Log("test");
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