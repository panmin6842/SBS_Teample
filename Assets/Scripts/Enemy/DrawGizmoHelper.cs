using System;
using UnityEngine;

namespace Enemy
{
    public class DrawGizmoHelper : MonoBehaviour
    {
        [SerializeField] private EnemyAttackHitboxSO[] enemyAttacks;
        private void OnDrawGizmos()
        {
            if(enemyAttacks == null)
                return;
            
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            
            foreach (var enemyAttack in enemyAttacks)
            {
                Gizmos.color = enemyAttack.GizmoColor;
                Gizmos.DrawCube(enemyAttack.GizmoOffset,enemyAttack.GizmoSize);
            }
            
        }
    }
}