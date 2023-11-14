using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent Agent;
        private Vector3 MovementVector;
        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();

            Rotate();
        }

        private void MovePlayer()
        {
            var offset = Agent.speed * Time.deltaTime * MovementVector;

            Agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            MovementVector = new(input.x, 0, input.y);
        }

        private void Rotate()
        {
            if (MovementVector == Vector3.zero)
            {
                return;
            }

            var startRotation = transform.rotation;
            var endRotation = Quaternion.LookRotation(MovementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * Agent.angularSpeed
            );
        }

        public bool ReachedDestination()
        {
            if (Agent.pathPending)
            {
                return false;
            }

            if (Agent.remainingDistance > Agent.stoppingDistance)
            {
                return false;
            }

            if (Agent.hasPath || Agent.velocity.sqrMagnitude != 0f)
            {
                return false;
            }

            return true;
        }

        public void MoveAgentByDestination(Vector3 destination) =>
            Agent.SetDestination(destination);

        public void StopMovingAgent() =>
            Agent.ResetPath();
    }
}