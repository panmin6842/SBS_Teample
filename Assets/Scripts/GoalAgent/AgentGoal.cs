using UnityEngine;

namespace GoalAgent
{
    public abstract class AgentGoal : ScriptableObject
    {
        protected GoalAgent OwnerAgent;

        public void Init(GoalAgent agent)
        {
            OwnerAgent = agent;
        }

        public abstract bool CanExecute();
        public abstract bool CanContinue();
        public abstract void Tick();
    }
}