using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesInteraction : MonoBehaviour
{
    public int id;
    public LayerMask whatIsCrate;
    public bool crateOpen;
    [SerializeField] Camera fpsCamera;
    private float interactableRange = 2;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCrateOpen += OnCrateOpen;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            OnCrateOpen();
        }
    }

    protected virtual void OnCrateOpen()
    {
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, interactableRange, whatIsCrate))
            {
                crateOpen = true;
                Debug.Log(hit.transform.name);

                LootChestOpen openChest = hit.transform.GetComponent<LootChestOpen>();
                openChest.OnCrateOpen();

            }
        }
    }
}
