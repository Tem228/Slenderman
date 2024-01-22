using UnityEngine;

[RequireComponent(typeof(Light))]
public class PlayerFlashLight : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private KeyCode _offOnKey;

    [SerializeField]
    private AudioClip _offOnSound;

    [Header("Components")]

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private Light _light;

    private void OnValidate() 
    {
        if(_light == null)
        {
            _light = GetComponent<Light>();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(_offOnKey))
        {
            _light.enabled = !_light.enabled;

            _audioSource.PlayOneShot(_offOnSound);
        }
    }
}
