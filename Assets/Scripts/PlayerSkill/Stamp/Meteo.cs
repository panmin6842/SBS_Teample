using UnityEngine;

public class Meteo : MonoBehaviour
{
    private PlayerProfile playerProfile;

    private float damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GameObject.FindWithTag("Player").GetComponent<PlayerProfile>(); ;

        if (playerProfile != null)
        {
            damage = playerProfile.ATK(900);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("스킬 : 메테오" + other.gameObject.name + "을(를) 공격했습니다!" + "damage = " + damage);
            playerProfile.SkillStart = false;
            Destroy(gameObject);
            if (playerProfile.BloodHeal)
                playerProfile.BloodHealHp(10, damage);
        }
        if (other.CompareTag("Place"))
        {
            playerProfile.SkillStart = false;
            Destroy(gameObject);
        }
    }
}
