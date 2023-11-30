using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [NonSerialized]
    public float HealthPoints = 0f;

    public void TakeDamage(float damageAmount)
    {
        HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0);
        print(HealthPoints);
    }
}
