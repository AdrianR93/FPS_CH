using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    GameManager instance;
    [SerializeField] private float playerLife = 100;
    [SerializeField] private Transform playerHome;
    public static bool _isPlayerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        
    }

    public void TakeDamage(float damage)
    {
        playerLife -= damage;
        Debug.Log($"Player has been hit for {damage} hit points. ");
    }

    private void Death()
    {
        if (playerLife < 1)
        {
            _isPlayerDead = true;
            GameManager.instance.GameOverScreen();
            Debug.Log("Player is dead");
        }

    }
}
