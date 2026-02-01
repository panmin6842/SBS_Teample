using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    PortalManager portalManager;

    int directionX;
    int directionZ;

    public int Direction; //0:¾Õ, 1:µÚ, 2:¿Þ, 3:¿À

    void Start()
    {
    }

    void Update()
    {
        if (portalManager == null && PortalManager.instance != null)
        {
            portalManager = PortalManager.instance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portalManager.PortalEffectImage.gameObject.SetActive(true);
            DirectionCheck();
            portalManager.PlayerTransform.transform.localPosition =
                new Vector3
                (
                    portalManager.PlayerTpSpotTransform.localPosition.x + directionX, 
                    portalManager.PlayerTpSpotTransform.localPosition.y, 
                    portalManager.PlayerTpSpotTransform.localPosition.z + directionZ
                );
            portalManager.MainCameraTransform.transform.localPosition =
                new Vector3
                (
                    portalManager.MainCameraTpSpotTransform.localPosition.x + directionX, 
                    portalManager.MainCameraTpSpotTransform.localPosition.y, 
                    portalManager.MainCameraTpSpotTransform.localPosition.z + directionZ
                );
        }
    }

    void DirectionCheck()
    {
        if (Direction == 0) //¾Õ
        {
            directionX = 0;
            directionZ = 20;
        }
        else if (Direction == 1) //µÚ
        {
            directionX = 0;
            directionZ = -20;
        }
        else if (Direction == 2) //¿Þ
        {
            directionX = -20;
            directionZ = 0;
        }
        else if (Direction == 3) //¿À
        {
            directionX = 20;
            directionZ = 0;
        }
        else
        {
        }
    }
}
