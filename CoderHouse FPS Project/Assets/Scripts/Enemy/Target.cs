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
        pointsToAdd = false;
        isEnemyDead = false;
        _animator = GetComponent<Animator>();
        health = enemyStatus.health;
    }

    public void Update()
    {

    }

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
        isEnemyDead = true;
        _animator.SetTrigger("dead");
        Destroy(gameObject, 5f);
        AddScore();

        
    }

    private void AddScore()
    {
        if (pointsToAdd == false)
        {
            GameManager.instance.AddPoints();
            pointsToAdd = true;
            
        }
    }
}
