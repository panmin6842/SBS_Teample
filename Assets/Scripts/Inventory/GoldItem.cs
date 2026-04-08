using UnityEngine;

//[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Gold")]
public class GoldItem : ScriptableObject
{
    [Header("고유한 아이템의 ID(중복불가)")]
    [SerializeField] private int itemID;
    /// <summary>
    /// 아이템의 고유 번호
    /// </summary>
    /// <value></value>
    public int ItemID
    {
        get
        {
            return itemID;
        }
    }

    [Header("아이템 이름")]
    [SerializeField] private string itemName;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    [Header("아이템의 타입")]
    [SerializeField] private ItemType itemType;
    /// <summary>
    /// 아이템의 유형
    /// </summary>
    /// <value></value>
    public ItemType Type
    {
        get
        {
            return itemType;
        }
    }

    [Header("인벤토리에서 보여질 아이템의 이미지")]
    [SerializeField] private Sprite itemImage;
    public Sprite Image
    {
        get
        {
            return itemImage;
        }
    }

    [Header("랜덤 골드 (최소 ~ 최대)")]
    [SerializeField] private int minGold;
    [SerializeField] private int maxGold;
    public int MinGold
    {
        get
        {
            return minGold;
        }
    }
    public int MaxGold
    {
        get
        {
            return maxGold;
        }
    }
}
