using System.Collections.Generic;
using Member.JJW.Code.GoalAgent;
using NUnit.Framework;
using UnityEngine;

namespace JJW._02_Code
{
    public abstract class GoalAgent<TSelf> where TSelf : GoalAgent<TSelf>
    {
        public abstract TSelf RealTypeInstance { get; }
        private List<AgentGoal<TSelf>> _goals;
        private AgentGoal<TSelf> _currentGoal;

        public void AddGoal(AgentGoal<TSelf> agentGoal)
        {
            _goals.Add(agentGoal);
        }

        public void Tick()
        {
            foreach (var goal in _goals)
            {
                if (goal == _currentGoal && _currentGoal.CanContinue(this))
                {
                    _currentGoal.Tick(this);
                    return;
                }

                if (goal.CanExecute(this))
                {
                    _currentGoal = goal;
                    _currentGoal.Tick(this);
                    return;
                }
            }
        }
    }
}