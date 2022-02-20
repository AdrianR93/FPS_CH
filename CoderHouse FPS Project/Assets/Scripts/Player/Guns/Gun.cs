using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected Recoil recoil;


    [SerializeField] protected float damage = 10f;
    private float range = 100f;
    private float interactableRange = 2;
    [SerializeField] protected float bulletForce = 100;
    [SerializeField] protected float fireRate = 20f;

    [SerializeField] Camera fpsCamera;

    [SerializeField] protected ParticleSystem muzzeFlash;
    [SerializeField] protected GameObject impactEffect;

    [SerializeField] protected float nextTimeToFire = 0f;
    public bool crateOpen;
    public LayerMask whatIsEnemy, whatIsCrate, whatIsBoss;
    public int id;


    //Recoil Stats
    [SerializeField] protected float recoilX;
    [SerializeField] protected float recoilY;
    [SerializeField] protected float recoilZ;

    private void Start()
    {
        recoil = FindObjectOfType<Recoil>();
        GameEvents.current.onCrateOpen += OnCrateOpen;
    }
    protected virtual void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            OnCrateOpen();
        }



    }

    protected virtual private void Shoot()
    {
        
        muzzeFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, whatIsEnemy))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * bulletForce);
            }


            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGameObject, 0.3f);
        }
        RaycastHit hitBoss;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hitBoss, range, whatIsBoss))
        {
            Debug.Log(hitBoss.transform.name);

            RenegadeBoss boss = hitBoss.transform.GetComponent<RenegadeBoss>();

            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            if (hitBoss.rigidbody != null)
            {
                hitBoss.rigidbody.AddForce(-hitBoss.normal * bulletForce);
            }


            GameObject impactGameObject = Instantiate(impactEffect, hitBoss.point, Quaternion.LookRotation(hitBoss.normal));

            Destroy(impactGameObject, 0.3f);
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