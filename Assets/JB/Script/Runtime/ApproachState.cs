using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ApproachState : StateBase
{
    private NavMeshAgent agent;
    private GameObject enemyObject;

    private bool isRunning { get; set; } = false;
    private CancellationTokenSource cts;

    public ApproachState(EnemyBase enemy) : base(enemy)
    {
        this.enemy = enemy;
    }

    public override UniTask Enter(CancellationToken token)
    {
        Debug.Log("ApproachState Enter");
        this.agent = enemy.agent;
        this.enemyObject = enemy.gameObject;
        IsCompleted = false;
        isRunning = false;
        cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        return UniTask.CompletedTask;
    }

    public override UniTask Tick(CancellationToken token)
    {
        Debug.Log("ApproachState Tick");
        if (!isRunning && !IsCompleted)
        {
            MoveToPlayer(cts.Token).Forget();
        }
        return UniTask.CompletedTask;
    }

    public override UniTask Exit(CancellationToken token)
    {
        Debug.Log("ApproachState Exit");
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
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
                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }
            if (!token.IsCancellationRequested)
            {
                IsCompleted = true;
            }
        }
        catch (System.OperationCanceledException)
        {
            // Do nothing on cancellation
        }
        finally
        {
            isRunning = false;
        }
    }
}
