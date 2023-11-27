using System;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
    [NonSerialized]
    public float Damage = 0f;
    private Animator _animatorComponent;

    public void HandleAttack(InputAction.CallbackContext context)
    {
        if(!context.performed)
        {
            return;
        }

        StartAttack();
    }

    private void Awake() 
    {
        _animatorComponent = GetComponentInChildren<Animator>();
    }

    private void StartAttack() 
    {
        _animatorComponent.SetFloat(Constants.SpeedAnimatorParam, 0);
        _animatorComponent.SetTrigger(Constants.AttackAnimatorParam);

    }
}