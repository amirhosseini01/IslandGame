using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Vector3 _movementVector;

        public void HandleMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            _movementVector = new(input.x, 0, input.y);
        }
        public bool ReachedDestination()
        {
            if (_agent.pathPending)
            {
                return false;
            }

            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                return false;
            }

            if (_agent.hasPath || _agent.velocity.sqrMagnitude != 0f)
            {
                return false;
            }

            return true;
        }

        public void MoveAgentByDestination(Vector3 destination) =>
            _agent.SetDestination(destination);

        public void StopMovingAgent() =>
            _agent.ResetPath();

        public void MoveAgentByOffset(Vector3 offset)
        {
            _agent.Move(offset);
        }

        public void UpdateAgentSpeed(float newSpeed)
        {
            _agent.speed = newSpeed;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();

            Rotate();
        }

        private void MovePlayer()
        {
            var offset = _agent.speed * Time.deltaTime * _movementVector;

            _agent.Move(offset);
        }

        private void Rotate()
        {
            if (_movementVector == Vector3.zero)
            {
                return;
            }

            var startRotation = transform.rotation;
            var endRotation = Quaternion.LookRotation(_movementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * _agent.angularSpeed
            );
        }
    }
}