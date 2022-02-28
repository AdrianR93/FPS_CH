using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        {
            GameEvents.current.ElevatorTriggerEnter(id);
        }


    }

}

