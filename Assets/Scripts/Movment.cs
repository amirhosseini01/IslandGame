using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace RPG.Character
{
    public class Movment : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Vector3 movementVector;
        private void Awake() 
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
            Rotate();
        }

        private void MovePlayer()
        {
            Vector3 offset = movementVector * Time.deltaTime * agent.speed;
            agent.Move(offset);
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new(input.x, 0, input.y);

            print(movementVector);
        }
        
        private void Rotate()
        {
            if(movementVector == Vector3.zero) return;
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.LookRotation(movementVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * agent.angularSpeed
            );
        }
    }
}