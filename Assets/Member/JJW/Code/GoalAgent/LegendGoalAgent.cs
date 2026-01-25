using System;
using System.Collections.Generic;

namespace Member.JJW.Code.GoalAgent
{
    public abstract class LegendGoalAgent<TSelf, TGoal>
        where TSelf : LegendGoalAgent<TSelf, TGoal>
        where TGoal : Goal<TSelf, TGoal>
    {
        private readonly List<TGoal> _goals = new();

        private TGoal _currentGoal;

        public abstract TSelf RealTypeInstance { get; }

        public void AddGoal(TGoal goal)
        {
            _goals.Add(goal);
        }

        public void Tick()
        {
            foreach (var goal in _goals)
            {
                if(goal == _currentGoal&& _currentGoal.CanContinue(this))
                {
                    _currentGoal.Tick(this);
                    return;
                }
                if (goal.CanExecute(this))
                {
                    _currentGoal = goal;
                    goal.Tick(this);
                    return;
                }
            }
        }
    }
}
