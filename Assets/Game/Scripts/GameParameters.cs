using UnityEngine;

[CreateAssetMenu(fileName = "GameParameters", menuName = "ScriptableObjects/GameParameters")]
public class GameParameters : ScriptableObject
{
    [field : SerializeField]
    public string DefaultMapPrefabPath { get; private set; }
}
