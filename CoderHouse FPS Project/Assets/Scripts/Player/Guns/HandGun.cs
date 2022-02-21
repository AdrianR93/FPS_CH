using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : Gun
{


    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            shootingAnimator.SetTrigger("PistolShoot");
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }
    }
}
