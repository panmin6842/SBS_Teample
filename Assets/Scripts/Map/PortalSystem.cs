using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    PortalManager portalManager;

    int directionX;
    int directionZ;

    void Start()
    {
        portalManager = PortalManager.instance;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DirectionCheck();
            portalManager.PlayerTransform.transform.position =
                new Vector3
                (
                    portalManager.PlayerTpSpotTransform.position.x + directionX, 
                    portalManager.PlayerTpSpotTransform.position.y, 
                    portalManager.PlayerTpSpotTransform.position.z + directionZ
                );
            portalManager.MainCameraTransform.transform.position =
                new Vector3
                (
                    portalManager.MainCameraTpSpotTransform.position.x + directionX, 
                    portalManager.MainCameraTpSpotTransform.position.y, 
                    portalManager.MainCameraTpSpotTransform.position.z + directionZ
                );
        }
    }

    void DirectionCheck()
    {
        if (portalManager.Direction == 0) //¾Õ
        {
            directionX = 0;
            directionZ = 20;
        }
        else if (portalManager.Direction == 1) //µÚ
        {
            directionX = 0;
            directionZ = -20;
        }
        else if (portalManager.Direction == 2) //¿Þ
        {
            directionX = -20;
            directionZ = 0;
        }
        else if (portalManager.Direction == 3) //¿À
        {
            directionX = 20;
            directionZ = 0;
        }
        else
        {
        }
    }
}
