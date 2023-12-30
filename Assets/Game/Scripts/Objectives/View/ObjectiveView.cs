using UnityEngine;

public class ObjectiveView : MonoBehaviour
{
    public ObjectiveInfo Info { get; private set; }

   public void Initialize(ObjectiveInfo info)
   {
        Info = info;
   }
}
