using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    
    public int x;
    Renderer rend;
    public float dissolveSpeed = 1f;
    private float maxValue = 1;
    private bool isShaderUp;

    // Start is called before the first frame update
    void Start()
    {

        isShaderUp = true;
        x = 0;
        rend = gameObject.GetComponent<Renderer>();
        rend.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
        
    {
        if (!isShaderUp) return;
        {
            maxValue -= dissolveSpeed;
            rend.sharedMaterial.SetFloat("Dissolve", maxValue);

            if (maxValue <= 0)
            {
                maxValue = 0;
                isShaderUp = false;
            }
        }
    }
}
