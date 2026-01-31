using GoalAgent;
using UnityEngine;

namespace Enemy.EnemyGoal
{
    [CreateAssetMenu(fileName = "Chase", menuName = "JJW/Goal/Chase", order = 0)]
    public class EnemyChaseSO : AgentGoal
    {
        [field: SerializeField] public float DetectionRange { get; private set; }
        [field: SerializeField] public float AdditionalChaseSpeed {get; private set; }
        
        public override bool CanExecute()
        {
            return ownerAgent.Target&&Vector3.Distance(ownerAgent.Target.position, ownerAgent.transform.position) <= DetectionRange;
        }

        public override bool CanContinue() => CanExecute();

        public override void Tick()
        {
            ownerAgent.NavMeshAgent.SetDestination(ownerAgent.Target.position);
        }
    }
}