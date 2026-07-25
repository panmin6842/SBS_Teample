using UnityEngine;

public class MageEnemy : EnemyBase
{
    protected override void Awake()
    {
        base.Awake();
        attackBehavior = new MageAttack(projectilePrefab, this);
        approachState = new ApproachState(this);
        retreatState = new RetreatingState(this);
        attackState = new AttackState(this);
    }
}
