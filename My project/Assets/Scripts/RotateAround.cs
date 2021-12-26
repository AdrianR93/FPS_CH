using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform point = null;
    [SerializeField] Vector3 axis = new Vector3(0f, 1.0f, 0f);
    [SerializeField] private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point.position, axis, speed * Time.deltaTime);
    }
}
