using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private float playerLife = 100f;
    [SerializeField] private Transform playerHome;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Death();

    }
    // Get Damage Method
    public void GetDamage(float damage)
    {
        playerLife -= damage;
        Debug.Log($"Player has been hit for {damage} hit points. ");

    }

    // Healing Method
    public void Healing(float heal)
    {
        playerLife += heal;
        Debug.Log($"Player has recovered {heal} health. ");
        if (playerLife >= 100)
        {
            playerLife = 100;
        }
        Debug.Log($"Player current life is {playerLife} health");



    }
    // Death Method, respawn player in home
    private void Death()
    {
        if (playerLife < 1)
        {
            transform.position = playerHome.position;
            Debug.Log("Player has been respawned");
        }
        if (playerLife <= 0)
        {
            playerLife = 100;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Shrinker")
        {
            Debug.Log($"This object {other.gameObject.name} is a shrinker");
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collisioned with {collision.gameObject.name}");
    }
}