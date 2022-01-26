using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGun : Gun
{


    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }
    }
}
