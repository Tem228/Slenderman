using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Material _skyboxMaterial;

    private void OnValidate() 
    {
        if(_skyboxMaterial != null
        && _skyboxMaterial.name.ToLower().Contains("skybox") == false)
        {
            _skyboxMaterial = null; 

            throw new System.Exception($"Выберите материал с шейдером skybox");                    
        }
    }

    private void OnDestroy() 
    {
        RenderSettings.skybox = null;
    }
     
    public void Initialize()
    {
        RenderSettings.skybox = _skyboxMaterial;
    }
}
