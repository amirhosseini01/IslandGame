using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Characters
{
    public class NpcController : MonoBehaviour
    {
        private Canvas _canvasComponent;

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if(!context.performed || !_canvasComponent.enabled)
            {
                return;
            }

            
        }

        private void Awake() => _canvasComponent = this.GetComponentInChildren<Canvas>();
        private void OnTriggerEnter(Collider other) => _canvasComponent.enabled = true;
        private void OnTriggerExit(Collider other) => _canvasComponent.enabled = false;
    }
}