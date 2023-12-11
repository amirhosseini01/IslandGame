using System;
using Assets.Scripts.Core;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    public UnityAction OnStartDefeated = () => {};
    [NonSerialized]
    public float HealthPoints = 0f;
    [SerializeField]
    private int _potionCount = 1;
    [SerializeField]
    private float _healthAmount = 15f;
    private Animator _animatorComponent;
    private BubbleEvent _bubbleEventComponent;
    private bool _isDefeated = false;

    public void TakeDamage(float damageAmount)
    {
        HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);

        if(this.CompareTag(Constants.PlayerTag))
        {
            EventManager.RaiseChangePlayerHealth(HealthPoints);
        }

        if (HealthPoints == 0)
        {
            this.Defeated();
        }
    }

    public void HandleHeal(InputAction.CallbackContext context)
    {
        if(!context.performed || _potionCount == 0)
        {
            return;
        }

        _potionCount --;
        HealthPoints += _healthAmount;

        EventManager.RaiseChangePlayerHealth(HealthPoints);

    }
    
    private void Awake()
    {
        if (_isDefeated)
        {
            return;
        }

        if(this.CompareTag(Constants.EnemyTag))
        {
            OnStartDefeated.Invoke();
        }

        _isDefeated = true;
        _animatorComponent = this.GetComponentInChildren<Animator>();
        _bubbleEventComponent = this.GetComponentInChildren<BubbleEvent>();
    }

    private void OnEnable() =>
        _bubbleEventComponent.OnBubbleCompleteDefeat += this.HandleBubbleCompleteDefeat;

    private void OnDisable() => 
        _bubbleEventComponent.OnBubbleCompleteDefeat -= this.HandleBubbleCompleteDefeat;

    private void Defeated() => _animatorComponent.SetTrigger(Constants.DefeatedAnimatorParam);

    private void HandleBubbleCompleteDefeat() => 
        Destroy(this.gameObject);
}
