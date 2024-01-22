using Hertzole.GoldPlayer;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(GoldPlayerController))]
public class Player : MonoBehaviour
{
    [field : SerializeField]
    public GoldPlayerController Controller { get; private set; }

    [field: SerializeField]
    public NoiseAndScratches NoiseEffect { get; private set; }

    [field: SerializeField]
    public VignetteAndChromaticAberration ChromaticAbberationEffect { get; private set; }

    public PlayerHealth HealthSystem { get; private set; }

    private void OnValidate() 
    {
        if(Controller == null)
        {
            Controller = GetComponent<GoldPlayerController>();
        }   
    }

    public void Initialize()
    {
        HealthSystem = new PlayerHealth();
    }
}
