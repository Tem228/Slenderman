using System.Linq;
using UnityEngine;

public class MapPagesSpawnPoints : MonoBehaviour
{
    [field: SerializeField]
    public Transform Parent { get; private set; }

    [field: SerializeField]
    public Transform[] Points { get; private set; }
}
