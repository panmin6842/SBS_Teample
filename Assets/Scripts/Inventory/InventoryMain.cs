using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 여러 아이템을 담을 가장 기본적인 인벤토리
/// </summary>
public class InventoryMain : InventoryBase
{
    public InputActionAsset uiInputAction;
    public InputActionMap uiActionMap;

    public static bool IsInventoryActive = false;

    new void Awake()
    {
        base.Awake();

        uiActionMap = uiInputAction.FindActionMap("Option");
    }

    private void OnEnable()
    {
        uiActionMap.Enable();
        uiActionMap.FindAction("OpenInventory").performed += OnOpenInventory;
    }

    private void OnOpenInventory(InputAction.CallbackContext value)
    {
        //옵션이 켜저있는 경우 활성화 안 함 나중에 작성

        if (!IsInventoryActive)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        inventoryBase.SetActive(true);
        IsInventoryActive = true;

        Cursor.visible = true;
    }

    private void CloseInventory()
    {
        inventoryBase.SetActive(false);
        IsInventoryActive = false;

        //Cursor.visible = false;
    }

    /// <summary>
    /// 특정 아이템 슬롯에 아이템을 등록시킨다
    /// </summary>
    /// <param name="item">어떤 아이템?</param>
    /// <param name="targetSlot">어느 슬롯에?</param>
    /// <param name="count">개수는?></param>

    public void AcquireItem(Item item, InventorySlot targetSlot, int count = 1)
    {
        //중첩 가능하면
        if (item.CanOverlap)
        {
            if (targetSlot.Item != null && targetSlot.IsMask(item))
            {
                if (targetSlot.Item.ItemID == item.ItemID)
                {
                    targetSlot.UpdateSlotCount(count);
                }
            }
        }
        else
        {
            targetSlot.AddItem(item, count);
        }
    }

    public void AcquireItem(Item item, int count = 1)
    {
        //중첩 가능하면
        if (item.CanOverlap)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Item != null && slots[i].IsMask(item))
                {
                    if (slots[i].Item.ItemID == item.ItemID)
                    {
                        slots[i].UpdateSlotCount(count);
                        return;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Item == null && slots[i].IsMask(item))
            {
                slots[i].AddItem(item, count);
                return;
            }
        }
    }

    public InventorySlot[] GetAllItems()
    {
        return slots;
    }
}
