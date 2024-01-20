using UnityEngine;

public class PlayerPagesInteraction : MonoBehaviour
{
    [Header("Parameters")]

    [SerializeField]
    private float _interactionDistance;

    [SerializeField]
    private KeyCode _interactionKey;

    [SerializeField]
    private LayerMask _interactionLayer;

    [SerializeField]
    private AudioClip _pageGrabSound;

    [Header("Components")]

    [SerializeField]
    private Camera _playerCamera;

    [SerializeField]
    private AudioSource _audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(_interactionKey))
        {
            RaycastHit hit;

            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _interactionDistance, _interactionLayer))
            {
                if (hit.transform.TryGetComponent(out Page page))
                {
                    _audioSource.PlayOneShot(_pageGrabSound);

                    page.Collect();
                }
            }
        }
    }
}
