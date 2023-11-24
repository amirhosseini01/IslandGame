using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Quests
{
	public class TreasureChest : MonoBehaviour
    {
        public Animator AnimatorComponent;
        private bool _isInteracted = false;
        private bool _hasBeenOpened = false;

        public void HandleInteract(InputAction.CallbackContext context)
        { 
            if(!_isInteracted || _hasBeenOpened)
            {
                return;
            }
            if(context.canceled)
            {

            }

            AnimatorComponent.SetBool("IsShaking", false);

            _hasBeenOpened = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            _isInteracted = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _isInteracted = false;
        }
    }
}
