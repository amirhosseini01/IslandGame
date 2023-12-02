using System;
using Assets.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityAction OnStartDefeated = () => {};
    [NonSerialized]
    public float HealthPoints = 0f;
    private Animator _animatorComponent;
    private BubbleEvent _bubbleEventComponent;
    private bool _isDefeated = false;

    public void TakeDamage(float damageAmount)
    {
        HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);


        if (HealthPoints == 0)
        {
            Defeated();
        }
    }

    private void Awake()
    {
        if (_isDefeated)
        {
            return;
        }

        if(CompareTag(Constants.EnemyTag))
        {
            OnStartDefeated.Invoke();
        }

        _isDefeated = true;
        _animatorComponent = GetComponentInChildren<Animator>();
        _bubbleEventComponent = GetComponentInChildren<BubbleEvent>();
    }

    private void OnEnable()
    {
        _bubbleEventComponent.OnBubbleCompleteDefeat += HandleBubbleCompleteDefeat;
    }

    private void OnDisable()
    {
        _bubbleEventComponent.OnBubbleCompleteDefeat -= HandleBubbleCompleteDefeat;
    }

    private void Defeated()
    {
        _animatorComponent.SetTrigger(Constants.DefeatedAnimatorParam);
    }

    private void HandleBubbleCompleteDefeat()
    {
        Destroy(gameObject);
    }
}
