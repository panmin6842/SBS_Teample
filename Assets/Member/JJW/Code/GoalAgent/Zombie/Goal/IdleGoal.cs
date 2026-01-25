namespace Member.JJW.Code.GoalAgent.Zombie.Goal
{
    public class IdleGoal : ZombieGoal
    {
        public override bool CanExecute(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent) => true;

        public override bool CanContinue(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent) => false;

        public override void Tick(LegendGoalAgent<ZombieGoalAgent, ZombieGoal> agent)
        {
            
        }
    }
}