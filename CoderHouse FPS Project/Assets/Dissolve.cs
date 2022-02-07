using UnityEngine;

public class Dissolve : MonoBehaviour
{
    // Component
    private Renderer _renderBoss;

    // GameObject
    public GameObject renegadeBoss;


    public void Start()
    {
        _renderBoss = renegadeBoss.GetComponent<Renderer>();
        Shader.Find("Dissolve");
        Shader.SetGlobalFloat("Dissolve", 0f);

    }

    public void Update()
    {
        
        

    }
}
