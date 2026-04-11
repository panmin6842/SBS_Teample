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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Instantiate(MonsterPrefabs[0], new Vector3(0, 2, 0), Quaternion.identity);
    }

    void Update()
    {
        
    }

    public void MonsterDead()
    {

    }
}
