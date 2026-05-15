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

    public StageManager stageManager;

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
        if (!stageManager.curStageCleared)
        {
            if (stageManager.curStageType == StageType.Normal)
            {
                if (isMonsterSpawn)
                {
                    SpawnGrid();
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
            else if (stageManager.curStageType == StageType.Bonfire)
            {
                stageManager.curStageCleared = true;
                stageManager.activePortal = true;

                if (isMonsterSpawn)
                {
                    Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                    Instantiate(stageManager.curStageSpawnPrefabs[0], spawnPos, Quaternion.identity, stageManager.transform);

                    isMonsterSpawn = false;
                }

                Debug.Log("플레이어 회복");
               
                GameManager.instance.OnShelterEnter?.Invoke();
                PlayerProfile playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
                if (playerProfile != null)
                {
                    playerProfile.MPBuff(4);
                    if (!GameManager.instance.shelterHpBan)
                        playerProfile.HPBuff(0.5f);
                    
                    if (!GameManager.instance.shelterActCountBan)
                        playerProfile.ActCountPlus(3, GameManager.instance.recoveryMultiplier);
                }
            }
            else if (stageManager.curStageType == StageType.Trap)
            {
                stageManager.curStageCleared = true;
                stageManager.activePortal = true;

                if (isMonsterSpawn)
                {
                    Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 0f, stageManager.curStagePos.y * stageManager.spacing);
                    Instantiate(stageManager.curStageSpawnPrefabs[Random.Range(0, stageManager.curStageSpawnPrefabs.Count)], spawnPos, Quaternion.identity, stageManager.transform);

                    isMonsterSpawn = false;
                }
            }
            else if (stageManager.curStageType == StageType.RandomPortal)
            {
                stageManager.curStageCleared = true;

                Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                //Instantiate(stageManager.randomPortalPrefab, spawnPos, Quaternion.identity);
            }
            else if (stageManager.curStageType == StageType.Treasure)
            {
                stageManager.curStageCleared = true;
                stageManager.activePortal = true;

                if (isMonsterSpawn)
                {
                    Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                    Instantiate(stageManager.curStageSpawnPrefabs[Random.Range(0, stageManager.curStageSpawnPrefabs.Count)], spawnPos, Quaternion.identity, stageManager.transform);

                    isMonsterSpawn = false;
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

    void SpawnGrid()
    {
        int gridSize = Mathf.CeilToInt(Mathf.Sqrt(stageManager.curStageSpawnPrefabs.Count));
        float spacing = 2f;

        int count = 0;

        isMonsterSpawn = false;

        for (int i = 0; i < stageManager.curStageSpawnPrefabs.Count; i++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    if (count >= stageManager.curStageSpawnPrefabs.Count) return;

                    Vector3 spawnPos = new Vector3(stageManager.curStagePos.x * stageManager.spacing, 2f, stageManager.curStagePos.y * stageManager.spacing);
                    GameObject monster = Instantiate(stageManager.curStageSpawnPrefabs[i], spawnPos, Quaternion.identity);
                    Debug.Log("몬스터 스폰: " + monster.name);
                    CurrentAliveMonsters.Add(monster);

                    Vector3 pos = new Vector3(x * spacing, 0, z * spacing);
                    monster.transform.position += pos;
                    Debug.Log("몬스터 위치: " + monster.transform.position);

                    count++;
                }
            }
        }
    }

    public void MonsterDead(GameObject enemy)
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map");
        Instantiate(dropItems[0], enemy.transform.position, Quaternion.identity, map.transform);
    }
}
