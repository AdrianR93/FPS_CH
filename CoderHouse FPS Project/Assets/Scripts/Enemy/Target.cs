using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public EnemyStatus enemyStatus;
    public float health;
    Animator _animator;
    public static bool isEnemyDead;
    public bool pointsToAdd;

    public void Start()
    {
        isEnemyDead = false;
        _animator = GetComponent<Animator>();
        health = enemyStatus.health;
    }

    public void Update()
    {

    }

    public void TakeDamage (float amount)
    {
        if (isEnemyDead == false) 
            health -= amount;
            if (health <= 0f)
            {
                Die();
                AddScore();
            }

    }

    private void Die()
    {
        isEnemyDead = true;
        _animator.SetTrigger("dead");
        Destroy(gameObject, 5f);
        
    }

    private void AddScore()
    {
        if (isEnemyDead == true)
        {
            GameManager.instance.AddPoints();
            
        }
    }
}
