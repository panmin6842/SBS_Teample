using GoalAgent;
using UnityEngine;

namespace Enemy.EnemyGoal
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "JJW/Goal/MeleeAttack", order = 0)]
    public class EnmeyMeleeAttackSO : AgentGoal
    {
        [field: SerializeField] public float DetectionRange {get;private set; }
        [field: SerializeField] public float AttackCoolTime {get; private set; }
        [field: SerializeField] public int AdditionalMeleeAttackDamage {get; private set; }

        private float _lastAttackTime;
        
        public override bool CanExecute()
        {
            return ownerAgent.Target && Vector3.Distance(ownerAgent.Target.position,ownerAgent.transform.position) <= DetectionRange && Time.time > _lastAttackTime + AttackCoolTime;
        }

        public override bool CanContinue() => CanExecute();

        public override void Tick()
        {
            int damage = ownerAgent.EnemyInfoSoso.AttackPower + AdditionalMeleeAttackDamage;
            Debug.Log(damage);
            _lastAttackTime = Time.time;
        }
    }
}