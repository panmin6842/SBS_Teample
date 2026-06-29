using UnityEngine;

public class Attack4Manager : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float stopDist = 15.0f;
    private float moveSpeed = 5.0f;

    int damage;

    private Vector3 firstPos;
    private float dist;

    private BossStatus bossStatus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        firstPos = transform.position;
        bossStatus = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossStatus>();

        if (playerProfile != null)
        {
            damage = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        dist = Vector3.Distance(firstPos, transform.position);

        if (dist >= stopDist || bossStatus.curHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("스킬 : 4" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
            damage = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossStatus>().atk;
            playerProfile.GetDamage(damage);
            Destroy(gameObject);
        }
    }
}
