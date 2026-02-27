using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPos;
    [Header("SkillObject")]
    [SerializeField] GameObject swordAttackObj;
    [SerializeField] GameObject bowAttackObj;
    [SerializeField] GameObject stampAttackObj;
    float rotateSpeed = 100f;

    Vector3 targetDir;
    float angle;

    float attackTime;
    [SerializeField] private float attackDelay = 1;
    public float attackStartDelay = 0;
    float originattackDelay = 1;
    bool attack = false;
    public bool uiClicking = false;
    public float shotDistance;
    private float originShotDistance;
    public float power;
    private float originPower;

    public float scalePercent = 0;

    public bool through = false;

    [SerializeField] int skillCount = 1;

    PlayerProfile playerProfile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GetComponent<PlayerProfile>();
    }

    // Update is called once per frame
    void Update()
    {
        AttackArrow();

        if (attack)
        {
            attackTime += Time.deltaTime;

            if (attackTime > attackDelay)
            {
                attack = false;
                attackTime = 0;
            }
        }

    }

    public void ChangeAttackDelay(float changePercent)
    {
        attackDelay = originattackDelay * (1f + (changePercent / 100f));
    }

    public void ChangeShotDistance(float changePercent)
    {
        shotDistance = originShotDistance * (1f + (changePercent / 100f));
    }

    public void ChangePower(float changePercent)
    {
        power = originPower * (1f + (changePercent / 100f));
    }

    void AttackArrow()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            targetDir = hit.point - attackPos.transform.position; //방향 벡터
            angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg; //수평면 상에서 몇 도 방향인지 계산
            Quaternion targetRotation = Quaternion.Euler(90f, 0f, -angle);
            attackPos.transform.localRotation = Quaternion.Slerp(
                attackPos.transform.localRotation,
                targetRotation,
                rotateSpeed * Time.deltaTime);
        }
    }

    public GameObject AttackPos
    {
        get { return attackPos; }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!attack && !uiClicking)
        {
            switch (skillCount)
            {
                case 1:
                    {
                        Instantiate(swordAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
                case 2:
                    {
                        StartCoroutine(BowAttack());
                    }
                    break;
                case 3:
                    {
                        Instantiate(stampAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
            }
        }
    }

    IEnumerator BowAttack()
    {
        attack = true;
        if (attackStartDelay > 0)
        {
            playerProfile.ChangeMoveSpeed(-20);
        }
        yield return new WaitForSeconds(attackStartDelay);
        GameObject newBow;
        Vector3 bowScale;
        newBow = Instantiate(bowAttackObj, transform.position, attackPos.transform.rotation);
        bowScale = newBow.transform.localScale;
        newBow.transform.localScale = bowScale * (1f + (scalePercent / 100f));
        playerProfile.ChangeMoveSpeed(0);
    }

    public void OnSkillChange(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            string pressNumber = context.control.name;

            switch (pressNumber)
            {
                case "z":
                    {
                        Debug.Log("swordskill");
                        originattackDelay = 1;
                        attackDelay = originattackDelay;
                        attack = false;
                        skillCount = 1;
                    }
                    break;
                case "x":
                    {
                        Debug.Log("bowskill");
                        originattackDelay = 0.5f;
                        attackDelay = originattackDelay;
                        originShotDistance = 10;
                        shotDistance = originShotDistance;
                        originPower = 7.0f;
                        power = originPower;
                        attack = false;
                        skillCount = 2;
                    }
                    break;
                case "c":
                    {
                        Debug.Log("stampskill");
                        originattackDelay = 2.5f;
                        attackDelay = originattackDelay;
                        originShotDistance = 10;
                        shotDistance = originShotDistance;
                        originPower = 5.0f;
                        power = originPower;
                        attack = false;
                        skillCount = 3;
                    }
                    break;
            }
        }
    }
}
