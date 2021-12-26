using System;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] float damage = 10f;
    [SerializeField] float range = 100f;


    [SerializeField] Camera fpsCamera;

    [SerializeField] ParticleSystem muzzeFlash;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {

        muzzeFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
