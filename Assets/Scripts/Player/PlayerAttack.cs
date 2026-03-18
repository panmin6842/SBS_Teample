using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Job
{
    Sword,
    Bow,
    Stamp
}
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackPos;
    [SerializeField] private GameObject jobChoiceUI;
    [SerializeField] private JobChoice jobChoice;
    [Header("SkillObject")]
    [SerializeField] GameObject swordAttackObj;
    [SerializeField] GameObject bowAttackObj;
    [SerializeField] GameObject stampAttackObj;
    float rotateSpeed = 100f;
    float maxRotateSpeed = 100f;

    Vector3 targetDir;
    float angle;

    float attackTime;
    [SerializeField] private float attackDelay = 1;
    public float attackStartDelay = 0;
    float originattackDelay = 1;
    private float passiveDelay;
    bool attack = false;
    public bool uiClicking = false;
    public float shotDistance;
    private float originShotDistance;
    public float power;
    private float originPower;
    private float passivePower;

    public float scalePercent = 0;

    public bool through = false;
    public bool bowExplosion = false;
    public bool bowPassiveSkill3 = false;
    public bool stampSkill6 = false;
    public bool stampPassiveSkill1 = false;
    public bool stampPassiveSkill2 = false;

    //private int skillCount;

    PlayerProfile playerProfile;

    public Job curJob = Job.Sword;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerProfile = GetComponent<PlayerProfile>();

        //처음은 검사
        StateDecision(1f, 0f, 0f, false, Job.Sword, 10, 3, 0);
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
        attackDelay = passiveDelay * (1f + (changePercent / 100f));
    }

    public void PassiveDelay(float changePercent)
    {
        passiveDelay = originattackDelay * (1f + (changePercent / 100f));
        attackDelay = passiveDelay;
    }

    public void ChangeShotDistance(float changePercent)
    {
        shotDistance = originShotDistance * (1f + (changePercent / 100f));
    }

    public void AttackPosRotation(float changePercent)
    {
        rotateSpeed = maxRotateSpeed * (1f + (changePercent / 100f));
    }

    public void ChangePower(float changePercent)
    {
        power = passivePower * (1f + (changePercent / 100f));
    }

    public void PassivePower(float changePercent)
    {
        passivePower = originPower * (1f + (changePercent / 100f));
        power = passivePower;
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

    //일반 공격 실행
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!attack && !uiClicking)
        {
            switch (curJob)
            {
                case Job.Sword:
                    {
                        Instantiate(swordAttackObj, transform.position, attackPos.transform.rotation);
                        attack = true;
                    }
                    break;
                case Job.Bow:
                    {
                        StartCoroutine(BowAttack());
                    }
                    break;
                case Job.Stamp:
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

    private void StateDecision(float _attackDelay, float _shotDistance, float _power, bool _attack, Job _curJob,
        float setHp, float setATK, float setDEF)
    {
        originattackDelay = _attackDelay;
        attackDelay = originattackDelay;
        passiveDelay = originattackDelay;
        originShotDistance = _shotDistance;
        shotDistance = originShotDistance;
        originPower = _power;
        power = originPower;
        passivePower = originPower;
        attack = _attack;
        curJob = _curJob;
        GameManager.instance.job = _curJob;

        playerProfile.SetMaxHp(setHp, 0);
        playerProfile.SetMaxATK(setATK, 0);
        playerProfile.SetMaxDEF(setDEF, 0);

        GameManager.instance.hpPoint = (int)setHp;
        GameManager.instance.atkPoint = (int)setATK;
        GameManager.instance.defPoint = (int)setDEF;
    }

    public void SwordChoice()
    {
        Debug.Log("swordskill");

        StateDecision(1f, 0f, 0f, false, Job.Sword, 10, 3, 0);
        jobChoiceUI.SetActive(false);
        jobChoice.enabled = true;

    }
    public void BowChoice()
    {
        Debug.Log("bowskill");

        StateDecision(0.5f, 10.0f, 10.0f, false, Job.Bow, 8, 3, -10);
        jobChoiceUI.SetActive(false);
        jobChoice.enabled = true;
    }
    public void StampChoice()
    {
        Debug.Log("stampskill");

        StateDecision(2.5f, 10.0f, 5.0f, false, Job.Stamp, 7, 4, 0);
        jobChoiceUI.SetActive(false);
        jobChoice.enabled = true;
    }
}
