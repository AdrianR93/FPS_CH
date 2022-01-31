using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomProjectiles : MonoBehaviour
  
    {
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 1f);

        if (!collision.gameObject.CompareTag("Player")) return;
        {

            var playerLifeController = collision.gameObject.GetComponent<PlayerLifeController>();
            if (playerLifeController != null)
                playerLifeController.TakeDamage(AiAttackPlayer.damage);
        }
    }
}
