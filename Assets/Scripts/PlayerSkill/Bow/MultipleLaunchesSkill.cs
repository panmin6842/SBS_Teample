using UnityEngine;

public class MultipleLaunchesSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float stopDist = 15.0f;
    private float moveSpeed = 10.0f;

    private float damage1;
    private float damage2;

    private int enemyHitCount = 0;

    private Vector3 firstPos;
    private float dist;

    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        firstPos = transform.position;

        Attack();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        dist = Vector3.Distance(firstPos, transform.position);

        if (dist >= stopDist)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            enemyHitCount++;
            if (enemyHitCount == 1)
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                playerProfile.BowSkillHit(hitPoint);
                if (other.CompareTag("Boss"))
                {
                    Debug.Log("스킬 : 다중 발사" + other.gameObject.name + "을(를) 공격했습니다!" + "damage1 = " + damage1);
                    other.gameObject.GetComponent<BossStatus>().GetDamage(damage1);
                }
                else if (other.CompareTag("Enemy"))
                {
                    Debug.Log("스킬 : 다중 발사" + other.gameObject.name + "을(를) 공격했습니다!" + "damage1 = " + damage1);
                }
                if (playerProfile.BloodHeal)
                {
                    playerProfile.BloodHealHp(10, damage1);
                }
            }
            else if (enemyHitCount == 2)
            {
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                playerProfile.BowSkillHit(hitPoint);
                if (other.CompareTag("Boss"))
                {
                    Debug.Log("스킬 : 다중 발사" + other.gameObject.name + "을(를) 공격했습니다!" + "damage2 = " + damage2);
                    other.gameObject.GetComponent<BossStatus>().GetDamage(damage2);
                }
                else if (other.CompareTag("Enemy"))
                {
                    Debug.Log("스킬 : 다중 발사" + other.gameObject.name + "을(를) 공격했습니다!" + "damage2 = " + damage2);
                }
                if (playerProfile.BloodHeal)
                {
                    playerProfile.BloodHealHp(10, damage2);
                }
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Wall") || other.CompareTag("Storage"))
        {
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        if (playerProfile != null)
        {
            bool critical = playerProfile.CriticalProbability();
            if (critical)
            {
                damage1 = playerProfile.CriticalBuff(playerProfile.ATK(100f));
                damage2 = playerProfile.CriticalBuff(playerProfile.ATK(70f));
            }
            else
            {
                damage1 = playerProfile.ATK(100f);
                damage2 = playerProfile.ATK(70f);
            }
        }
    }
}
