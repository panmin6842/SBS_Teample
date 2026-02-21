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
    public bool isPortalActive;
    [Space(10f)]
    public GameObject PortalObject;
    [Space(10f)]
    public Transform PlayerTpSpotTransform;
    public Transform MainCameraTpSpotTransform;
    [Space(10f)]
    public Dictionary<WallDirection, GameObject> NearStage = new Dictionary<WallDirection, GameObject>();
    public List<PortalManager> LinkedStageList = new List<PortalManager>();



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
    }

    void Start()
    {
        PlayerTransform = PlayerObject.GetComponent<Transform>();
    }

    void Update()
    {
        PortalActiveCheck();
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
}
