using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Characters
{
    public class NpcController : MonoBehaviour
    {
        public TextAsset InkJson;
        private Canvas _canvasComponent;

		public void HandleInteract(InputAction.CallbackContext context)
        {
            if(!context.performed || !_canvasComponent.enabled)
            {
                return;
            }

            if(InkJson is null)
            {
                Debug.LogWarning("enter the ink json file");
                return;
            }
            
            EventManager.RaiseInitiateDialogue(InkJson);
        }

        private void Awake() => _canvasComponent = this.GetComponentInChildren<Canvas>();
        private void OnTriggerEnter(Collider other) => _canvasComponent.enabled = true;
        private void OnTriggerExit(Collider other) => _canvasComponent.enabled = false;
    }
}