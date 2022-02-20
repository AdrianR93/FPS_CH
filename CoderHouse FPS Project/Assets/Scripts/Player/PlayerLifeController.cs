using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLifeController : MonoBehaviour
{
    public Team Teams => _team;
    [SerializeField] private Team _team;
    GameManager instance;
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    [SerializeField] private Transform playerHome;
    public static bool _isPlayerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log($"Player has been hit for {damage} hit points. ");
    }

    private void Death()
    {
        if (currentHealth < 1)
        {
            _isPlayerDead = true;
            GameManager.instance.GameOverScreen();
            Debug.Log("Player is dead");
        }

    }

    public enum Team
    {
        Red,
        Blue
    }
}
