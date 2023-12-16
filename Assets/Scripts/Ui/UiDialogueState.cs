using UnityEngine.UIElements;

namespace Assets.Scripts.Ui
{
    public class UIDialogueState : UiBaseState
    {
        private VisualElement _dialogueContainer;
        // private Label _dialogueText;
        // private VisualElement _nextButton;
        // private VisualElement _choicesGroup;

        public UIDialogueState(UiController ui) : base(ui) { }

        public override void EnterState()
        {
            _dialogueContainer = UiController.Root.Q<VisualElement>("dialogue-container");
            // _dialogueText = UiController.Root.Q<Label>("dialogue-text");
            // _nextButton = UiController.Root.Q<VisualElement>("dialogue-next-button");
            // _choicesGroup = UiController.Root.Q<VisualElement>("choices-group");

            _dialogueContainer.style.display = DisplayStyle.Flex;
        }

        public override void SelectButton() { }
    }
}