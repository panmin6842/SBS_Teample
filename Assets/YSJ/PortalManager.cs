using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PortalManager : MonoBehaviour
{
    public static PortalManager instance;

    public bool isPortalActive;

    public GameObject PortalObject;
    public Transform TpSpotTransform;
    public Transform PlayerTransform;
    public int Direction; //0:¾Õ, 1:µÚ, 2:¿Þ, 3:¿À

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
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
