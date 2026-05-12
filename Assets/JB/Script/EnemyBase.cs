using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Approaching,
    Attacking,
    Retreating
}

public class EnemyBase : MonoBehaviour
{
    [Header("적 스탯")]
    [SerializeField] protected EnemyState state = EnemyState.Idle;
    [SerializeField] protected float health = 100;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float attackRange = 10.0f;
    [SerializeField] protected float detectionRange = 20.0f;
    [SerializeField] protected float attackCoolDown = 1.0f;

    [Header("플레이어와의 거리")]
    [SerializeField] protected float distanceToPlayer;

    public const float NEAR_BOUNDARY = 5.0f;
    public const float NEAR_BOUNDARY_SQUARED = NEAR_BOUNDARY * NEAR_BOUNDARY;
    public const float FAR_BOUNDARY = 20.0f;
    public const float FAR_BOUNDARY_SQUARED = FAR_BOUNDARY * FAR_BOUNDARY;
    public const float APPROACH_DISTANCE = 15.0f;
    public const float APPROACH_DISTANCE_SQUARED = APPROACH_DISTANCE * APPROACH_DISTANCE;

    protected Transform player;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.speed = speed;

        // CancellationToken 생성
        var token = this.GetCancellationTokenOnDestroy();
        
        // 모든 루틴에 토큰을 전달하여 파괴 시 즉시 종료되도록 함
        MainLoop(token).Forget();
    }

    protected virtual async UniTask MainLoop(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {
            await CheckDistance(token);
            await ChangeState(token);

            switch (state)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Approaching:
                    await MoveToPlayer(token);
                    break;
                case EnemyState.Attacking:
                    await Attack();
                    break;
                case EnemyState.Retreating:
                    await RetreatingFromPlayer(token);
                    break;
                default:
                    break;
            }
        }
    }

    protected virtual async UniTask ChangeState(CancellationToken token)
    {
        // 적 자체가 파괴되었는지 체크
        if (this == null || agent == null) return;

        if (distanceToPlayer >= FAR_BOUNDARY_SQUARED)
        {
            this.state = EnemyState.Approaching;
        }
        else if (distanceToPlayer <= NEAR_BOUNDARY_SQUARED)
        {
            this.state = EnemyState.Retreating;
        }
        if (distanceToPlayer <= attackRange * attackRange)
        {
            this.state = EnemyState.Attacking;
            // 대기 시간에도 토큰을 전달해야 파괴 시 즉시 멈춤
            await UniTask.Delay(TimeSpan.FromSeconds(attackCoolDown), cancellationToken: token);
        }
        await UniTask.Yield(PlayerLoopTiming.Update, token);
    }

    private async UniTask RetreatingFromPlayer(CancellationToken token)
    {
        while (distanceToPlayer < NEAR_BOUNDARY_SQUARED && !token.IsCancellationRequested)
        {
            if (player != null && agent != null && agent.isOnNavMesh)
            {
                Vector3 awayDir = (transform.position - player.position).normalized;
                agent.SetDestination(transform.position + awayDir * this.speed);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
    }

    private async UniTask MoveToPlayer(CancellationToken token)
    {
        while (distanceToPlayer > APPROACH_DISTANCE_SQUARED && !token.IsCancellationRequested)
        {
            if (player != null && agent != null && agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        if (agent != null && agent.isOnNavMesh) agent.ResetPath();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected virtual void Die()
    {
        // 파괴 전 모든 NavMesh 동작 정지
        if (agent != null && agent.isOnNavMesh) agent.isStopped = true;
        Destroy(this.gameObject);
    }

    protected virtual async UniTask CheckDistance(CancellationToken token)
    {
        try
        {
            if (player != null && this != null)
            {
                distanceToPlayer = (transform.position - player.position).sqrMagnitude;
            }
            else if (player == null)
            {
                // 플레이어가 없으면 attackCoolDown 초 대기 (토큰 포함)
                await UniTask.Delay(TimeSpan.FromSeconds(attackCoolDown), cancellationToken: token);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
        catch (OperationCanceledException) { }
    }

    protected virtual async UniTask Attack()
    {
        agent.isStopped = true;
        Debug.Log("<color=red>" + gameObject.name + " Prepares to Attack!</color>");
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: CancellationToken.None); // 공격 애니메이션(시전) 시간
        Debug.Log("<color=red>" + gameObject.name + " Attacks!</color>");

        this.state = EnemyState.Idle;
        agent.isStopped = false;
    }
}
