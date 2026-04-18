using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PingSelectButton : MonoBehaviour
{
    MinimapManager minimapManager;

    [SerializeField] StageType ButtonImageType;
    [SerializeField] List<Image> ButtonImages = new List<Image>();

    GameObject curSeletion;

    private void Awake()
    {
        minimapManager = MinimapManager.instance;
    }

    void Start()
    {
    }

    void Update()
    {
        switch (ButtonImageType)
        {
            case StageType.Normal:
                break;
            case StageType.Difficult:
                break;
            case StageType.Trap:
                break;
            case StageType.Treasure:
                break;
        }

    }

    public void OnButtonClick()
    {
    }
}
