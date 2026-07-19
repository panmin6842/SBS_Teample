using UnityEngine;

public class TurretEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        attackBehavior = new TurretAttack(projectilePrefab, this);
        approachState = new ApproachState(this);
        retreatState = new RetreatingState(this);
        attackState = new AttackState(this);
    }
}
