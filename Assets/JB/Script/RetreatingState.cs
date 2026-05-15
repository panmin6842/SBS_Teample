using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class RetreatingState : IStateBase
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
    private GameObject enemyObject;

    public RetreatingState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public UniTask Enter(CancellationToken token)
    {
        Debug.Log("RetreatingState Enter");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        return UniTask.CompletedTask;
    }

    public UniTask Tick(CancellationToken token)
    {
        Debug.Log("RetreatingState Tick");
        RetreatingFromPlayer(token).Forget();
        return UniTask.CompletedTask;
    }

    public UniTask Exit(CancellationToken token)
    {
        Debug.Log("RetreatingState Exit");
        return UniTask.CompletedTask;
    }

    private async UniTask RetreatingFromPlayer(CancellationToken token)
    {
        if (enemy.distanceToPlayer < EnemyConstant.NEAR_BOUNDARY_SQUARED && !token.IsCancellationRequested)
        {
            if (enemy.player != null && agent != null && agent.isOnNavMesh)
            {
                Vector3 awayDir = (enemy.transform.position - enemy.player.position).normalized;
                agent.SetDestination(enemy.transform.position + awayDir * agent.speed);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
    }
}
