using UnityEngine;

public class SkeletonMonsterImage : MonoBehaviour
{
    MonsterBehavior monsterBehavior;

    private void Awake()
    {
        monsterBehavior = GetComponentInParent<MonsterBehavior>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(50, 0, 0);
    }
}
