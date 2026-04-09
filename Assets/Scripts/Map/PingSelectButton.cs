using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PingSelectButton : MonoBehaviour
{
    MinimapManager minimapManager;

    [SerializeField] MinimapStageType ButtonImageType;
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
            case MinimapStageType.Normal:
                break;
            case MinimapStageType.Difficult:
                break;
            case MinimapStageType.Trap:
                break;
            case MinimapStageType.Treasure:
                break;
        }

    }

    public void OnButtonClick()
    {
    }
}
