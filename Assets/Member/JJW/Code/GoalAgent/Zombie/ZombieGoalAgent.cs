using UnityEngine;

namespace Member.JJW.Code.GoalAgent.Zombie
{
    public class ZombieGoalAgent : LegendGoalAgent<ZombieGoalAgent, ZombieGoal>
    {
        public override ZombieGoalAgent RealTypeInstance => this;
        
        public Transform Target;
        public MonoBehaviour SelfMono;
    }
}