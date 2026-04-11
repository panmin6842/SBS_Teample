using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapStage : MonoBehaviour
{
    MinimapManager minimapManager;

    public Vector2Int index;

    public MinimapStageType stageType;

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (minimapManager != null)
        {
            minimapManager = MinimapManager.instance;
        }

        transform.localPosition = new Vector2(index.x * 25, index.y * 25);
    }

    void OnStageClick()
    {

        minimapManager.curSelectedStage = index;
    }
}
