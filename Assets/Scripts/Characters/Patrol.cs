using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

namespace Assets.Scripts.Characters
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField]
        private GameObject _splineGameObject;
        private SplineContainer _splineContainer;
        private NavMeshAgent Agent;

        private float _splinePosition = 0f;
        private float _splineLength = 0f;
        private float _lengthWaked = 0f;

        public Vector3 GetNextPosition()
        {
            return _splineContainer.EvaluatePosition(_splinePosition);
        }

        public void CalculateNextPosition()
        {
            _lengthWaked += Time.deltaTime * Agent.speed;

            if (_lengthWaked > _splineLength)
            {
                _lengthWaked = 0f;
            }

            _splinePosition = Mathf.Clamp01(_lengthWaked / _splineLength);
        }

        private void Awake()
        {
            _splineContainer = _splineGameObject.GetComponent<SplineContainer>();
            _splineLength = _splineContainer.CalculateLength();
            Agent = GetComponent<NavMeshAgent>();
        }
    }
}
