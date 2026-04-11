using UnityEngine;

public class HorsePowerUpSkill : MonoBehaviour
{
    PlayerAttack playerAttack;
    PlayerProfile playerProfile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();

        if (playerAttack != null)
        {
            playerAttack.ChangeAttackDelay(-40f); //우선 기본 수치에서 1초 감소하도록 함
            playerAttack.ChangePower(30);
        }

        Invoke("CameraShake", 0.1f);
    }

    private void CameraShake()
    {
        playerProfile.ShakeCamera(0.2f, 1.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("ObjDestroy", 20);
    }

    private void ObjDestroy()
    {
        playerAttack.ChangeAttackDelay(0);
        playerAttack.ChangePower(0);
        playerAttack.stampSkill6 = false;
        playerProfile.SkillStart = false;
        Destroy(gameObject);
    }
}
