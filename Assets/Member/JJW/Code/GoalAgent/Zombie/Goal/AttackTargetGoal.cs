using UnityEngine;

namespace Member.JJW.Code.GoalAgent.Zombie.Goal
{
    public class AttackTargetGoal : ZombieGoal
    {
        private float _lastAttackTime;
        private readonly float _attackCoolTime;

        public AttackTargetGoal(float attackCoolTime)
        {
            _attackCoolTime = attackCoolTime;
            _lastAttackTime = -attackCoolTime;
        }
        
        public override bool CanExecute(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)
        {
            var zombie = agent.RealTypeInstance;
            return zombie.Target && Vector3.Distance(zombie.SelfMono.transform.position, zombie.Target.position) < 3f && Time.time > _lastAttackTime + _attackCoolTime;
        }

        public override bool CanContinue(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)=> CanExecute(agent);

        public override void Tick(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)
        {
            Debug.Log("공격");
            _lastAttackTime = Time.time;
        }
    }
}