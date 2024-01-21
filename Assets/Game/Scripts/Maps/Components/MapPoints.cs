using UnityEngine;

public class MapPoints : MonoBehaviour
{
    [field: SerializeField]
    public Transform[] Points { get; private set; }

    public Transform DefaultPoint => Points[0];
}
