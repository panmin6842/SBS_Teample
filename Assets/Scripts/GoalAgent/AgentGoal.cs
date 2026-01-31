using UnityEngine;

namespace GoalAgent
{
    public abstract class AgentGoal : ScriptableObject
    {
        protected GoalAgent ownerAgent;

        public void Init(GoalAgent agent)
        {
            ownerAgent = agent;
        }

        public abstract bool CanExecute();
        public abstract bool CanContinue();
        public abstract void Tick();
    }
}