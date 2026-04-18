using UnityEngine;

public class SnipingPostureSkill : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerProfile playerProfile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        playerProfile = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProfile>();

        if (playerAttack != null)
        {
            playerAttack.ChangeAttackDelay(200f);
            playerAttack.ChangeShotDistance(200);
            playerProfile.ChangeBasicATK(200f);
            playerAttack.ChangePower(150);
            playerAttack.through = true;
            playerAttack.scalePercent = 10;
            playerAttack.attackStartDelay = 0.3f;
            playerProfile.UseMP(1);
        }
        Invoke("DestroyObject", 20);
        Invoke("CameraShake", 0.1f);
    }

    private void CameraShake()
    {
        playerProfile.ShakeCamera(0.2f, 1.0f, 10.0f);
    }

    private void DestroyObject()
    {
        playerAttack.ChangeAttackDelay(0);
        playerProfile.ChangeBasicATK(0);
        playerAttack.ChangeShotDistance(0);
        playerAttack.ChangePower(0);
        playerAttack.through = false;
        playerProfile.SkillStart = false;
        playerAttack.scalePercent = 0;
        playerAttack.attackStartDelay = 0;
        Destroy(gameObject);
    }
}
