using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

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
    public GameObject PlayerObject;
    public GameObject MainCameraObject;
    public Image PortalEffectImage;
    [HideInInspector] public Transform PlayerTransform;
    [HideInInspector] public Transform MainCameraTransform;
    [Space(10f)]
    public int Direction; //0:¾Õ, 1:µÚ, 2:¿Þ, 3:¿À

    void Awake()
    {
        instance = this;
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
