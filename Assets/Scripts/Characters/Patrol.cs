using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Scripts.Characters
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField]
        private GameObject _splineGameObject;
        private SplineContainer _splineContainer;

        private void Awake()
        {
            _splineContainer = _splineGameObject.GetComponent<SplineContainer>();
        }

        public Vector3 GetNextPosition()
        {
            return _splineContainer.EvaluatePosition(0);
        }
    }
}
