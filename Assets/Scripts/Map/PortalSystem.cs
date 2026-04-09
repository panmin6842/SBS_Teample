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
        if (other.CompareTag("Player") && DaySystemManager.instance.CurMoveCount > 0)
        {
            StartCoroutine(Teleport());
        }
        else if (DaySystemManager.instance.CurMoveCount <= 0)
        {
            //»ç¸ÁÆ®¸®°Å
        }
    }

    IEnumerator Teleport()
    {
        portalManager.PortalEffectImage.gameObject.SetActive(true);
        portalManager.PlayerTransform.transform.position = portalManager.PlayerTpSpotTransform.position;
        portalManager.MainCameraObject.transform.position = portalManager.MainCameraTpSpotTransform.position;

        switch (direction)
        {
            case PortalDirection.Front:
                portalManager.PlayerTransform.transform.position += new Vector3(0f, 0f, stageManager.spacing);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, stageManager.spacing);
                break;
            case PortalDirection.Back:
                portalManager.PlayerTransform.transform.position += new Vector3(0f, 0f, -stageManager.spacing);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, -stageManager.spacing);
                break;
            case PortalDirection.Left:
                portalManager.PlayerTransform.transform.position += new Vector3(-stageManager.spacing, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(-stageManager.spacing, 0f, 0f);
                break;
            case PortalDirection.Right:
                portalManager.PlayerTransform.transform.position += new Vector3(stageManager.spacing, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(stageManager.spacing, 0f, 0f);
                break;
        }

        DaySystemManager.instance.CurMoveCount--;
        yield return null;
    }
}
