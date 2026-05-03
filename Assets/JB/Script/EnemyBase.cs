using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] protected int health = 100;
    [SerializeField] protected float speed = 10.0f;
    [SerializeField] protected float attackRange = 10.0f;
    [SerializeField] protected float detectionRange = 20.0f;

    public const float CLOSED_BOUNDARY = 5.0f;
    public const float FAR_BOUNDARY = 20.0f;

    protected Transform player;
    protected NavMeshAgent agent;
    private Coroutine distanceCheckCoroutine;
    private Coroutine attackCoroutine;

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        // 무한 루프 방지를 위해 코루틴으로 실행
        distanceCheckCoroutine = StartCoroutine(CheckDistanceToPlayerRoutine());
        attackCoroutine = null; // 공격 코루틴은 필요할 때 시작
    }

    protected virtual void OnDestroy()
    {
        // 객체 파괴 시 코루틴 정지하여 메모리 누수 방지
        if (distanceCheckCoroutine != null)
        {
            StopCoroutine(distanceCheckCoroutine);
        }
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }

    protected virtual Vector3 DetectPlayer()
    {
        Debug.Log("Enemy detects the player!");
        return player != null ? player.position : Vector3.zero;
    }

    protected virtual void PrepareForAttack()
    {
        Debug.Log("Enemy sets up for an attack!");
    }

    protected virtual void Attack()
    {
        Debug.Log("Enemy attacks!");
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Remaining health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Enemy dies!");
        Destroy(this.gameObject);
    }

    protected virtual IEnumerator CheckDistanceToPlayerRoutine()
    {
        // 0.1초마다 체크하여 CPU 부하를 줄임
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (true)
        {
            if (player == null)
            {
                yield return wait;
                continue;
            }

            Vector3 playerPosition = player.position;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            if (distanceToPlayer <= CLOSED_BOUNDARY)
            {
                Debug.Log("적과 플레이어간의 거리가 5m 이하입니다.");
                Move(this.transform.position - playerPosition);
            }
            else if (distanceToPlayer >= FAR_BOUNDARY)
            {
                Debug.Log("적과 플레이어간의 거리가 20m 이상입니다. 적이 플레이어를 추적합니다.");
                Move(playerPosition - this.transform.position);
            }
            else
            {
                Debug.Log("Enemy is Attack.");
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(AttackRoutine());
                }
            }

            yield return wait;
        }
    }

    protected virtual IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (player == null)
            {
                yield return null;
                continue;
            }

            Vector3 playerPosition = player.position;
            float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
                // 공격 후 1초 대기
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                // 공격 범위를 벗어나면 코루틴 종료
                yield break;
            }
        }
    }

    protected virtual void Move(Vector3 direction)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            Debug.Log("Enemy moves!");
            // direction을 기반으로 NavMeshAgent를 이동시킵니다.
            while(direction.magnitude == 15.0f)
            {
                Debug.Log("Enemy is moving to the player.");
                agent.Move(direction.normalized * agent.speed * Time.deltaTime);
            }
        }
    }
}
