using System.Collections;
using UnityEngine;

public class WeaponScope : MonoBehaviour
{
    private WeaponHolder weaponHolder;

    [SerializeField] private Animator animator;

    public bool isScoped = false;

    [SerializeField] GameObject weaponCamera;

    [SerializeField] Camera mainCamera;

    [SerializeField] private GameObject scopeOverlay;

    float scopedFOV = 15f;

    float normalFOV;

    private void Start()
    {
        weaponHolder = GetComponent<WeaponHolder>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            int aux = weaponHolder.GetCurrentWeapon();
            Debug.Log(aux);
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);
            if (aux >= 2)
            {
                if (isScoped)
                {
                    StartCoroutine(ScopeOn());
                }
                else
                {
                    ScopeOff();
                }
            }
        }
    }

    IEnumerator ScopeOn()
    {
        yield return new WaitForSeconds(.15f);

        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopedFOV;
    }

    void ScopeOff()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }
}
