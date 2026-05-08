using UnityEngine;

public class MainFollowPC : MonoBehaviour
{
    [SerializeField] Transform playerCameraTransform;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = playerCameraTransform.position;
    }
}
