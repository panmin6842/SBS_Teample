using UnityEngine;

public class HorsePowerWaveSkill : MonoBehaviour
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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("스킬 : 마력 파동" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);

            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
    }
}
