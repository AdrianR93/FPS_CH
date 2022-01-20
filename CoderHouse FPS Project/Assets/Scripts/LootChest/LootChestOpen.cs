using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestOpen : MonoBehaviour
{
    Animator _animator;
    private void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        OpenChest();
    }


    private void OpenChest()
    {
        _animator.SetTrigger("contact");
    }

}
