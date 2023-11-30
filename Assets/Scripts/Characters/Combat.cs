using System;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class Combat : MonoBehaviour
{
    [NonSerialized]
    public float Damage = 0f;

    [NonSerialized]
    public bool IsAttacking = false;

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
        _bubbleEventComponent.OnBubbleHit += HandleBubbleHit;
    }

    private void OnDisable()
    {
        _bubbleEventComponent.OnBubbleStartAttack -= HandleBubbleStartAttack;
        _bubbleEventComponent.OnBubbleCompleteAttack -= HandleBubbleCompleteAttack;
        _bubbleEventComponent.OnBubbleHit -= HandleBubbleHit;
    }

    public void StartAttack()
    {
        if (IsAttacking)
        {
            return;
        }

        _animatorComponent.SetFloat(Constants.SpeedAnimatorParam, 0);
        _animatorComponent.SetTrigger(Constants.AttackAnimatorParam);

    }

    private void HandleBubbleStartAttack()
    {
        IsAttacking = true;
    }

    private void HandleBubbleCompleteAttack()
    {
        IsAttacking = false;
    }

    private void HandleBubbleHit()
    {
        #pragma warning disable UNT0028
        var targets = Physics.BoxCastAll(
            center: transform.position + transform.forward,
            halfExtents: transform.localScale / 2,
            direction: transform.forward,
            orientation: transform.rotation,
            maxDistance: 1f
        );
        #pragma warning restore UNT0028

        foreach (var target in targets)
        {
            if (CompareTag(target.transform.tag))
            {
                continue;
            }

            var healthComponent = target.transform
                .gameObject.GetComponent<Health>();

            if (healthComponent is null)
            {
                return;
            }

            healthComponent.TakeDamage(Damage);
        }
    }
}