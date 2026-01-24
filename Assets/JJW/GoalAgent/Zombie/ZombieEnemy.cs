using Member.JJW.Code.GoalAgent.Zombie.Goal;
using UnityEngine;

namespace Member.JJW.Code.GoalAgent.Zombie
{
    public class ZombieEnemy : MonoBehaviour
    {
        private readonly ZombieGoalAgent agent = new();
        [SerializeField] private Transform target;

        private void Awake()
        {
            agent.SelfMono = this;
            agent.Target = target;
            
            agent.AddGoal(new AttackTargetGoal(1f));
            agent.AddGoal(new FollowTargetGoal());
            agent.AddGoal(new IdleGoal());
        }

        private void Update()
        {
            agent.Tick();
        }
    }
}