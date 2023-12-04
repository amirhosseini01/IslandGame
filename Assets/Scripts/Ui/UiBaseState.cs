namespace Assets.Scripts.Ui
{
	public abstract class UiBaseState
    {
        public UiController UiController;
        public UiBaseState(UiController uiController)
        {
            UiController = uiController;
        }
        public abstract void EnterState();
        public abstract void SelectButton();
    }
}
