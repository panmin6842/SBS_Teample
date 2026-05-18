using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "JJW/Enemy/EnemyInfo", order = 0)]
    public class EnemyInfoSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public string Desc { get; set; }
        
        [field: SerializeField] public int MaxHp { get; set; }
        [field: SerializeField] public float DefensePower { get; set; }
        [field: SerializeField] public int AttackPower { get; set; }

        [field: SerializeField] public float MoveSpeed { get; set; }
        [field: SerializeField] public float AttackCoolTime { get; set; }
        [field: SerializeField] public float AttackRange { get; set; }
        [field: SerializeField] public float DetectionRange { get; set; }
    }
}