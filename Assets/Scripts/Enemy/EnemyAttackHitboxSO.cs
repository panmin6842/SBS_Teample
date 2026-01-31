using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "EnemyAttack", menuName = "JJW/Enemy/EnemyAttack", order = 0)]
    public class EnemyAttackHitboxSO : ScriptableObject
    {
        [field: SerializeField] public Vector3 GizmoSize {get;private set;}
        [field: SerializeField] public Vector3 GizmoOffset {get;private set;}
        [field: SerializeField] public Color GizmoColor {get;private set;}
    }
}