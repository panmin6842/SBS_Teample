using GoalAgent;
using UnityEngine;

namespace JJW._02_Code
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "JJW/Goal/MeleeAttack", order = 0)]
    public class MeleeAttack: AgentGoal
    {
        [field: SerializeField] public float DetectionRange {get;private set; }
        [field: SerializeField] public float AttackCoolTime {get; private set; }
        [field: SerializeField] public int AdditionalMeleeAttackDamage {get; private set; }

        private float _lastAttackTime;
        
        public override bool CanExecute()
        {
            return OwnerAgent.Target && Vector3.Distance(OwnerAgent.Target.position,OwnerAgent.transform.position) <= DetectionRange && Time.time > _lastAttackTime + AttackCoolTime;
        }

        public override bool CanContinue() => CanExecute();

        public override void Tick()
        {
            int damage = OwnerAgent.EnemyInfo.AttackPower + AdditionalMeleeAttackDamage;
            Debug.Log(damage);
            _lastAttackTime = Time.time;
        }
    }
}