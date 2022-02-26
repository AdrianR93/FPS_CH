using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected Recoil recoil;

    [SerializeField] protected Animator animator;
    [SerializeField] protected ParticleSystem muzzeFlash;
    [SerializeField] Camera fpsCamera;

    [SerializeField] protected float damage = 10f;
    private float range = 100f;
    [SerializeField] protected float bulletForce = 100;
    [SerializeField] protected float fireRate = 20f;

    [SerializeField] protected int TotalBullets = 50;
    [SerializeField] protected int magazineBullets = 10;
    [SerializeField] protected int currentBullets;
    private bool isReloading = false;


    [SerializeField] protected GameObject impactEffect;

    [SerializeField] protected float nextTimeToFire = 0f;
    public LayerMask whatIsEnemy;

    //Recoil Stats
    [SerializeField] protected float recoilX;
    [SerializeField] protected float recoilY;
    [SerializeField] protected float recoilZ;

    private void Start()
    {
        recoil = FindObjectOfType<Recoil>();
        currentBullets = magazineBullets;
    }

    protected virtual void Update()
    {




        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && currentBullets > 0)
        {
            currentBullets--;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


    }

    IEnumerator Reload()
    {


        isReloading = true;
        animator.SetBool("isReloading", true);
        yield return new WaitForSeconds(90f);



        isReloading = false;
        animator.SetBool("isReloading", false);
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
    }

}