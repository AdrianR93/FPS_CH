using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int id;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip doorSound;
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
            LeanTween.moveLocalZ(gameObject, 10f, 1f).setEaseInQuad();
            audioSource.clip = doorSound;
            audioSource.PlayOneShot(doorSound);
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            LeanTween.moveLocalZ(gameObject, 4f, 1f).setEaseOutQuad();
            audioSource.clip = doorSound;
            audioSource.PlayOneShot(doorSound);
            Debug.Log($"playing {doorSound}");
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
