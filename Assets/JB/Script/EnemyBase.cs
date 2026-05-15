using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Enemy;
using UnityEngine;
using UnityEngine.AI;

public interface IStateBase
{
    public UniTask Enter(CancellationToken token);
    public UniTask Tick(CancellationToken token);
    public UniTask Exit(CancellationToken token);
}


public class EnemyBase : MonoBehaviour
{
    [Header("적 정보")]
    [SerializeField] protected EnemyInfoSO enemyInfo;
    [SerializeField] protected IStateBase currentState;

    [Header("적 행동")]
    public ApproachState approachState;
    public RetreatingState retreatState;
    public AttackState attackState;

    [Header("적 스탯")]
    [SerializeField] protected float health = 100.0f;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float attackRange = 10.0f;
    [SerializeField] protected float detectionRange = 20.0f;
    [SerializeField] protected float attackCoolDown = 1.0f;

    [Header("플레이어와의 거리")]
    [SerializeField] public float distanceToPlayer { get; private set; }
    public Transform player { get; private set; }
    public NavMeshAgent agent { get; private set; }

    private CancellationToken token;
    protected virtual void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        
        token = this.GetCancellationTokenOnDestroy();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) agent.speed = speed;

        SetupEnemyInfo();
        MainLoop(token).Forget();
        approachState = new ApproachState(this);
        retreatState = new RetreatingState(this);
        attackState = new AttackState(this);
    }

    void Start()
    {
        TransitionToState(approachState, token);
    }

    protected virtual void TransitionToState(IStateBase newState, CancellationToken token)
    {
        currentState?.Exit(token).Forget();
        currentState = newState;
        currentState.Enter(token).Forget();
    }

    protected virtual void SetupEnemyInfo()
    {
        if (enemyInfo != null)
        {
            this.health = enemyInfo.MaxHp;
            this.speed = enemyInfo.MoveSpeed;
            this.attackRange = enemyInfo.AttackRange;
            this.detectionRange = enemyInfo.DetectionRange;
            this.attackCoolDown = enemyInfo.AttackCoolTime;
        }
    }

    protected virtual async UniTask MainLoop(CancellationToken token)
    {
        while(!token.IsCancellationRequested)
        {
            await CheckDistance(token);
            currentState?.Tick(token).Forget();
            await ChangeState(token);
        }
    }

    protected virtual async UniTask ChangeState(CancellationToken token)
    {
        // 적 자체가 파괴되었는지 체크
        if (this == null || agent == null) return;

        if (distanceToPlayer >= EnemyConstant.FAR_BOUNDARY_SQUARED)
        {
            TransitionToState(approachState, token);
        }
        else if (distanceToPlayer <= EnemyConstant.NEAR_BOUNDARY_SQUARED)
        {
            TransitionToState(retreatState, token);
        }
        if (distanceToPlayer <= attackRange * attackRange)
        {
            TransitionToState(attackState, token);
            // 대기 시간에도 토큰을 전달해야 파괴 시 즉시 멈춤
            await UniTask.Delay(TimeSpan.FromSeconds(attackCoolDown), cancellationToken: token);
        }
        await UniTask.Yield(PlayerLoopTiming.Update, token);
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
}
