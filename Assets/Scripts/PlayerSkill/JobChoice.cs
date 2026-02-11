using UnityEngine;

public class JobChoice : MonoBehaviour
{
    [Header("직업별 스킬 아이템")]
    [SerializeField] private SkillItem[] swordSkillItems;

    [Header("스킬 슬롯")]
    [SerializeField] private GameObject skillSlotParent;
    private SkillPick[] slots;

    [SerializeField] private int jobChoiceCount = 0; //0 = 무직, 1 = 전사, 2 = 궁수, 3 = 법사
    private bool choice = false; //임시

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slots = skillSlotParent.GetComponentsInChildren<SkillPick>();
    }

    // Update is called once per frame
    void Update()
    {
        //임시로 업데이트에 해 놓는데 만약 선택하는 기획이 나오면 그 때 교체
        if (!choice)
        {
            JobChoiceCheck();
        }
    }

    private void JobChoiceCheck()
    {
        switch (jobChoiceCount)
        {
            case 0:
                break;
            case 1:
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        slots[i].skillItem = swordSkillItems[i];
                    }

                    choice = true;
                }
                break;
        }
    }
}
