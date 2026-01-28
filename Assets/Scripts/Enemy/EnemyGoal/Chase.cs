using GoalAgent;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace JJW._02_Code
{
    [CreateAssetMenu(fileName = "Chase", menuName = "JJW/Goal/Chase", order = 0)]
    public class Chase : AgentGoal
    {
        [field: SerializeField] public float DetectionRange { get; private set; }
        [field: SerializeField] public float AdditionalChaseSpeed {get; private set; }
        
        public override bool CanExecute()
        {
            return OwnerAgent.Target&&Vector3.Distance(OwnerAgent.Target.position, OwnerAgent.transform.position) <= DetectionRange;
        }

        public override bool CanContinue() => CanExecute();

        public override void Tick()
        {
            OwnerAgent.NavMeshAgent.SetDestination(OwnerAgent.Target.position);
        }
    }
}