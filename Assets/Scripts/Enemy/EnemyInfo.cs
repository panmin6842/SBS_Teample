using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "JJW/Enemy/EnemyInfo", order = 0)]
    public class EnemyInfo : ScriptableObject
    {
        [field: SerializeField] public string Name { get;private set; }
        [field: SerializeField] public string Desc { get;private set; }
        
        [field: SerializeField] public int MaxHp { get;private set; }
        [field: SerializeField] public int DefensePower { get;private set; }
        [field: SerializeField] public int AttackPower { get;private set; }
    }
}