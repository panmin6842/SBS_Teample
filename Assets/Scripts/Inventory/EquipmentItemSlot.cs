using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비 아이템 장착 창 슬롯에 관련된 스크립트
/// </summary>
public class EquipmentItemSlot : MonoBehaviour
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

    [Header("아이템 슬롯에 있는 UI 오브젝트")]
    [SerializeField] private Image itemImage;
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
        itemImage.sprite = item.Image;

        SetColor(1);
    }

    //해당 슬롯 하나 삭제
    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        SetColor(0);
    }
}
