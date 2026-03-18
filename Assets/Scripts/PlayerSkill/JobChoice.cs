using UnityEngine;

public class JobChoice : MonoBehaviour
{
    private PlayerAttack playerAttack;

    [Header("직업별 스킬 아이템")]
    [SerializeField] private SkillItem[] swordSkillItems;
    [SerializeField] private SkillItem[] bowSkillItems;
    [SerializeField] private SkillItem[] stampSkillItems;

    [Header("스킬 슬롯")]
    [SerializeField] private GameObject skillSlotParent;
    private SkillPick[] slots;

    private void OnEnable()
    {
        slots = skillSlotParent.GetComponentsInChildren<SkillPick>();
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();

        JobChoiceCheck();
    }

    private void JobChoiceCheck()
    {
        switch (playerAttack.curJob)
        {
            case Job.Sword:
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        slots[i].skillItem = swordSkillItems[i];
                    }
                }
                break;
            case Job.Bow:
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        slots[i].skillItem = bowSkillItems[i];
                    }
                }
                break;
            case Job.Stamp:
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        slots[i].skillItem = stampSkillItems[i];
                    }
                }
                break;
        }
    }
}
