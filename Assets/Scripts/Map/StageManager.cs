using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PortalDirection
{
    Front,
    Back,
    Left,
    Right
}

public enum StageType
{
    Normal,
    Difficult,
    Trap,
    SealedStone,
    Treasure,
    Boss,
    None
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public MonsterSpawnManager monsterSpawnManager;

    public bool Tutorial;


    public List<GameObject> stage = new List<GameObject>();
    public GameObject BossStage;
    public float spacing = 40f;

    public int StageCount = 7;

    public HashSet<Vector2Int> StagePositions = new HashSet<Vector2Int>();
    public List<Vector2Int> surroundStagePositions = new List<Vector2Int>();
    public Vector2Int curStagePos;
    public StageType curStageType;
    public int curStageMonsterCount;

    [SerializeField] GameObject Player;

    public bool activePortal;

    private void Awake()
    {
        monsterSpawnManager = GetComponentInChildren<MonsterSpawnManager>();
        instance = this;
    }

    void Start()
    {
        if (!Tutorial)
        {
            StartCoroutine(StageCreate());
        }
    }

    void Update()
    {
        StartCoroutine(SurroundStage());
    }

    IEnumerator SurroundStage()
    {
        for (int i = 0; i < 9; i++)
        {
            int x = curStagePos.x + (i % 3 - 1);
            int z = curStagePos.y + (i / 3 - 1);
            Vector2Int pos = new Vector2Int(x, z);
            if (StagePositions.Contains(pos))
            {
                surroundStagePositions.Add(pos);
            }
        }

        yield return null;
    }

    IEnumerator StageCreate()
    {
        int countHalf = (StageCount % 2 == 1) ? StageCount / 2 + 1 : StageCount / 2;

        for (int x = -countHalf; x < StageCount - countHalf + 2; x++)
        {
            for (int z = -countHalf; z < StageCount - countHalf + 2; z++)
            {
                if (x >= -1 && x <= 1 && z >= -1 && z <= 1)
                {
                    Instantiate(BossStage, transform.localPosition, Quaternion.identity);
                }
                else
                {
                    Vector3 spawnPos = new Vector3
                    (
                        x * spacing,
                        0f,
                        z * spacing
                    );
                    StagePositions.Add(new Vector2Int(x, z));
                    Instantiate(stage[0], spawnPos, Quaternion.identity);
                }

            }
        }

        yield return null;
    }
}
