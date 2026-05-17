using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IStateBase
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
    private GameObject enemyObject;

    public AttackState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }
    public UniTask Enter(CancellationToken token)
    {
        Debug.Log("공격 상태로 진입");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        return UniTask.CompletedTask;
    }

    public UniTask Tick(CancellationToken token)
    {
        Debug.Log("공격 상태에서 행동 중");
        Attack().Forget();
        return UniTask.CompletedTask;
    }

    public UniTask Exit(CancellationToken token)
    {
        Debug.Log("공격 상태에서 나감");
        return UniTask.CompletedTask;
    }
    protected virtual async UniTask Attack()
    {
        agent.isStopped = true;

        Debug.Log("<color=red>" + enemyObject.name + " Prepares to Attack!</color>");
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: CancellationToken.None); // 공격 애니메이션(시전) 시간
        Debug.Log("<color=red>" + enemyObject.name + " Attacks!</color>");

        agent.isStopped = false;
    }

}
