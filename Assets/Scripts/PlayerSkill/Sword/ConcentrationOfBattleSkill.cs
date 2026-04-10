using UnityEngine;

public class ConcentrationOfBattleSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float stopDist = 7.0f;
    private float moveSpeed = 10.0f;

    private float damage;

    private Vector3 firstPos;
    private float dist;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();
        firstPos = transform.position;

        if (playerProfile != null)
        {
            bool critical = playerProfile.CriticalProbability();
            if (critical)
                damage = playerProfile.CriticalBuff(playerProfile.ATK(55f));
            else
                damage = playerProfile.ATK(55f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        dist = Vector3.Distance(firstPos, transform.position);

        if (dist >= stopDist)
        {
            playerProfile.SkillStart = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if (other.CompareTag("Boss"))
            {
                Debug.Log("스킬 : 전투 집중" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                other.gameObject.GetComponent<BossStatus>().GetDamage(damage);
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("스킬 : 전투 집중" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
            }
            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
        if (other.CompareTag("Wall") || other.CompareTag("Storage"))
        {
            playerProfile.SkillStart = false;
            Destroy(gameObject);
        }
    }
}
