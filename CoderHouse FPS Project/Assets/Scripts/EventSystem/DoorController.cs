using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.OnDoorwayTriggerExit += OnDoorwayClose;
    }

    private void OnDoorwayOpen(int id)
    {
        if(id == this.id)
        {
            LeanTween.moveLocalY(gameObject, 0.5f, 1f).setEaseOutQuad();
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            LeanTween.moveLocalY(gameObject, 0.1f, 1f).setEaseInQuad();
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.OnDoorwayTriggerExit -= OnDoorwayClose;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
