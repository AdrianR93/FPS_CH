using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChestOpen : MonoBehaviour
{
    public int id;
    public static bool crateOpen = false;
    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();

        GameEvents.current.onCrateOpen += OnCrateOpen;

    }

    public void OnCrateOpen(int id)
    {
        if (id == this.id)
        {
            if (!crateOpen)
            {
                _animator.Play("Open");
                crateOpen = true;
            }
            else
            {
                _animator.Play("Close");
                crateOpen = false;
            }
        }
    }
}
