using UnityEngine.InputSystem;

public class EquipmentInventory : InventoryBase
{
    public static bool IsInventoryActive = false;

    public InputActionAsset uiInputAction;
    private InputActionMap uiActionMap;

    new private void Awake()
    {
        base.Awake();

        uiActionMap = uiInputAction.FindActionMap("Option");
    }

    private void OnEnable()
    {
        //uiActionMap.Enable();
        //uiActionMap.FindAction("OpenInventory").performed += OnOpenInventory;
    }

    /// <summary>
    /// 장비 장착
    /// </summary>
    public void EquipmentInstall()
    {

    }

    //private void OnOpenInventory(InputAction.CallbackContext context)
    //{
    //    //옵션이 켜져있는 경우 활성화 안 함 나중에 작성

    //    if (IsInventoryActive)
    //    {
    //        inventoryBase.SetActive(false);
    //        IsInventoryActive = false;

    //        //Cursor.visible = false;
    //    }
    //    else
    //    {
    //        inventoryBase.SetActive(true);
    //        IsInventoryActive = true;

    //        Cursor.visible = true;
    //    }
    //}
}
