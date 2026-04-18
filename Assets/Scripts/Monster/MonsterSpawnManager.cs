using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MonsterStatData
{
    public float MaxHP;
    public float CurHP;
    public float MoveSpeed;
    [Space(10f)]
    public float AttackDamage;
    public float AttackDelay;
    [Space(10f)]
    public float AttackRange;
}

public class MonsterSpawnManager : MonoBehaviour
{
    public static MonsterSpawnManager instance;

    [SerializeField] List<GameObject> MonsterPrefabs;
    public List<GameObject> CurrentAliveMonsters;

    StageManager stageManager;

    public bool isMonsterSpawn;

    [SerializeField] private GameObject[] dropItems;

    private void Awake()
    {
        instance = this;
        stageManager = GetComponentInParent<StageManager>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (stageManager.curStageType == StageType.Normal)
        {
            if (isMonsterSpawn)
            {
                isMonsterSpawn = false;
                for (int i = 0; i < stageManager.curStageMonsterCount; i++)
                {
                    Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                    GameObject monster = Instantiate(MonsterPrefabs[0], spawnPos, Quaternion.identity);
                    CurrentAliveMonsters.Add(monster);
                }
            }

            if (CurrentAliveMonsters.Count > 0)
            {
                for (int i = 0; i < CurrentAliveMonsters.Count; i++)
                {
                    if (CurrentAliveMonsters[i] == null)
                    {
                        CurrentAliveMonsters.RemoveAt(i);
                    }
                }

                stageManager.activePortal = false;
            }
            else if (CurrentAliveMonsters.Count == 0)
            {
                stageManager.activePortal = true;
            }
        }
        else
        {
            stageManager.activePortal = true;
        }
    }

    public void MonsterDead(GameObject enemy)
    {
        Instantiate(dropItems[0], enemy.transform.position, Quaternion.identity);
    }
}
