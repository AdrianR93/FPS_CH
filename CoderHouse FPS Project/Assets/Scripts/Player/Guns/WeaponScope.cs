using System.Collections;
using UnityEngine;

public class WeaponScope : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool isScoped = false;

    [SerializeField] GameObject weaponCamera;

    [SerializeField] Camera mainCamera;

    [SerializeField] private GameObject scopeOverlay;

    float scopedFOV = 15f;

    float normalFOV;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

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
