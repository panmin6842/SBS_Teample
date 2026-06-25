using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ApproachState : IStateBase
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
    private GameObject enemyObject;

    public bool IsCompleted { get; private set; } = false;
    private bool isRunning { get; set; } = false;

    public ApproachState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public UniTask Enter(CancellationToken token)
    {
        Debug.Log("ApproachState Enter");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        IsCompleted = false;
        isRunning = false;
        return UniTask.CompletedTask;
    }

    public UniTask Tick(CancellationToken token)
    {
        Debug.Log("ApproachState Tick");
        if (!isRunning && !IsCompleted)
        {
            MoveToPlayer(token).Forget();
        }
        return UniTask.CompletedTask;
    }

    public UniTask Exit(CancellationToken token)
    {
        Debug.Log("ApproachState Exit");
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
        IsCompleted = false;
        isRunning = false;
        return UniTask.CompletedTask;
    }

    private async UniTask MoveToPlayer(CancellationToken token)
    {
        isRunning = true;
        try
        {
            while (enemy.distanceToPlayer > EnemyConstant.APPROACH_DISTANCE_SQUARED && !token.IsCancellationRequested)
            {
                if (enemy.player != null && agent != null && agent.isOnNavMesh)
                {
                    agent.SetDestination(enemy.player.position);
                }
            }
        }
        finally
        {
            IsCompleted = true;
            isRunning = false;
        }
    }
}
