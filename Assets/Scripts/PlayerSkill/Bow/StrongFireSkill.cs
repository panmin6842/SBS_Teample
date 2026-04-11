using UnityEngine;

public class StrongFireSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float damage;

    private float stopDist = 25.0f;
    private float moveSpeed = 30.0f;

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
                damage = playerProfile.CriticalBuff(playerProfile.ATK(1600f));
            else
                damage = playerProfile.ATK(1600f);
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
            playerProfile.ShakeCamera(0.2f, 3.0f, 15.0f);
            if (other.CompareTag("Boss"))
            {
                Debug.Log("스킬 : 큰거 한방" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                other.gameObject.GetComponent<BossStatus>().GetDamage(damage);
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("스킬 : 큰거 한방" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
            }
            if (playerProfile.BloodHeal)
            {
                playerProfile.BloodHealHp(10, damage);
            }
        }
        if (other.CompareTag("Wall") || other.CompareTag("Storage"))
        {
            //Destroy(gameObject);
        }
    }
}
