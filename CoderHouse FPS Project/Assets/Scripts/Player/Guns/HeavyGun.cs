using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyGun : Gun
{
    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= base.nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            base.Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }

        // Method to call Loot chests interactions
        if (Input.GetKeyUp(KeyCode.E))
        {
            base.OnCrateOpen();
        }
    }
}
