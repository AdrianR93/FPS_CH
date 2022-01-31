using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCrosshair : MonoBehaviour
{
    
    public void EnableCrosshair()
    {
        gameObject.SetActive(true);
    }

    public void DisableCrossair()
    {
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
