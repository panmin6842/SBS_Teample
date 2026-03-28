using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MinimapStage : MonoBehaviour
{
    MinimapManager minimapManager;
    [HideInInspector] public bool isPingUiOn = false;

    public Vector2Int index;

    [SerializeField] MinimapStageType stageType;
    [SerializeField] List<Button> PingTypeButtons = new List<Button>();

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


        for (int i = 0; i < PingTypeButtons.Count; i++)
        {
            PingTypeButtons[i].transform.localPosition = new Vector2(920, 500 - i * 30);
            PingTypeButtons[i].gameObject.SetActive(isPingUiOn);
        }
    }

    public void PingUiOn()
    {
        isPingUiOn = !isPingUiOn;
    }
}
