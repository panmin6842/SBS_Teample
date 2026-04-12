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

    private void Awake()
    {
        instance = this;
        stageManager = GetComponentInParent<StageManager>();
    }

    void Start()
    {
        GameObject monster = Instantiate(MonsterPrefabs[0], new Vector3(0, 2, 0), Quaternion.identity);
        CurrentAliveMonsters.Add(monster);
    }

    void Update()
    {

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

    public void MonsterDead()
    {

    }
}
