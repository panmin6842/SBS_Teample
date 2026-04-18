using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
public enum WallDirection
{
    North,
    South,
    East,
    West
}

public class PortalManager : MonoBehaviour
{
    public StageType stageType;
    public List<GameObject> SpawnPrefabs = new List<GameObject>();
    public bool isCleared;
    [Space(10f)]
    public bool isPortalActive;
    [Space(10f)]
    public List<GameObject> PortalObject = new List<GameObject>();
    [Space(10f)]
    public Transform PlayerTpSpotTransform;
    public Transform MainCameraTpSpotTransform;
    [Space(10f)]
    public Dictionary<WallDirection, GameObject> NearStage = new Dictionary<WallDirection, GameObject>();


     StageManager stageManager;
     [HideInInspector] public GameObject ThisStage;
     [HideInInspector] public GameObject PlayerObject;
     [HideInInspector] public GameObject MainCameraObject;
     [HideInInspector] public Image PortalEffectImage;
     [HideInInspector] public CinemachineCamera CinemachineCamera;
     [HideInInspector] public Transform PlayerTransform;

    void Awake()
    {
        ThisStage = gameObject;
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        MainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        CinemachineCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineCamera>();
        
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void Start()
    {
        if (PlayerObject != null)
            PlayerTransform = PlayerObject.GetComponent<Transform>();
        else if (PlayerObject == null)
            PlayerObject = GameObject.FindGameObjectWithTag("Player");

        PortalEffectImage = UIManager.Instance.fade.GetComponent<Image>();
        if(MainCameraObject == null)
        {
            MainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    void Update()
    {
        PortalActivation();

        isPortalActive = stageManager.activePortal;
    }

    void PortalActivation()
    {
        int x = Mathf.RoundToInt(ThisStage.transform.position.x / stageManager.spacing);
        int z = Mathf.RoundToInt(ThisStage.transform.position.z / stageManager.spacing);
        Vector2Int pos = new Vector2Int(x, z);

        bool north = stageManager.StagePositions.Contains(new Vector2Int(pos.x, pos.y + 1));
        bool south = stageManager.StagePositions.Contains(new Vector2Int(pos.x, pos.y - 1));
        bool west = stageManager.StagePositions.Contains(new Vector2Int(pos.x - 1, pos.y));
        bool east = stageManager.StagePositions.Contains(new Vector2Int(pos.x + 1, pos.y));

        if (PortalObject[0] != null)
        {
            PortalObject[0].SetActive(north && isPortalActive);
            PortalObject[0].SetActive(isPortalActive);
        }
        if (PortalObject[1] != null)
        {
            PortalObject[1].SetActive(south && isPortalActive);
            PortalObject[1].SetActive(isPortalActive);
        }
        if (PortalObject[2] != null)
        {
            PortalObject[2].SetActive(west && isPortalActive);
            PortalObject[2].SetActive(isPortalActive);
        }
        if (PortalObject[3] != null)
        {
            PortalObject[3].SetActive(east && isPortalActive);
            PortalObject[3].SetActive(isPortalActive);
        }
    }
}
