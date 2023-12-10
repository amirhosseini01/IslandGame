using Assets.Scripts.Core;
using UnityEngine;
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
			UiController.MainMenuContainer.style.display = DisplayStyle.Flex;
			
			UiController.Buttons = UiController.MainMenuContainer.Query<Button>(null, "menu-button").ToList();
			UiController.Buttons[0].RemoveFromClassList("bg-sky-blue");
			UiController.Buttons[0].AddToClassList("active");
		}
		public override void SelectButton()
		{
			var btn = UiController.Buttons[UiController.CurrentSelection];
			if(btn.name == "start-button")
			{
				SceneTransition.Initiate(1);
			}
		}
	}
}
