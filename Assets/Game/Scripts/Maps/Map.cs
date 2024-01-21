using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private Material _skyboxMaterial;

    [field: SerializeField]
    public Terrain Terrain { get; private set; }

    [field : SerializeField]
    public MapPoints PlayerSpawnPoints { get; private set; }

    [field: SerializeField]
    public MapPoints PagesSpawnPoints { get; private set; }

    [field: SerializeField]
    public MapPoints SlenderSpawnPoints { get; private set; }

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
