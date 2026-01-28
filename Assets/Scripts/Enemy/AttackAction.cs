using UnityEngine;

namespace Enemy
{
    public abstract class AttackAction : ScriptableObject
    {
        [field:SerializeField] public string Name {get; private set;}
        [field: SerializeField] public string Desc {get; private set; }
        [field:SerializeField] public float AttackCoolTime {get; private set;}
        [field:SerializeField] public int Damage {get; private set;}
        public abstract void Attack();
    }
}