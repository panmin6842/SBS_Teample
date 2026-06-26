using UnityEngine;

public class ProjectileEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        attackBehavior = new ProjectileTypePattern(projectilePrefab, this);
        approachState = new ApproachState(this);
        retreatState = new RetreatingState(this);
        attackState = new AttackState(this);
    }
}
