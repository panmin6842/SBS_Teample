using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// 창고에 있는 아이템을 인벤토리로 이동
/// </summary>

public class StorageToInventory : MonoBehaviour
{
    [Header("인벤토리")]
    [SerializeField] private GameObject e_InventorySlotsParent;
    [SerializeField] private GameObject a_InventorySlotsParent;
    [SerializeField] private GameObject p_InventorySlotsParent;
    private InventorySlot[] e_InventorySlots;
    private InventorySlot[] a_InventorySlots;
    private InventorySlot[] p_InventorySlots;

    [SerializeField] private GameObject equipmentSlotsPartent;
    private EquipmentItemSlot[] equipmentSlots;

    [SerializeField] private InventoryMain inventory;
    private SkillPlay skillPlay;
    private PlayerProfile playerProfile;

    private void Awake()
    {
        e_InventorySlots = e_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        a_InventorySlots = a_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        p_InventorySlots = p_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        equipmentSlots = equipmentSlotsPartent.GetComponentsInChildren<EquipmentItemSlot>();
    }

    /// <summary>
    /// 상자에 있는 아이템 인벤토리 창으로 일괄수령
    /// </summary>
    public void GetAll()
    {
        InventorySlot[] allSlots = inventory.GetAllItems();
        int eCount = 0;
        int aCount = 0;
        int pCount = 0;
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].Item != null)
            {
                //중첩 가능하면
                if (allSlots[i].Item.CanOverlap)
                {
                    if (allSlots[i].Item.Type == ItemType.Gold)
                    {
                        int itemCount = allSlots[i].itemCount;
                        for (int g = 0; g <= itemCount; g++)
                        {
                            int random = Random.Range(allSlots[i].Item.MinGold, allSlots[i].Item.MaxGold);
                            GameManager.instance.gold += random;
                            inventory.goldText.text = "Gold : " + GameManager.instance.gold.ToString();
                            allSlots[i].itemCount--;
                            if (allSlots[i].itemCount <= 0)
                            {
                                allSlots[i].ClearSlot();
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < e_InventorySlots.Length; j++)
                        {
                            if (p_InventorySlots[pCount].Item == null && p_InventorySlots[pCount].IsMask(allSlots[i].Item))
                            {
                                p_InventorySlots[pCount].AddItem(allSlots[i].Item, allSlots[i].itemCount);
                                allSlots[i].ClearSlot();
                                pCount++;
                                break;
                            }
                            else if (p_InventorySlots[pCount].Item != null && p_InventorySlots[pCount].IsMask(allSlots[i].Item))
                            {
                                if (p_InventorySlots[pCount].Item.ItemID == allSlots[i].Item.ItemID)
                                {
                                    p_InventorySlots[pCount].UpdateSlotCount(allSlots[i].itemCount);
                                    allSlots[i].ClearSlot();
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (!allSlots[i].Item.CanOverlap)
                {
                    for (int j = 0; j < e_InventorySlots.Length; j++)
                    {
                        if (e_InventorySlots[eCount].Item == null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                        {
                            e_InventorySlots[eCount].AddItem(allSlots[i].Item, 1);
                            allSlots[i].ClearSlot();
                            eCount++;
                            break;
                        }
                        else if (a_InventorySlots[aCount].Item == null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                        {
                            a_InventorySlots[aCount].AddItem(allSlots[i].Item, 1);
                            allSlots[i].ClearSlot();
                            aCount++;
                            break;
                        }

                        if (e_InventorySlots[eCount].Item != null)
                        {
                            eCount++;
                        }
                        if (a_InventorySlots[aCount].Item != null)
                        {
                            aCount++;
                        }
                    }
                }
                GameManager.instance.level++;
                UIManager.Instance.profileLevelText.text = "LV." + GameManager.instance.level.ToString();
                GameManager.instance.itemGetAll = true;
            }
        }
        
    }

    /// <summary>
    /// 인벤토리에 있는 장비 장비창으로 창작 InventorySlot에서 기능
    /// </summary>
    /// <param name="inventorySlot"></param>
    public void Install(InventorySlot inventorySlot)
    {
        Item newItem = null;
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (inventorySlot.Item != null)
            {
                if (equipmentSlots[i].Item != null) //장비 교체
                {
                    if (equipmentSlots[i].IsMask(inventorySlot.Item))
                    {
                        newItem = equipmentSlots[i].Item;
                        equipmentSlots[i].AddItem(inventorySlot.Item);
                        inventorySlot.ClearSlot();
                        inventorySlot.AddItem(newItem);
                        BuffGet(equipmentSlots[i].Item);
                        return;
                    }
                }
                else if (equipmentSlots[i].Item == null) //그대로 장비 착용
                {
                    if (equipmentSlots[i].IsMask(inventorySlot.Item))
                    {
                        equipmentSlots[i].AddItem(inventorySlot.Item);
                        inventorySlot.ClearSlot();
                        BuffGet(equipmentSlots[i].Item);
                        return;
                    }
                }
            }
        }
    }


    public void ReleaseOfEquipment(EquipmentItemSlot slot)
    {
        int eCount = 0;
        int aCount = 0;
        for (int i = 0; i < e_InventorySlots.Length; i++)
        {
            if (e_InventorySlots[eCount].Item == null && e_InventorySlots[eCount].IsMask(slot.Item))
            {
                e_InventorySlots[eCount].AddItem(slot.Item, 1);
                slot.ClearSlot();
                ReleaseBuff(e_InventorySlots[eCount].Item);
                eCount++;
                break;
            }
            else if (a_InventorySlots[aCount].Item == null && a_InventorySlots[aCount].IsMask(slot.Item))
            {
                a_InventorySlots[aCount].AddItem(slot.Item, 1);
                slot.ClearSlot();
                ReleaseBuff(a_InventorySlots[aCount].Item);
                aCount++;
                break;
            }

            if (e_InventorySlots[eCount].Item != null)
            {
                eCount++;
            }
            if (a_InventorySlots[aCount].Item != null)
            {
                aCount++;
            }
        }
    }

    public void BuyProduct(StoreSlot slot)
    {
        if (slot.Item != null)
        {
            InventorySlot[] allitems = inventory.GetAllItems();

            int count = 0;
            for (; count < allitems.Length; ++count)
            {
                //현재 아이템 칸이 null 이면 주울 수 있음
                if (allitems[count].Item == null)
                {
                    allitems[count].AddItem(slot.Item, 1);
                    break;
                }

                //현제 아이템이 null이 아니지만 중첩 가능하면 주울 수 있음
                if (allitems[count].Item.ItemID == slot.Item.ItemID && allitems[count].Item.CanOverlap)
                {
                    allitems[count].UpdateSlotCount(1);
                    break;
                }
            }

            //다 차고 중첩 아니면 못 주움
            if (count == allitems.Length)
            {
                return;
            }

        }
    }

    private void ReleaseBuff(Item item)
    {
        skillPlay = inventory.player.GetComponent<SkillPlay>();
        playerProfile = inventory.player.GetComponent<PlayerProfile>();

        if (item.IsEquipment)
        {
            GameManager.instance.e_atk -= item.AtkBuff;
            GameManager.instance.e_critical -= item.CriticalBuff;
            GameManager.instance.e_def -= item.DefBuff;
            GameManager.instance.e_hp -= item.HpBuff;

            SetBuff();
        }
        if (item.IsAccessory)
        {
            GameManager.instance.a_atk -= item.AtkBuff;
            GameManager.instance.a_hp -= item.HpBuff;
            GameManager.instance.a_mp -= item.MPBuff;
            GameManager.instance.a_critical -= item.CriticalBuff;
            GameManager.instance.a_skillCoolTime = 0;

            SetBuff();
        }
    }

    private void BuffGet(Item item)
    {
        skillPlay = inventory.player.GetComponent<SkillPlay>();
        playerProfile = inventory.player.GetComponent<PlayerProfile>();

        if (item.IsEquipment)
        {
            GameManager.instance.e_atk = item.AtkBuff;
            GameManager.instance.e_critical = item.CriticalBuff;
            GameManager.instance.e_def = item.DefBuff;
            GameManager.instance.e_hp = item.HpBuff;

            SetBuff();
        }
        if (item.IsAccessory)
        {
            GameManager.instance.a_atk = item.AtkBuff;
            GameManager.instance.a_hp = item.HpBuff;
            GameManager.instance.a_mp = item.MPBuff;
            GameManager.instance.a_critical = item.CriticalBuff;
            GameManager.instance.a_skillCoolTime = item.SkillCoolTimeBuff;

            SetBuff();
        }
    }

    private void SetBuff()
    {
        playerProfile.SetMaxHp(GameManager.instance.hpPoint, GameManager.instance.a_hp, GameManager.instance.e_hp);
        playerProfile.SetMaxATK(GameManager.instance.atkPoint, GameManager.instance.a_atk, GameManager.instance.e_atk);
        playerProfile.SetMaxDEF(GameManager.instance.defPoint, GameManager.instance.a_def, GameManager.instance.e_def);
        playerProfile.SetCritical(GameManager.instance.criticalPoint, GameManager.instance.a_critical, GameManager.instance.e_critical);
        playerProfile.SetMaxMp(GameManager.instance.a_mp);
        skillPlay.SetSkillCoolTimeBuff(GameManager.instance.a_skillCoolTime);
    }
}
