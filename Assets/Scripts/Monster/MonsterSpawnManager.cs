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

    public List<GameObject> CurrentAliveMonsters;

    StageManager stageManager;

    public bool isMonsterSpawn;

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
        if (!stageManager.curStageCleared)
        {
            if (stageManager.curStageType == StageType.Normal)
            {
                if (isMonsterSpawn)
                {
                    isMonsterSpawn = false;
                    for (int i = 0; i < stageManager.curStageSpawnPrefabs.Count; i++)
                    {
                        Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                        GameObject monster = Instantiate(stageManager.curStageSpawnPrefabs[i], spawnPos, Quaternion.identity);
                        CurrentAliveMonsters.Add(monster);
                    }
                } //몬스터 스폰 로직

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
                } //몬스터 생존 여부 확인
                else if (CurrentAliveMonsters.Count == 0)
                {
                    stageManager.activePortal = true;
                }
            }
            else if (stageManager.curStageType == StageType.None)
            {
                stageManager.curStageCleared = true;
            }
            else
            {
                stageManager.activePortal = true;
            }
        }
        else
        {
            stageManager.activePortal = true;
        }
    }

    public void MonsterDead()
    {

    }
}
