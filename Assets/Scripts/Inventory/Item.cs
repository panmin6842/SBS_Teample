using UnityEngine;

[System.Flags]
public enum ItemType
{
    //NONE Type는 인벤토리에 들어오지 않는다
    NONE = 0, //0
    SKILL = 1 << 0, //1

    //장비 아이템
    Equipment_HEAD = 1 << 1, //2
    Equipment_TOP = 1 << 2, //4
    Equipment_BOTTOMS = 1 << 3, //8
    Equipment_SHOES = 1 << 4, //16
    Equipment_PENDANT = 1 << 5, //32
    Equipment_RING = 1 << 6, //64
    Equipment_AMULET = 1 << 7, //128
    Equipment_WEAPON = 1 << 8, //256
    Equipment_CHARM = 1 << 9,

    //물약 아이템
}

[CreateAssetMenu(fileName = "Item", menuName = "Add Item/Item")]
public class Item : ScriptableObject
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

    [Header("아이템의 중첩 가능한가?")]
    [SerializeField] private bool canOverlap;
    /// <summary>
    /// 아이템이 중첩 가능한가?
    /// </summary>
    /// <value></value>
    public bool CanOverlap
    {
        get
        {
            return canOverlap;
        }
    }

    [Header("사용(상호작용)이 가능한 아이템인가?")]
    [SerializeField] private bool isInteractivity;
    /// <summary>
    /// 사용(상호작용)이 가능한 아이템인가?
    /// </summary>
    /// <value></value>
    public bool IsInteractivity
    {
        get
        {
            return isInteractivity;
        }
    }

    [Header("아이템을 사용하면 사라지는가?")]
    [SerializeField] private bool isConsumable;
    /// <summary>
    /// 아이템을 사용하면 한개씩 사라지는가?
    /// </summary>
    /// <value></value>
    public bool IsConsumable
    {
        get
        {
            return isConsumable;
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

    [Header("아이템 설명")]
    [SerializeField] private string explanation;

    public string Explanation
    {
        get
        {
            return explanation;
        }
    }

    [Header("효과")]
    [SerializeField] float buff;

    public float Buff
    {
        get
        {
            return buff;
        }
    }
}
