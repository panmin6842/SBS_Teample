using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
public enum WallDirection
{
    North,
    South,
    East,
    West
}

public class PortalManager : MonoBehaviour
{
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
        PortalEffectImage = GameObject.FindWithTag("FadeBackground").GetComponent<Image>();
        stageManager = StageManager.instance;
    }

    void Start()
    {
        PlayerTransform = PlayerObject.GetComponent<Transform>();
    }

    void Update()
    {
        PortalActiveCheck();
        PortalActivation();
    }

    void PortalActiveCheck()
    {
        if (true) //Portal activation check
        {
            isPortalActive = true;
        }
        //else
        //{
        //    isPortalActive = false;
        //}
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

        PortalObject[0].SetActive(north && isPortalActive);
        PortalObject[1].SetActive(south && isPortalActive);
        PortalObject[2].SetActive(west && isPortalActive);
        PortalObject[3].SetActive(east && isPortalActive);
    }
}
