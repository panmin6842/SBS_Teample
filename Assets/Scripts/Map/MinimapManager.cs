using UnityEngine;
using System.Collections.Generic;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager instance;

    [SerializeField] GameObject MapStageImage;
    [SerializeField] GameObject MiniMapStageImage;
    [SerializeField] Canvas cv;

    public List<Vector2Int> MapStagePositions = new List<Vector2Int>();
    public Vector2Int curSelectedStage;

    StageManager stageManager;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        MainmapCreate();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        MinimapCreate();
    }

    void MinimapUpdate()
    {

    }

    void MainmapCreate()
    {      
        int StageCount = StageManager.instance.StageCount;

        int countHalf = (StageCount % 2 == 1) ? StageCount / 2 + 1 : StageCount / 2;

        for (int x = -countHalf; x < StageCount - countHalf + 2; x++)
        {
            for (int y = -countHalf; y < StageCount - countHalf + 2; y++)
            {
                if (x >= -1 && x <= 1 && y >= -1 && y <= 1)
                {

                }
                else
                {
                    GameObject stageImage = Instantiate(MapStageImage, gameObject.transform);
                    stageImage.GetComponent<MinimapStage>().index = new Vector2Int(x, y);
                }

            }
        }
    }

    void MinimapCreate()
    {
        for (int i = 0; i < MapStagePositions.Count; i++)
        {
            GameObject minimapStageImage = Instantiate(MiniMapStageImage, cv.transform);
            minimapStageImage.transform.localPosition = new Vector3(MapStagePositions[i].x * 0.5f + 800, MapStagePositions[i].y * 0.5f + 400, 0);
        }
    }
}
