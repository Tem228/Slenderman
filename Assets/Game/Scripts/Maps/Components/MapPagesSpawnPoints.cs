using System.Linq;
using UnityEngine;

public class MapPagesSpawnPoints : MonoBehaviour
{
    [SerializeField]
    private Transform _parent;

    [SerializeField]
    private Transform[] _points;

    public Vector3[] Points { get; private set; }

    public Transform Parent => _parent;

    public void Initialize()
    {
        Points = _points.Select(point => point.position).ToArray();
    }
}
