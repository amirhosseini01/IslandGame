using UnityEngine;

namespace Assets.Scripts.Ui
{
	public class UiController : MonoBehaviour
    {
        public UiBaseState CurrentState;
        public UiMainMenuState UiMainMenuState;
        private void Awake()
        {
            UiMainMenuState = new(this);
        }
        private void Start()
        {
            CurrentState = UiMainMenuState;
            CurrentState.EnterState();
        }
    }

}