using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

public class AttackState : IStateBase
{
    private EnemyBase enemy;
    private IAttackBehavior attackBehavior;
    private NavMeshAgent agent;
    private GameObject enemyObject;

    public AttackState(EnemyBase enemy)
    {
        this.enemy = enemy;
        this.attackBehavior = enemy.attackBehavior;
    }
    public bool IsCompleted => true;
    public async UniTask Enter(CancellationToken token)
    {
        Debug.Log("공격 상태로 진입");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        await UniTask.CompletedTask;
    }

    public async UniTask Tick(CancellationToken token)
    {
        Debug.Log("공격 상태에서 행동 중");
        await AttackAnim();
        attackBehavior.Attack();
        await UniTask.CompletedTask;
    }

    public async UniTask Exit(CancellationToken token)
    {
        Debug.Log("공격 상태에서 나감");
        await UniTask.CompletedTask;
    }
    protected virtual async UniTask AttackAnim()
    {
        agent.isStopped = true;
        float timeSpent = 0f;

        Debug.Log("<color=red>" + enemyObject.name + " Prepares to Attack!</color>");
        // 공격 범위 표시
        enemyObject.GetComponentInChildren<DecalProjector>().enabled = true;
        while (timeSpent < 0.5f)
        {
            Vector3 directionToPlayer = (enemy.player.position - enemyObject.transform.position).normalized;
            directionToPlayer.y = 0f; // 수직 방향 무시

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            // 적이 플레이어를 바라보도록 회전(시간변화량 * 속도)
            enemyObject.transform.rotation = Quaternion.Slerp(enemyObject.transform.rotation, targetRotation, Time.deltaTime * 10f);
            timeSpent += Time.deltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        enemyObject.GetComponentInChildren<DecalProjector>().enabled = false;
        Debug.Log("<color=red>" + enemyObject.name + " Attacks!</color>");

        agent.isStopped = false;
    }

}
