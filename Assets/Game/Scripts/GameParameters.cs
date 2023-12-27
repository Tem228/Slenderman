using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "GameParameters", menuName = "ScriptableObjects/GameParameters")]
public class GameParameters : ScriptableObject
{
    [field : SerializeField]
    public AssetReferenceGameObject DefaultMapPrefab { get; private set; }

    private void OnValidate() 
    {
        if(DefaultMapPrefab != null
        && DefaultMapPrefab.editorAsset.GetComponent<Map>() == null)
        {
            DefaultMapPrefab = null;

            throw new System.Exception($"Укажите ссылку на обьект у которого есть скрипт Map");
        }
    }
}
