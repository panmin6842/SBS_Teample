using UnityEngine;

public class ReleaseOfSwordSkill : MonoBehaviour
{
    [SerializeField] private Vector3 boxSize;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Vector3 center;
    PlayerProfile playerProfile;

    private float moveSpeed = 10.0f;

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
                damage = playerProfile.CriticalBuff(playerProfile.ATK(500f));
            else
                damage = playerProfile.ATK(500f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") || other.CompareTag("Storage"))
        {
            playerProfile.SkillStart = false;
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            playerProfile.SwordSkillHit(hitPoint);
            if (other.CompareTag("Boss"))
            {
                Debug.Log("스킬 : 검기방출" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                other.gameObject.GetComponent<BossStatus>().GetDamage(damage);
            }
            else if (other.CompareTag("Enemy"))
            {
                Debug.Log("스킬 : 검기방출" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
                playerProfile.EnemyAttack(other, damage);
            }
            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
            //적 hp 감소
        }
    }
}
