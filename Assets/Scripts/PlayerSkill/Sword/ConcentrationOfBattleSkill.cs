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
            damage = playerProfile.ATK(55f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        dist = Vector3.Distance(firstPos, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("스킬 : 전투 집중" + other.gameObject.name + "을(를) 공격했습니다!");
            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
