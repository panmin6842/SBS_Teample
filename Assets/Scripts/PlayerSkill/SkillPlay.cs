using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillPlay : MonoBehaviour
{
    private int actSkill1Number = 0;
    private int actSkill2Number = 0;
    private int passiveSkillNumber = 0;

    [SerializeField] private GameObject slotParent;
    private PlayerProfileSkill[] slots;

    [Header("쿨타임")]
    [SerializeField] private Slider[] coolTimeSlider;
    [SerializeField] private float coolTimeSkill1;
    [SerializeField] private float coolTimeSkill2;
    [SerializeField] private float passiveCoolTimeSkill1;
    [SerializeField] private float passiveCoolTimeSkill2;
    [SerializeField] private float curCoolTimeSkill1;
    [SerializeField] private float curCoolTimeSkill2;

    [SerializeField] private bool skill1Start = false;
    [SerializeField] private bool skill2Start = false;

    [Header("칼 오브젝트")]
    [SerializeField] private GameObject sword1;
    [SerializeField] private GameObject sword2;
    [SerializeField] private GameObject sword3;
    [SerializeField] private GameObject sword4;
    [SerializeField] private GameObject sword5;
    [SerializeField] private GameObject sword6;

    [Header("활 오브젝트")]
    [SerializeField] private GameObject bow1;

    PlayerAttack playerAttack;
    PlayerProfile playerProfile;
    [SerializeField] private SwordAttackManager swordAttackManager;
    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerProfile = GetComponent<PlayerProfile>();
    }

    private void Update()
    {
        CoolTime();
    }

    private void CoolTime()
    {
        if (skill1Start)
        {
            if (curCoolTimeSkill1 <= 0)
            {
                skill1Start = false;
                curCoolTimeSkill1 = 0f;
                if (coolTimeSlider != null) coolTimeSlider[0].value = 0f;
            }
            else
            {
                curCoolTimeSkill1 -= Time.deltaTime;
                UpdateSlider(0, curCoolTimeSkill1);
            }
        }

        if (skill2Start)
        {
            if (curCoolTimeSkill2 <= 0)
            {
                skill2Start = false;
                curCoolTimeSkill2 = 0f;
                if (coolTimeSlider != null) coolTimeSlider[1].value = 0f;
            }
            else
            {
                curCoolTimeSkill2 -= Time.deltaTime;
                UpdateSlider(1, curCoolTimeSkill2);
            }
        }
    }

    private void UpdateSlider(int number, float curCollTime)
    {
        if (coolTimeSlider != null)
        {
            coolTimeSlider[number].value = curCollTime;
        }
    }
    public void SkillNumberSetting()
    {
        slots = slotParent.GetComponentsInChildren<PlayerProfileSkill>();
        actSkill1Number = slots[0].choiceNumber;
        actSkill2Number = slots[1].choiceNumber;
        passiveSkillNumber = slots[2].choiceNumber;
        coolTimeSkill1 = slots[0].coolTime;
        coolTimeSkill2 = slots[1].coolTime;
        coolTimeSlider[0].value = 0;
        coolTimeSlider[1].value = 0;

        curCoolTimeSkill1 = coolTimeSkill1;
        curCoolTimeSkill2 = coolTimeSkill2;
    }

    public void OnSkillAttack(InputAction.CallbackContext context)
    {
        if (context.started && actSkill1Number > 0 && !playerProfile.SkillStart)
        {
            if (context.control.name == "1" && slots[0].coolTime > 0)
            {
                if (skill1Start) return;

                skill1Start = true;
                if (passiveCoolTimeSkill1 > 0)
                    curCoolTimeSkill1 = passiveCoolTimeSkill1;
                else
                    curCoolTimeSkill1 = coolTimeSkill1;
                coolTimeSkill1 = slots[0].coolTime;
                coolTimeSlider[0].maxValue = curCoolTimeSkill1;
                coolTimeSlider[0].value = curCoolTimeSkill1;
                playerProfile.SkillStart = true;
                //SwordActiveSkill(actSkill1Number);
                BowActiveSkill(actSkill1Number);
            }

            if (context.control.name == "2" && slots[1].coolTime > 0)
            {
                if (skill2Start) return;

                skill2Start = true;
                if (passiveCoolTimeSkill2 > 0)
                    curCoolTimeSkill2 = passiveCoolTimeSkill2;
                else
                    curCoolTimeSkill2 = coolTimeSkill2;
                coolTimeSkill2 = slots[1].coolTime;
                coolTimeSlider[1].maxValue = curCoolTimeSkill2;
                coolTimeSlider[1].value = curCoolTimeSkill2;
                playerProfile.SkillStart = true;
                //SwordActiveSkill(actSkill2Number);
                BowActiveSkill(actSkill2Number);
            }
        }
    }

    private void SwordActiveSkill(int number)
    {
        switch (number)
        {
            case 1:
                {
                    Instantiate(sword1, transform.position, sword1.transform.rotation);
                }
                break;
            case 2:
                {
                    Instantiate(sword2, transform.position,
                        Quaternion.Euler(0, playerAttack.AttackPos.transform.eulerAngles.y, 0));
                }
                break;
            case 3:
                {
                    Instantiate(sword3, transform.position, sword3.transform.rotation);
                }
                break;
            case 4:
                {
                    Instantiate(sword4, transform.position, Quaternion.Euler(0, playerAttack.AttackPos.transform.eulerAngles.y, 0));
                }
                break;
            case 5:
                {
                    Instantiate(sword5, transform.position, Quaternion.Euler(0, playerAttack.AttackPos.transform.eulerAngles.y, 0));
                }
                break;
            case 6:
                {
                    Instantiate(sword6, transform.position, sword6.transform.rotation);
                }
                break;
        }

    }

    Quaternion rotation;
    private void BowActiveSkill(int number)
    {
        switch (number)
        {
            case 1:
                {
                    rotation = Quaternion.Euler(0, playerAttack.AttackPos.transform.eulerAngles.y, 0);
                    playerProfile.UseMP(1);

                    StartCoroutine(BowSkillRoutine());
                }
                break;
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;
            case 4:
                {

                }
                break;
            case 5:
                {

                }
                break;
            case 6:
                {

                }
                break;
        }
    }
    IEnumerator BowSkillRoutine()
    {
        int count = 0;
        while (count < 5)
        {
            BowSkill1Create(); // 화살 생성
            count++;
            yield return new WaitForSeconds(0.1f); // 0.1초 대기
        }

        // 5번 다 실행 후 종료 로직
        playerProfile.SkillStart = false;
    }
    private void BowSkill1Create()
    {
        Instantiate(bow1, transform.position, rotation);
    }

    public void SwordPassiveSkill()
    {
        switch (passiveSkillNumber)
        {
            case 1:
                {
                    PassiveBuff(30, -30, -30, -20, 0, 0, 0, false, 0, 0);
                }
                break;
            case 2:
                {
                    PassiveBuff(-25, 25, 25, 0, 0, 0, 0, true, 0, 0);
                }
                break;
            case 3:
                {
                    if (actSkill1Number == 6)
                    {
                        PassiveBuff(0, 10, 15, 0, 20, -25, 100, false, 10, 0);
                    }
                    else if (actSkill2Number == 6)
                        PassiveBuff(0, 10, 15, 0, 20, -25, 100, false, 0, 10);
                    else
                        PassiveBuff(0, 10, 15, 0, 20, -25, 100, false, 0, 0);
                }
                break;
        }
    }

    private void PassiveBuff(float passiveDef, float passiveBasicAtk, float passiveAtk, float passiveMoveSpeed,
        float increasedColliderSize, float changeAttackDelay, float coolTimePersent, bool bloodHeal, float coolTimePlue1, float coolTimePlue2)
    {
        playerProfile.PassiveDEF(passiveDef);
        playerProfile.PassiveBasicATK(passiveBasicAtk);
        playerProfile.PassiveATK(passiveAtk);
        playerProfile.PassiveMoveSpeed(passiveMoveSpeed);
        if (swordAttackManager != null)
        {
            swordAttackManager.IncreasedColliderSize(increasedColliderSize);
        }
        playerAttack.ChangeAttackDelay(changeAttackDelay);
        passiveCoolTimeSkill1 = (coolTimeSkill1 * (1f + (coolTimePersent / 100))) + coolTimePlue1;
        passiveCoolTimeSkill2 = (coolTimeSkill2 * (1f + (coolTimePersent / 100))) + coolTimePlue2;
        playerProfile.BloodHeal = bloodHeal;
    }
}
