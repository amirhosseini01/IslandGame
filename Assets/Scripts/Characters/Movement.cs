using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Assets.Scripts.Utilities;
using System;

namespace Assets.Scripts.Characters
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        [NonSerialized]
        public Vector3 OriginalForwardVector;

        [NonSerialized]
        public bool IsMoving = false;

        private NavMeshAgent _agent;
        private Vector3 _movementVector;
        private Animator _animatorComponent;
        private bool _clampAnimatorSpeedAgain = true;

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                IsMoving = true;
            }
            if (context.canceled)
            {
                IsMoving = false;
            }

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

        public void MoveAgentByDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
            IsMoving = true;
        }

        public void StopMovingAgent()
        {
            _agent.ResetPath();
            IsMoving = false;
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            _agent.Move(offset);
            IsMoving = true;
        }

        public void UpdateAgentSpeed(float newSpeed, bool shouldClampSpeed)
        {
            _agent.speed = newSpeed;
            _clampAnimatorSpeedAgain = shouldClampSpeed;
        }

        public void Rotate(Vector3 newForwardVector)
        {
            if (newForwardVector == Vector3.zero)
            {
                return;
            }

            var startRotation = transform.rotation;
            var endRotation = Quaternion.LookRotation(newForwardVector);

            transform.rotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                Time.deltaTime * _agent.angularSpeed
            );
        }

        private void Start()
        {
            _agent.updateRotation = false;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animatorComponent = GetComponentInChildren<Animator>();

            OriginalForwardVector = transform.forward;
        }

        private void Update()
        {
            MovePlayer();
            MovementAnimator();

            if (CompareTag(Constants.PlayerTag))
            {
                Rotate(_movementVector);
            }
        }

        private void MovePlayer()
        {
            var offset = _agent.speed * Time.deltaTime * _movementVector;

            _agent.Move(offset);
        }

        private void MovementAnimator()
        {
            var speed = _animatorComponent.GetFloat(Constants.SpeedAnimatorParam);
            var smoothening = Time.deltaTime * _agent.acceleration;
            if (IsMoving)
            {
                speed += smoothening;
            }
            else
            {
                speed -= smoothening;
            }

            speed = Mathf.Clamp01(speed);

            if(CompareTag(Constants.EnemyTag) && _clampAnimatorSpeedAgain)
            {
                 speed = Mathf.Clamp(speed, 0f, 0.5f);
            }

            _animatorComponent.SetFloat(Constants.SpeedAnimatorParam, speed);
        }
    }
}