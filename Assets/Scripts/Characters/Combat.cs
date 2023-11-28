using System;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
    [NonSerialized]
    public float Damage = 0f;
    private Animator _animatorComponent;
    private BubbleEvent _bubbleEventComponent;

    public void HandleAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        StartAttack();
    }

    private void Awake()
    {
        _animatorComponent = GetComponentInChildren<Animator>();
        _bubbleEventComponent = GetComponentInChildren<BubbleEvent>();
    }

    private void OnEnable()
    {
        _bubbleEventComponent.OnBubbleStartAttack += HandleBubbleStartAttack;
        _bubbleEventComponent.OnBubbleCompleteAttack += HandleBubbleCompleteAttack;
    }

    private void StartAttack()
    {
        _animatorComponent.SetFloat(Constants.SpeedAnimatorParam, 0);
        _animatorComponent.SetTrigger(Constants.AttackAnimatorParam);

    }

    private void HandleBubbleStartAttack()
    {

    }

    private void HandleBubbleCompleteAttack()
    {

    }
}