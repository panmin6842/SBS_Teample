using UnityEngine;

namespace Member.JJW.Code.GoalAgent.Zombie.Goal
{
    public class FollowTargetGoal : ZombieGoal
    {
        public override bool CanExecute(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)
        {
            var zombie = agent.RealTypeInstance;
            return zombie.Target && Vector3.Distance(zombie.SelfMono.transform.position, zombie.Target.position) < 10f;
        }

        public override bool CanContinue(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent) => CanExecute(agent);

        public override void Tick(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)
        {
            var zombie = agent.RealTypeInstance;
            zombie.SelfMono.transform.position += (zombie.Target.position - zombie.SelfMono.transform.position).normalized * Time.deltaTime;
        }
    }
}