using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongGun : Gun
{
    [SerializeField] int numberOfBullets = 2;
    private void Start()
    {
        recoil = FindObjectOfType<Recoil>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= base.nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            for (int i = 0; i < numberOfBullets - 1; i++)
            {
                base.Shoot();
                recoil.Recoilfiring(recoilX, recoilY, recoilZ);
            }
        }
    }
}
