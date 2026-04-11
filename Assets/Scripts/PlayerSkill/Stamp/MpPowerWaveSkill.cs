using System.Collections;
using UnityEngine;

public class MpPowerWaveSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>();

        if (playerProfile != null)
        {
            playerProfile.UseMP(1);
            bool critical = playerProfile.CriticalProbability();
            if (critical)
                damage = playerProfile.CriticalBuff(playerProfile.ATK(300f));
            else
                damage = playerProfile.ATK(300f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyObject", 1);
    }

    private void DestroyObject()
    {
        playerProfile.SkillStart = false;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            if (other.CompareTag("Boss"))
            {
                Debug.Log("스킬 : 마력 파동" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                other.gameObject.GetComponent<BossStatus>().GetDamage(damage);
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("스킬 : 마력 파동" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                StartCoroutine(NuckBack(other.GetComponent<Rigidbody>(), other));
                //기절도 있으야함
            }

            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
    }

    IEnumerator NuckBack(Rigidbody enemyRb, Collider enemy)
    {
        enemyRb.linearVelocity = Vector3.zero;
        Vector3 dist = enemy.transform.position - transform.position;
        enemyRb.AddForce(dist * 5f, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        enemyRb.linearVelocity = Vector3.zero;
    }
}
