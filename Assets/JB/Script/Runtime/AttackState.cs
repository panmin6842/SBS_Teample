using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class AttackState : StateBase
{
    private IAttackBehavior attackBehavior;
    private NavMeshAgent agent;
    private GameObject enemyObject;
    private CancellationTokenSource cts;
    private Animator animator;
    private int attackNumber = 0;

    public AttackState(EnemyBase enemy) : base(enemy)
    {
        this.enemy = enemy;
        this.attackBehavior = enemy.attackBehavior;
        this.animator = enemy.GetComponentInChildren<Animator>();
    }
    public override async UniTask Enter(CancellationToken token)
    {
        Debug.Log("공격 상태로 진입");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        await UniTask.CompletedTask;
    }

    public override async UniTask Tick(CancellationToken token)
    {
        Debug.Log("공격 상태에서 행동 중");
        //this.animator.SetTrigger("Attack");
        attackNumber = UnityEngine.Random.Range(0, enemy.totalRatioOfAttacks);
        try
        {
            await AttackAnim(cts.Token);
            if (!cts.Token.IsCancellationRequested)
            {
                attackBehavior.Attack(attackNumber);
            }
        }
        catch (OperationCanceledException)
        {
            // Do nothing on cancellation
        }
        await UniTask.CompletedTask;
    }

    public override async UniTask Exit(CancellationToken token)
    {
        Debug.Log("공격 상태에서 나감");
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
        this.animator.ResetTrigger("Attack");
        await UniTask.CompletedTask;
    }
    protected virtual async UniTask AttackAnim(CancellationToken token)
    {
        agent.isStopped = true;
        float timeSpent = 0f;

        Debug.Log("<color=red>" + enemyObject.name + " Prepares to Attack!</color>");

        DecalProjector[] decals = enemyObject.GetComponentsInChildren<DecalProjector>();
        DecalProjector centerDecal = decals[1];

        if (attackBehavior.GetIsMultiProjectileAttack(attackNumber))
        {
            foreach (var item in decals)
            {
                if (item != null) item.enabled = true;
            }
        }
        else
            if (centerDecal != null) centerDecal.enabled = true;

        try
        {
            while (timeSpent < 0.5f && !token.IsCancellationRequested)
            {
                if (enemy.player == null || enemyObject == null) break;

                Vector3 directionToPlayer = (enemy.player.position - enemyObject.transform.position).normalized;
                directionToPlayer.y = 0f; // 수직 방향 무시

                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                // 적이 플레이어를 바라보도록 회전(시간변화량 * 속도)
                enemyObject.transform.rotation = Quaternion.Slerp(enemyObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
                timeSpent += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
        }
        finally
        {
            if (token.IsCancellationRequested) await UniTask.CompletedTask;
            if (enemyObject != null && decals != null)
            {
                foreach (var item in decals)
                    item.enabled = false;
            }
            if (agent != null)
            {
                agent.isStopped = false;
            }
        }
    }

}
