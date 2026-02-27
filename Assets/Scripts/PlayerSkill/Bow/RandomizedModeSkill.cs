using UnityEngine;

public class RandomizedModeSkill : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();

        if (playerAttack != null)
        {
            playerAttack.ChangeAttackDelay(-35f);
            playerProfile.ChangeBasicATK(-30f);
            playerProfile.UseMP(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyObject", 20);
    }

    private void DestroyObject()
    {
        playerAttack.ChangeAttackDelay(0);
        playerProfile.ChangeBasicATK(0);
        playerProfile.SkillStart = false;
        Destroy(gameObject);
    }
}
