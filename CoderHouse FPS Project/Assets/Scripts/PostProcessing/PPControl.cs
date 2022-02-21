using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPControl : MonoBehaviour
{
    [SerializeField] private PostProcessVolume ppVolume;
    private Vignette _vignette;
    private Transform _vig;

    private void Awake()
    {
      var isVignette =  ppVolume.profile.TryGetSettings(out _vignette);

        if (isVignette)     
        {
            print(message: "Found Vignette");
        }
        else
        {
            print(message: "Vignette not found");

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _vignette.intensity.value += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            _vignette.intensity.value -= 0.1f;
        }

    }
}
