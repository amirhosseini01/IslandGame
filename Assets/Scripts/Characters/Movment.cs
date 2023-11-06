using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace RPG.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movment : MonoBehaviour
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
            Vector3 offset = MovementVector * Time.deltaTime * Agent.speed;

            Agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            MovementVector = new(input.x, 0, input.y);

            print(MovementVector);
        }
        
        private void Rotate()
        {
            if(MovementVector == Vector3.zero) return;

            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(MovementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * Agent.angularSpeed
            );
        }
    }
}