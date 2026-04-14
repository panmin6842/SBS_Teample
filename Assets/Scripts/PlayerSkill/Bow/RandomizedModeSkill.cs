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

        Invoke("CameraShake", 0.1f);
        Invoke("DestroyObject", 20);
    }

    private void CameraShake()
    {
        playerProfile.ShakeCamera(0.2f, 1.0f, 10.0f);
    }

    private void DestroyObject()
    {
        playerAttack.ChangeAttackDelay(0);
        playerProfile.ChangeBasicATK(0);
        playerProfile.SkillStart = false;
        Destroy(gameObject);
    }
}
