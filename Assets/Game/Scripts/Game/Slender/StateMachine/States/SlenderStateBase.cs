using Unity.VisualScripting;
using UnityEngine;

public abstract class SlenderStateBase : MonoBehaviour
{
    public abstract SlenderStateType Type { get; }

   public abstract void Activate(); 

   public abstract void Deactivate();
}
