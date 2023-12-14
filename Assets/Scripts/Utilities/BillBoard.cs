using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class BillBoard : MonoBehaviour
    {
        private GameObject _camera;
		private void Awake() => _camera = GameObject.FindGameObjectWithTag(Constants.MainCameraTag);

		private void LateUpdate() 
        {
            var cameraDirection = this.transform.position + _camera.transform.forward;

			this.transform.LookAt(cameraDirection);
        }
    }
}
