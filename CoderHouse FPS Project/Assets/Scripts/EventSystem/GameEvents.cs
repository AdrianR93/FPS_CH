using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onDoorwayTriggerEnter;
    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }

    }

    public event Action<int> OnDoorwayTriggerExit;
    public void DoorwayTriggerExit(int id)
    {
        if(OnDoorwayTriggerExit !=null)
        {
            OnDoorwayTriggerExit(id);
        }
    }

    public event Action<int> onCrateOpen;
    public void CrateOpen(int id)
    {
        if (onCrateOpen !=null)
        {
            onCrateOpen(id);
        }
    }

}
