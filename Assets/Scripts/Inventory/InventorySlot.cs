using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 슬롯 하나를 담당
/// </summary>

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private Item item;
    public Item Item
    {
        get
        {
            return item;
        }
    }

    [Header("해당 슬롯에 어떤 타입이 들어올 수 있는가")]
    [SerializeField] private ItemType slotMask;

    public int itemCount; //획득한 아이템 개수

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image itemImage;
    [SerializeField] private GameObject explanToolTip;
    [SerializeField] TextMeshProUGUI textCount;
    [SerializeField] TextMeshProUGUI explanText;

    // 아이템 이미지의 투명도 조절
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
    public bool IsMask(Item item)
    {
        return ((int)item.Type & (int)slotMask) == 0 ? false : true;
    }

    //인벤토리에 새로운 아이템 슬롯 추가
    public void AddItem(Item nItem, int count = 1)
    {
        item = nItem;
        itemCount = count;
        itemImage.sprite = item.Image;
        if (explanText != null)
        {
            explanText.text = item.Explanation;
        }

        if (item.Type <= ItemType.Equipment_WEAPON)
        {
            textCount.text = "";
        }
        else
        {
            textCount.text = itemCount.ToString();
        }

        SetColor(1);
    }

    //해당 슬롯의 아이템 개수 업데이트
    public void UpdateSlotCount(int count)
    {
        itemCount += count;
        textCount.text = itemCount.ToString();

        if (itemCount == 0)
        {
            ClearSlot();
        }
    }

    //해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        textCount.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            explanToolTip.SetActive(true);
        }
    }
}
