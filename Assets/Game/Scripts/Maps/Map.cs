using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private Material _skyboxMaterial;

    [field : SerializeField]
    public MapPlayerSpawnPoints PlayerSpawnPoints { get; private set; }

    [field: SerializeField]
    public MapPagesSpawnPoints PageSpawnPoints { get; private set; }

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

        PlayerSpawnPoints.Initialize();

        PageSpawnPoints.Initialize();
    }
}
