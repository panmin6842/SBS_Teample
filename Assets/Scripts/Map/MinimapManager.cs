using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MinimapManager : MonoBehaviour
{
    public static MinimapManager instance;

    [SerializeField] List<GameObject> PingObject = new List<GameObject>();
    [SerializeField] GameObject MapStageImage;

    public List<Vector2Int> MapStagePositions = new List<Vector2Int>();

    List<GameObject> ActivePingObject = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
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

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            transform.localPosition = new Vector2(0, 0);
            transform.localScale = new Vector2(4f, 4f);
        }
        else
        {
            transform.localPosition = new Vector2(800, 400);
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
