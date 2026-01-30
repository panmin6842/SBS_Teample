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

    [SerializeField] private InventoryMain inventory;

    private void Awake()
    {
        e_InventorySlots = e_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        a_InventorySlots = a_InventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }
    public void GetAll()
    {
        InventorySlot[] allSlots = inventory.GetAllItems();
        int eCount = 0;
        int aCount = 0;
        for (int i = 0; i < allSlots.Length; i++)
        {
            //중첩 가능하면
            if (allSlots[i].Item != null)
            {
                if (allSlots[i].Item.CanOverlap)
                {
                    if (e_InventorySlots[eCount].Item == null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                    {
                        e_InventorySlots[eCount].AddItem(allSlots[i].Item, allSlots[i].itemCount);
                        allSlots[i].ClearSlot();
                        eCount++;
                    }
                    else if (e_InventorySlots[eCount].Item != null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                    {
                        if (e_InventorySlots[eCount].Item.ItemID == allSlots[i].Item.ItemID)
                        {
                            e_InventorySlots[eCount].UpdateSlotCount(allSlots[i].itemCount);
                            allSlots[i].ClearSlot();
                        }
                    }

                    if (a_InventorySlots[aCount].Item == null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                    {
                        a_InventorySlots[aCount].AddItem(allSlots[i].Item, allSlots[i].itemCount);
                        allSlots[i].ClearSlot();
                        aCount++;
                    }
                    else if (a_InventorySlots[aCount].Item != null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                    {
                        if (a_InventorySlots[aCount].Item.ItemID == allSlots[i].Item.ItemID)
                        {
                            a_InventorySlots[aCount].UpdateSlotCount(allSlots[i].itemCount);
                            allSlots[i].ClearSlot();
                        }
                    }
                }
                else
                {
                    if (e_InventorySlots[eCount].Item == null && e_InventorySlots[eCount].IsMask(allSlots[i].Item))
                    {
                        e_InventorySlots[eCount].AddItem(allSlots[i].Item, 1);
                        allSlots[i].ClearSlot();
                        eCount++;
                    }
                    else if (a_InventorySlots[aCount].Item == null && a_InventorySlots[aCount].IsMask(allSlots[i].Item))
                    {
                        a_InventorySlots[aCount].AddItem(allSlots[i].Item, 1);
                        allSlots[i].ClearSlot();
                        aCount++;
                    }
                }
            }
        }
    }
}
