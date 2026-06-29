using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class RetreatingState : IStateBase
{
    private EnemyBase enemy;
    private NavMeshAgent agent;
    public bool IsCompleted { get; private set; }
    private bool isRunning { get; set; }
    private CancellationTokenSource cts;

    public RetreatingState(EnemyBase enemy)
    {
        this.enemy = enemy;
    }

    public UniTask Enter(CancellationToken token)
    {
        Debug.Log("RetreatingState Enter");
        this.agent = enemy.agent;
        IsCompleted = false;
        isRunning = false;
        cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        return UniTask.CompletedTask;
    }

    public UniTask Tick(CancellationToken token)
    {
        Debug.Log("RetreatingState Tick");
        if(!isRunning && !IsCompleted)
            RetreatingFromPlayer(cts.Token).Forget();
        return UniTask.CompletedTask;
    }

    public UniTask Exit(CancellationToken token)
    {
        Debug.Log("RetreatingState Exit");
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

    private async UniTask RetreatingFromPlayer(CancellationToken token)
    {
        isRunning = true;
        try
        {
            if (enemy.player != null && agent != null && agent.isOnNavMesh && !token.IsCancellationRequested)
            {
                // 목적지 설정 (플레이어 반대 방향)                                                            
                Vector3 awayDir = (enemy.transform.position - enemy.player.position).normalized;
                Vector3 destination = enemy.transform.position + awayDir * agent.speed;
                agent.SetDestination(destination);

                // 목적지에 온전히 도착할 때까지 비동기로 대기 (주도권 홀딩)                                   
                await WaitForArrival(token);
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

    // NavMeshAgent가 목적지에 도달했는지 확인하는 헬퍼 메서드                                                 
    private async UniTask WaitForArrival(CancellationToken token)
    {
        Vector3 startPosition = enemy.transform.position;
        // 경로 계산 시간이 걸리므로 목적지 지정 후 최소 1프레임 대기                                          
        await UniTask.Yield(PlayerLoopTiming.Update, token);

        while (agent != null && agent.isOnNavMesh && !token.IsCancellationRequested)
        {
            float movedDistanceSq = (enemy.transform.position - startPosition).sqrMagnitude;
            if( movedDistanceSq >= EnemyConstant.RETREAT_DISTANCE_SQUARED)
            {
                Debug.Log("<color=green>Retreating Completed!</color>");
                break;
            }
            // 경로 계산이 끝났고 남은 거리가 정지 거리(stoppingDistance) 이내인 경우                          
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // 경로를 소진했거나 에이전트 속도가 거의 없는 경우 도달 완료로 판단                           
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    break;
                }
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
}
