using UnityEngine;

/// <summary>
/// 창고에 있는 아이템을 인벤토리로 이동
/// </summary>

public class StorageToInventory : MonoBehaviour
{
    [Header("인벤토리")]
    [SerializeField] private GameObject e_InventorySlotsParent;
    [SerializeField] private GameObject a_InventorySlotsParent;
    private InventorySlot[] e_InventorySlots;
    private InventorySlot[] a_InventorySlots;

    [SerializeField] private GameObject equipmentSlotsPartent;
    private EquipmentItemSlot[] equipmentSlots;

    [SerializeField] private InventoryMain inventory;
    PlayerState playerState;

    private void Awake()
    {
        e_InventorySlots = e_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        a_InventorySlots = a_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
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
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].Item != null)
            {
                //중첩 가능하면
                if (allSlots[i].Item.CanOverlap)
                {
                    for (int j = 0; j < e_InventorySlots.Length; j++)
                    {
                        if (e_InventorySlots[eCount].Item == null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                        {
                            e_InventorySlots[eCount].AddItem(allSlots[i].Item, allSlots[i].itemCount);
                            allSlots[i].ClearSlot();
                            eCount++;
                            break;
                        }
                        else if (e_InventorySlots[eCount].Item != null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                        {
                            if (e_InventorySlots[eCount].Item.ItemID == allSlots[i].Item.ItemID)
                            {
                                e_InventorySlots[eCount].UpdateSlotCount(allSlots[i].itemCount);
                                allSlots[i].ClearSlot();
                                break;
                            }
                        }

                        if (a_InventorySlots[aCount].Item == null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                        {
                            a_InventorySlots[aCount].AddItem(allSlots[i].Item, allSlots[i].itemCount);
                            allSlots[i].ClearSlot();
                            aCount++;
                            break;
                        }
                        else if (a_InventorySlots[aCount].Item != null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                        {
                            if (a_InventorySlots[aCount].Item.ItemID == allSlots[i].Item.ItemID)
                            {
                                a_InventorySlots[aCount].UpdateSlotCount(allSlots[i].itemCount);
                                allSlots[i].ClearSlot();
                                break;
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

    private void BuffGet(Item item)
    {
        playerState = inventory.player.GetComponent<PlayerState>();
        if (item.Type == ItemType.Equipment_HEAD)
        {

        }
        else if (item.Type == ItemType.Equipment_TOP)
        {

        }
        else if (item.Type == ItemType.Equipment_BOTTOMS)
        {

        }
        else if (item.Type == ItemType.Equipment_SHOES)
        {

        }
        else if (item.Type == ItemType.Equipment_PENDANT)
        {

        }
        else if (item.Type == ItemType.Equipment_RING)
        {

        }
        else if (item.Type == ItemType.Equipment_AMULET)
        {

        }
        else if (item.Type == ItemType.Equipment_WEAPON)
        {

        }
    }
}
