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
                    portalManager.TpSpotTransform.position.x + directionX, 
                    portalManager.TpSpotTransform.position.y, 
                    portalManager.TpSpotTransform.position.z + directionZ
                );
        }
    }

    void DirectionCheck()
    {
        if (portalManager.Direction == 0) //¾Õ
        {
            directionX = 20;
        }
        else if (portalManager.Direction == 1) //µÚ
        {
            directionX = -20;
        }
        else if (portalManager.Direction == 2) //¿Þ
        {
            directionZ = -20;
        }
        else if (portalManager.Direction == 3) //¿À
        {
            directionZ = 20;
        }
        else
        {
        }
    }
}
