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
        [SerializeField] private float _walkDuration = 3f;
        [SerializeField] private float _pauseDuration = 2f;
        private float _walkTime = 0f;
        private float _pauseTime = 0f;
        private bool _isWalking = true;

        public Vector3 GetNextPosition()
        {
            return _splineContainer.EvaluatePosition(_splinePosition);
        }

        public void CalculateNextPosition()
        {
            _walkTime += Time.deltaTime;

            if(_walkTime > _walkDuration)
            {
                _isWalking = false;
            }

            if(!_isWalking)
            {
                _pauseTime += Time.deltaTime;

                if(_pauseTime < _pauseDuration)
                {
                    return;
                }

                _pauseTime = 0f;
                _walkTime = 0f;
                _isWalking = true;
            }

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
