using UnityEngine;
using UnityEngine.Splines;

namespace Assets.Scripts.Characters
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField]
        private GameObject _splineGameObject;
        private SplineContainer _splineContainer;

        private float _splinePosition = 0f;

        public Vector3 GetNextPosition()
        {
            return _splineContainer.EvaluatePosition(_splinePosition);
        }

        public void CalculateNextPosition()
        {
            _splinePosition += Time.deltaTime;

            if(_splinePosition > 1f)
            {
                _splinePosition = 0f;
            }
        }

        private void Awake()
        {
            _splineContainer = _splineGameObject.GetComponent<SplineContainer>();
        }
    }
}
