using UnityEngine;

/// <summary>
/// 인벤토리 슬롯들을 등록 시키고 사용할 준비를 완료한다
/// 추상클라스로 작성하여 인벤토리 베이스 자체적으로 인스턴스 할 수 없게 한다
/// </summary>

public class InventoryBase : MonoBehaviour
{
    [SerializeField] protected GameObject inventoryBase;
    [SerializeField] protected GameObject inventorySlotsParent;

    protected InventorySlot[] slots;

    /// <summary>
    /// 인벤토리 베이스 초기화
    /// </summary>
    protected void Awake()
    {
        if (inventoryBase.activeSelf)
        {
            inventoryBase.SetActive(false);
        }

        slots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
    }
}
