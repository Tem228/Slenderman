using System;
using UnityEngine;

public class ObjectiveInfo 
{
    [SerializeField]
    private string _description;

    [SerializeField]
    private int _maxProgress;

    private int _currentProgress;

    [SerializeField]
    private ObjectiveType _type;

    public string Desciption => string.Format(_description, CurrentProgress, MaxProgress);

    public int MaxProgress => _maxProgress;

    public int CurrentProgress 
    {
        get => _currentProgress;

        set 
        {
            if(value < 0)
            {
                throw new Exception("Значение не может быть ниже 0");
            }

            _currentProgress = value;

            ProgressUpdated?.Invoke(_currentProgress);
        }
    }

    public ObjectiveType Type => _type;    

    public event Action<int> ProgressUpdated;
}
