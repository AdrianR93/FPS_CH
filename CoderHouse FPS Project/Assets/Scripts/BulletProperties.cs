using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour

{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] int damage = 35;






    void Start()

    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    // Bullet Movement;
    private void BulletMovement()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log("Bullet on movement");
    }

    // On collision damaging enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        var playerDamage = collision.gameObject.GetComponent<PlayerLifeController>();
        if (playerDamage != null)
        playerDamage.GetDamage(damage);
        Destroy(gameObject);
        
    }

}

