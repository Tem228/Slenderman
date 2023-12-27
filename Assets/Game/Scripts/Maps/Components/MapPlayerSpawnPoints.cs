using System.Linq;
using UnityEngine;

public class MapPlayerSpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] _points;

    public Vector3 DefaultPoint => Points[0];

    public Vector3 RandomPoint => Points[Random.Range(0, Points.Length)];

    public Vector3[] Points { get; private set; }

    public void Initialize()
    {
        Points = _points.Select(point => point.position).ToArray();        
    }
}
