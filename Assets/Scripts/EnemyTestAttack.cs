using UnityEngine;

public class EnemyTestAttack : MonoBehaviour
{
    float power = 7.0f;

    Vector3 startPos;
    Vector3 direction;
    float distance;

    [SerializeField] string hitTag;
    GameObject player;
    void Start()
    {
        startPos = transform.position;
        player = GameObject.FindWithTag(hitTag);

        if (player != null)
        {
            // 핵심: (타겟 좌표 - 내 좌표)를 계산하고 .normalized로 길이를 1로 만듭니다.
            direction = (player.transform.position - transform.position).normalized;

            // 화살이 날아가는 방향을 쳐다보게 만듭니다. (안 하면 옆으로 날아갈 수 있음)
            transform.forward = direction;
        }
        else
        {
            // 플레이어를 못 찾으면 그냥 정면으로 날아가게 예외처리
            direction = transform.forward;
        }
    }

    void Update()
    {
        transform.position += direction * power * Time.deltaTime;

        distance = Vector3.Distance(startPos, transform.position);

        if (distance > 20)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(hitTag))
        {
            Destroy(gameObject);
        }
    }
}
