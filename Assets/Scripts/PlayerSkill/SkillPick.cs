using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillPick : MonoBehaviour, IPointerClickHandler
{
    [Header("해당 오브젝트에 할당되는 스킬아이템")]
    [SerializeField] private SkillItem skillItem;

    [Header("테스트 이름")]
    [SerializeField] private TextMeshProUGUI testText;

    [Header("이미지 오브젝트")]
    [SerializeField] private Image slotImage;

    [Header("스킬 해금 버튼")]
    [SerializeField] private Button clearButton;

    [Header("툴 팁 오브젝트")]
    [SerializeField] private GameObject explanToolTip;

    private InventoryMain inventory;
    private SkillUIManager skillUIManager;

    private bool clearSuccess = false;

    /// <summary>
    /// 상호작용 가능한 객체가 가지고 있는 아이템
    /// /// </summary>
    /// <value></value>
    public SkillItem SkillItem
    {
        get
        {
            return skillItem;
        }
    }

    private void OnEnable()
    {
        inventory = GameObject.Find("InventorySystem").GetComponent<InventoryMain>();
        skillUIManager = GameObject.Find("InventorySystem").GetComponent<SkillUIManager>();
        if (skillItem.Type == SkillItemType.Skill_Active)
        {
            testText.text = "ActiveSkill";
        }
        else if (skillItem.Type == SkillItemType.Skill_Passive)
        {
            testText.text = "PassiveSkill";
        }

        slotImage.sprite = skillItem.Image;
        explanToolTip.SetActive(false);
        inventory.slotClick = false;
        SetColor(1);
    }

    // 아이템 이미지의 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = slotImage.color;
        color.a = _alpha;
        slotImage.color = color;
    }

    /// <summary>
    /// 슬롯 클릭하면 설명창이 나옴
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (skillItem != null && !inventory.slotClick)
        {
            explanToolTip.SetActive(true);
            inventory.slotClick = true;
        }
    }

    public void SkillClearSuccess()
    {
        if (!clearSuccess)
        {
            clearButton.GetComponentInChildren<TextMeshProUGUI>().text = "Install";
            clearSuccess = true;
        }
        else if (clearSuccess && skillUIManager != null)
        {
            skillUIManager.Install(this);
            explanToolTip.SetActive(false);
            inventory.slotClick = false;
        }
    }

    /// <summary>
    /// 설명창 닫기
    /// </summary>
    public void SkillExplainToolTipClose()
    {
        explanToolTip.SetActive(false);
        inventory.slotClick = false;
    }
}
