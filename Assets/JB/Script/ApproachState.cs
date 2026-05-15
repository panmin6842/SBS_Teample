using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ApproachState : IStateBase
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
    private GameObject enemyObject;

    public ApproachState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public UniTask Enter(CancellationToken token)
    {
        Debug.Log("ApproachState Enter");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        return UniTask.CompletedTask;
    }

    public UniTask Tick(CancellationToken token)
    {
        Debug.Log("ApproachState Tick");
        return UniTask.CompletedTask;
    }

    public UniTask Exit(CancellationToken token)
    {
        Debug.Log("ApproachState Exit");
        return UniTask.CompletedTask;
    }

    private async UniTask MoveToPlayer(CancellationToken token)
    {
        while (enemy.distanceToPlayer > EnemyConstant.APPROACH_DISTANCE_SQUARED && !token.IsCancellationRequested)
        {
            if (enemy.player != null && enemy.agent != null && enemy.agent.isOnNavMesh)
            {
                enemy.agent.SetDestination(enemy.player.position);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        if (enemy.agent != null && enemy.agent.isOnNavMesh) enemy.agent.ResetPath();
    }
}
