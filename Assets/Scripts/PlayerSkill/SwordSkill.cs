using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwordSkill : MonoBehaviour
{
    private int actSkill1Number = 0;
    private int actSkill2Number = 0;
    private int passiveSkillNumber = 0;

    [SerializeField] private GameObject slotParent;
    private PlayerProfileSkill[] slots;

    [Header("ÄðÅ¸ÀÓ")]
    [SerializeField] private Slider[] coolTimeSlider;
    [SerializeField] private float coolTimeSkill1;
    [SerializeField] private float coolTimeSkill2;
    [SerializeField] private float curCoolTimeSkill1;
    [SerializeField] private float curCoolTimeSkill2;

    [SerializeField] private bool skill1Start = false;
    [SerializeField] private bool skill2Start = false;

    [Header("Ä® ¿ÀºêÁ§Æ®")]
    [SerializeField] private GameObject sword1;
    [SerializeField] private GameObject sword2;
    [SerializeField] private GameObject sword3;
    [SerializeField] private GameObject sword4;
    [SerializeField] private GameObject sword5;
    [SerializeField] private GameObject sword6;

    PlayerAttack playerAttack;

    private void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
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
    }

    public void OnSkillAttack(InputAction.CallbackContext context)
    {
        if (context.started && actSkill1Number > 0)
        {
            if (context.control.name == "1" && slots[0].coolTime > 0)
            {
                if (skill1Start) return;

                skill1Start = true;
                curCoolTimeSkill1 = coolTimeSkill1;
                coolTimeSkill1 = slots[0].coolTime;
                coolTimeSlider[0].maxValue = coolTimeSkill1;
                coolTimeSlider[0].value = coolTimeSkill1;
                SwordActiveSkill(actSkill1Number);
            }

            if (context.control.name == "2" && slots[1].coolTime > 0)
            {
                if (skill2Start) return;

                skill2Start = true;
                curCoolTimeSkill2 = coolTimeSkill2;
                coolTimeSkill2 = slots[1].coolTime;
                coolTimeSlider[1].maxValue = coolTimeSkill2;
                coolTimeSlider[1].value = coolTimeSkill2;
                SwordActiveSkill(actSkill2Number);
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
                    Instantiate(sword5, transform.position, sword5.transform.rotation);
                }
                break;
            case 6:
                {
                    Instantiate(sword6, transform.position, sword6.transform.rotation);
                }
                break;
        }
    }
}
