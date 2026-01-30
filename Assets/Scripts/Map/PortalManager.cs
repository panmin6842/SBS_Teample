using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.Cinemachine;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;

    public bool isPortalActive;
    [Space(10f)]
    public GameObject PortalObject;
    [Space(10f)]
    public Transform PlayerTpSpotTransform;
    public Transform MainCameraTpSpotTransform;
    [Space(10f)]
    [HideInInspector] public GameObject PlayerObject;
    [HideInInspector] public GameObject MainCameraObject;
    [HideInInspector] public Image PortalEffectImage;
    [HideInInspector] public CinemachineCamera CinemachineCamera;
    [HideInInspector] public Transform PlayerTransform;
    [HideInInspector] public Transform MainCameraTransform;

    void Awake()
    {
        instance = this;
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        MainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        CinemachineCamera = GameObject.Find("PlayerCamera").GetComponent<CinemachineCamera>();
        PortalEffectImage = GameObject.FindWithTag("FadeBackground").GetComponent<Image>();
    }

    void Start()
    {
        PlayerTransform = PlayerObject.GetComponent<Transform>();
        MainCameraTransform = MainCameraObject.GetComponent<Transform>();
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
