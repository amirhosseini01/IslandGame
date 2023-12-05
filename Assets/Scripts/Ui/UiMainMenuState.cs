using UnityEngine.UIElements;

namespace Assets.Scripts.Ui
{
	public class UiMainMenuState : UiBaseState
	{
		public UiMainMenuState(UiController uiController) : base(uiController)
		{

		}
		public override void EnterState()
		{
			UiController.Buttons = UiController.Root.Query<Button>(null, "menu-button").ToList();
		}
		public override void SelectButton()
		{
			// var btn = UiController.Buttons[UiController.CurrentSelection];
		}
	}
}
