
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    // Gun Stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // bools
    bool shooting, readyToShoot, reloading;

    //Reference 
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics not implemented yet
   // public CamShake camShake;
   //  public float camShakeMagnitude, camShakeDuration;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }
    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKey(KeyCode.Mouse0);

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        // Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);



       //RayCast
       if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<ShootingAI>().TakeDamage(damage);
            Debug.Log($"{rayHit.collider.name} hit for {damage} damage.");


        }

        //Shake Camera not implemented yet
        // camShake.Shake(camShakeDuration, camShakeMagnitude);


        bulletsLeft--;
        bulletsShot--;


        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);



    }

    private void ResetShot()
    {
        readyToShoot = true;

    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;

    }



}
