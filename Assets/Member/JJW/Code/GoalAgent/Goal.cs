namespace Member.JJW.Code.GoalAgent
{
    public abstract class Goal<TAgent, TSelf>
        where TSelf : Goal<TAgent, TSelf>
        where TAgent : LegendGoalAgent<TAgent, TSelf>
    {
        public abstract bool CanExecute(LegendGoalAgent<TAgent, TSelf> agent);
        public abstract bool CanContinue(LegendGoalAgent<TAgent, TSelf> agent);
        public abstract void Tick(LegendGoalAgent<TAgent, TSelf> agent);
    }
}