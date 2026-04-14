using System.Collections;
using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    PortalManager portalManager;
    StageManager stageManager;

    //0:¾Õ, 1:µÚ, 2:¿Þ, 3:¿À
    [SerializeField] PortalDirection direction;

    void Start()
    {
    }

    void Update()
    {
        if (portalManager == null)
        {
            portalManager = GetComponentInParent<PortalManager>();
        }

        if (stageManager == null && StageManager.instance != null)
        {
            stageManager = StageManager.instance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && portalManager.PlayerObject.GetComponent<PlayerProfile>().ActCount > 0)
        {
            StartCoroutine(Teleport());
        }
        else if (portalManager.PlayerObject.GetComponent<PlayerProfile>().ActCount <= 0)
        {
            //»ç¸ÁÆ®¸®°Å
        }
    }

    IEnumerator Teleport()
    {
        portalManager.PortalEffectImage.gameObject.SetActive(true);
        portalManager.PlayerObject.transform.position = portalManager.PlayerTpSpotTransform.position;
        portalManager.MainCameraObject.transform.position = portalManager.MainCameraTpSpotTransform.position;

        switch (direction)
        {
            case PortalDirection.Front:
                portalManager.PlayerObject.transform.position += new Vector3(0f, 0f, stageManager.spacing);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, stageManager.spacing);
                break;
            case PortalDirection.Back:
                portalManager.PlayerObject.transform.position += new Vector3(0f, 0f, -stageManager.spacing);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, -stageManager.spacing);
                break;
            case PortalDirection.Left:
                portalManager.PlayerObject.transform.position += new Vector3(-stageManager.spacing, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(-stageManager.spacing, 0f, 0f);
                break;
            case PortalDirection.Right:
                portalManager.PlayerObject.transform.position += new Vector3(stageManager.spacing, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(stageManager.spacing, 0f, 0f);
                break;
        }

        portalManager.PlayerObject.GetComponent<PlayerProfile>().UseActCount(1);
        yield return null;
    }
}
