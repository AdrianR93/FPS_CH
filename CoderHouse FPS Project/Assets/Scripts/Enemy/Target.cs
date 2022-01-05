using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] float health = 50.0f;

    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
