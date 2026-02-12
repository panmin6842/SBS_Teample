using UnityEngine;
public enum SkillItemType
{
    //스킬
    Skill_Active = 1 << 10,
    Skill_Passive = 1 << 11,
}

[CreateAssetMenu(fileName = "SkillItem", menuName = "Add Item/SkillItem")]
public class SkillItem : ScriptableObject
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

    [Header("스킬아이템의 타입")]
    [SerializeField] private SkillItemType skillItemType;
    /// <summary>
    /// 아이템의 유형
    /// </summary>
    /// <value></value>
    public SkillItemType Type
    {
        get
        {
            return skillItemType;
        }
    }

    [Header("인벤토리에서 보여질 스킬아이템의 이름")]
    [SerializeField] private string itemName;
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    [Header("인벤토리에서 보여질 스킬아이템의 이미지")]
    [SerializeField] private Sprite itemImage;
    public Sprite Image
    {
        get
        {
            return itemImage;
        }
    }

    [Header("스킬아이템 설명")]
    [SerializeField] private string explanation;

    public string Explanation
    {
        get
        {
            return explanation;
        }
    }

    [Header("스킬 번호")]
    [SerializeField] int number;

    public int Number
    {
        get
        {
            return number;
        }
    }

    [Header("쿨타임")]
    [SerializeField] float coolTime;

    public float CoolTime
    {
        get
        {
            return coolTime;
        }
    }
}
