using UnityEngine;

public class BowExplosionSkill : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();

        if (playerProfile != null)
        {
            damage = playerProfile.BasicATK(60);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("스킬 : 활 폭발" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
    }
}
