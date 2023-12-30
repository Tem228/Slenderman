using System.Collections.Generic;
using UnityEngine;

public class ObjectivesInfosService : MonoBehaviour
{
    [Header("Info")]
    [SerializeField]
    private ObjectiveInfo[] _objectives;

    private Dictionary<ObjectiveType, ObjectiveInfo> _objectivesDictionary;

    public void Initialize()
    {
        InitializeObjectivesDictionary();
    }   

    private void InitializeObjectivesDictionary()
    {
        _objectivesDictionary = new Dictionary<ObjectiveType, ObjectiveInfo>();

        for(int i = 0; i < _objectives.Length; i++)
        {
            ObjectiveInfo objective = _objectives[i];

            _objectivesDictionary.Add(objective.Type, objective);
        }

        _objectives = null;
    }

    public ObjectiveInfo GetObjectiveByType(ObjectiveType type)
    {
        if(!_objectivesDictionary.ContainsKey(type))
        {
            return null;
        }

        return _objectivesDictionary[type];
    }
}
