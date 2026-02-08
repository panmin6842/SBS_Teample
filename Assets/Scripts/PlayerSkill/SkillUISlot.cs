using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillUISlot : MonoBehaviour, IPointerClickHandler
{
    private SkillItem skillItem;
    public SkillItem SkillItem
    {
        get
        {
            return skillItem;
        }
    }

    [Header("해당 슬롯에 어떤 타입이 들어올 수 있는가")]
    [SerializeField] private SkillItemType slotMask;

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image itemImage;

    [Header("아이템 이름 텍스트")]
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private string firstName;

    [Header("슬롯 클릭 ID")]
    [SerializeField] private int slotClickID;

    [Header("클릭 시 표시 오브젝트")]
    [SerializeField] private GameObject clickImage;

    private SkillUIManager skillUIManager;

    private void OnEnable()
    {
        skillUIManager = GameObject.Find("InventorySystem").GetComponent<SkillUIManager>();
    }
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    /// <summary>
    /// slotMask에서 설정된 값에 따라 비트연산을한다.
    /// 현재 마스크값이 비트연산으로 0이 나온다면 현재 슬롯에 마스크가 일치하지 않는다는 뜻.
    /// 0이 아닌 수는 현재 비트위치(10진수로 1, 2, 4, 8)로 값이 나온다.
    /// </summary>
    public bool IsMask(SkillItem item)
    {
        return ((int)item.Type & (int)slotMask) == 0 ? false : true;
    }

    //인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(SkillItem nItem, int count = 1)
    {
        skillItem = nItem;
        itemImage.sprite = skillItem.Image;
        itemText.text = skillItem.ItemName;

        SetColor(1);
    }

    //해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        skillItem = null;
        itemImage.sprite = null;
        itemText.text = firstName;
        SetColor(0);
    }

    private void Update()
    {
        if (skillUIManager.slotClickSlot != slotClickID)/// 다른 슬롯을 클릭 하거나 클릭을 취소할 경우
        {
            clickImage.SetActive(false);
        }
    }

    /// <summary>
    /// 슬롯 클릭 했을 경우 표시
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (skillUIManager != null)
        {
            if (skillUIManager.slotClickSlot == slotClickID)
            {
                skillUIManager.slotClickSlot = 0;
                skillUIManager.checkSkillSlot = null;
            }
            else if (skillUIManager.slotClickSlot != slotClickID)
            {
                skillUIManager.slotClickSlot = slotClickID;
                clickImage.SetActive(true);
                skillUIManager.checkSkillSlot = this;
            }
        }
    }
}
