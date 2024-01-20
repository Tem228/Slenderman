using System.Linq;
using UnityEngine;

public class MapPlayerSpawnPoints : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] Points { get; private set; }

    public Transform DefaultPoint => Points[0];
}
