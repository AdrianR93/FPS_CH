using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public int id;
    [SerializeField] private bool isElevatorUp;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip doorSound;

    // Start is called before the first frame update
    void Start()
    {
        isElevatorUp = false;
        GameEvents.current.onElevatorEnter += OnElevator;
    }

    private void OnElevator(int id)
    {
        if (id == this.id)
        {
            if (!isElevatorUp) 
            {
                LeanTween.moveLocalY(gameObject, 0.55f, 1f).setEaseInQuad();
                audioSource.clip = doorSound;
                audioSource.PlayOneShot(doorSound);
                isElevatorUp = true;
                Debug.Log("Elevator set to up");
                return;
                
            }

            if (isElevatorUp) 
            { 
            
                LeanTween.moveLocalY(gameObject, 0.02f, 1f).setEaseOutQuad();
                audioSource.clip = doorSound;
                audioSource.PlayOneShot(doorSound);
                isElevatorUp = false;
                Debug.Log("Elevator set to down");
            }
            return;

        }
    }



    private void OnDestroy()
    {
        GameEvents.current.onElevatorEnter -= OnElevator;
    }

    // Update is called once per frame
    void Update()
    {


    }
}