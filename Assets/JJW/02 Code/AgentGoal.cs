using Member.JJW.Code.GoalAgent;
using UnityEngine;

namespace JJW._02_Code
{
    public abstract class AgentGoal<TAgent> : ScriptableObject where TAgent : GoalAgent<TAgent> 
    {
        public abstract bool CanExecute(GoalAgent<TAgent> agent);
        public abstract bool CanContinue(GoalAgent<TAgent> agent);
        public abstract bool Tick(GoalAgent<TAgent> agent);
    }
}