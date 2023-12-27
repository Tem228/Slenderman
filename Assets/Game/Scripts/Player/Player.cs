using Hertzole.GoldPlayer;
using UnityEngine;

[RequireComponent(typeof(GoldPlayerController))]
public class Player : MonoBehaviour
{
    [field : SerializeField]
    public GoldPlayerController Controller { get; private set; }
    
    private void OnValidate() 
    {
        if(Controller == null)
        {
            Controller = GetComponent<GoldPlayerController>();
        }   
    }
}
