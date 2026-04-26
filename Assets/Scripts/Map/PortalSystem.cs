using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalSystem : MonoBehaviour
{
    [SerializeField] PortalManager portalManager;
    StageManager stageManager;

    GameObject player;

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
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<PlayerProfile>().UseActCount(1);
            //if (other.GetComponent<PlayerProfile>().ActCount > 0)
            //{
            //    StartCoroutine(Teleport());
            //}
        }
        else if (portalManager.PlayerObject.GetComponent<PlayerProfile>().ActCount <= 0)
        {
            //»ç¸ÁÆ®¸®°Å
        }
    }

    IEnumerator Teleport()
    {
        Image img = UIManager.Instance.fade.GetComponent<Image>();
        img.gameObject.SetActive(true);
        player.transform.position = portalManager.PlayerTpSpotTransform.position;
        portalManager.MainCameraObject.transform.position = portalManager.MainCameraTpSpotTransform.position;
        portalManager.isCleared = true;

        switch (direction)
        {
            case PortalDirection.Front:
                player.transform.position += new Vector3(0f, 0f, StageManager.instance.spacing - 9f);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, StageManager.instance.spacing - 9f);
                break;
            case PortalDirection.Back:
                player.transform.position += new Vector3(0f, 0f, -StageManager.instance.spacing + 9f);
                portalManager.MainCameraObject.transform.position += new Vector3(0f, 0f, -StageManager.instance.spacing + 9f);
                break;
            case PortalDirection.Left:
                player.transform.position += new Vector3(-StageManager.instance.spacing + 9f, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(-StageManager.instance.spacing + 9f, 0f, 0f);
                break;
            case PortalDirection.Right:
                player.transform.position += new Vector3(StageManager.instance.spacing - 9f, 0f, 0f);
                portalManager.MainCameraObject.transform.position += new Vector3(StageManager.instance.spacing - 9f, 0f, 0f);
                break;
            case PortalDirection.Clear:
                //player.transform.position += new Vector3(StageManager.instance.spacing - 9f, 0f, 0f);
                //portalManager.MainCameraObject.transform.position += new Vector3(StageManager.instance.spacing - 9f, 0f, 0f);
                break;
        }

        player.GetComponent<PlayerProfile>().UseActCount(1);
        yield return null;
    }
}
