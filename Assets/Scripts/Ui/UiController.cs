using System.Collections.Generic;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Scripts.Ui
{
    public class UiController : MonoBehaviour
    {
        public UiBaseState CurrentState;
        public UiMainMenuState UiMainMenuState;
        public UIDialogueState UIDialogueState;
        public List<Button> Buttons;
        public VisualElement Root;
        public VisualElement MainMenuContainer;
        public VisualElement PlayerInfoContainer;
        public int CurrentSelection = 0;
        public Label HealthLabel;
        public Label PotionsLabel;

        private UIDocument _uiDocumentComponent;

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                return;
            }
            CurrentState.SelectButton();
        }

        public void HandleNavigate(InputAction.CallbackContext context)
        {

            if (!context.performed || Buttons.Count == 0)
            {
                return;
            }

            Buttons[CurrentSelection].RemoveFromClassList("active");
            Buttons[CurrentSelection].AddToClassList("bg-sky-blue");

            var input = context.ReadValue<Vector2>();
            CurrentSelection += input.x > 0 ? 1 : -1;
            CurrentSelection = Mathf.Clamp(CurrentSelection, 0, Buttons.Count - 1);

            Buttons[CurrentSelection].AddToClassList("active");
            Buttons[CurrentSelection].RemoveFromClassList("bg-sky-blue");
        }

        private void Awake()
        {
            UiMainMenuState = new(this);
            UIDialogueState = new(this);

            _uiDocumentComponent = this.GetComponent<UIDocument>();
            Root = _uiDocumentComponent.rootVisualElement;

            MainMenuContainer = Root.Q<VisualElement>("main-menu-element");
            PlayerInfoContainer = Root.Q<VisualElement>("player-info-container");
            HealthLabel = Root.Q<Label>("health-label");
            PotionsLabel = Root.Q<Label>("potion-label");
        }
        private void Start()
        {
            var sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 0)
            {
                CurrentState = UiMainMenuState;
                CurrentState.EnterState();
            }
            else
            {
                PlayerInfoContainer.style.display = DisplayStyle.Flex;
            }
        }

        private void OnEnable()
        {
            EventManager.OnChangePlayerHealth += this.HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions += this.HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue += this.HandleInitiateDialogue;
        }

        private void OnDisable()
        {
            EventManager.OnChangePlayerHealth -= this.HandleChangePlayerHealth;
            EventManager.OnChangePlayerPotions -= this.HandleChangePlayerPotions;
            EventManager.OnInitiateDialogue -= this.HandleInitiateDialogue;
        }

        private void HandleChangePlayerHealth(float newHealthPoints)
        {
            HealthLabel.text = newHealthPoints.ToString();
        }
        private void HandleChangePlayerPotions(float newHealthPotions)
        {
            PotionsLabel.text = newHealthPotions.ToString();
        }
        private void HandleInitiateDialogue(TextAsset inkJson)
        {
            CurrentState = UIDialogueState; 
            CurrentState.EnterState(); 
        }
    }
}