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
            shootingAnimator.SetTrigger("HeavyShoot");
            nextTimeToFire = Time.time + 1f / fireRate;
            base.Shoot();
            recoil.Recoilfiring(recoilX, recoilY, recoilZ);
        }

    }
}
