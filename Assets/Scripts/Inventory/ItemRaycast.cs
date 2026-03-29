using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 씬 내의 아이템(또는 정적 물체)에 다가가면 해당 아이템을 줍거나, 상호작용 할 수 있도록 해주는 스크립트
/// </summary>

public class ItemRaycast : MonoBehaviour
{
    private bool isStorageActive = false; //상자를 열수 있나?
    private bool isStoreActive = false;
    private ItemPickUp currentItem; //활성화시 현재 등록된 아이템

    [Header("상자 인벤토리")]
    private GameObject storageInventory;
    [Header("상점 창")]
    [SerializeField] private GameObject storeWindow;

    private InventoryMain inventory;

    private PlayerAttack playerAttack;

    private void Awake()
    {

    }

    private void Start()
    {
        inventory = UIManager.Instance.inventory;

        playerAttack = GetComponent<PlayerAttack>();
        if (inventory == null)
        {
            GameObject obj = GameObject.Find("InventorySystem");
            if (obj != null) inventory = obj.GetComponent<InventoryMain>();
        }

        if (inventory != null && inventory.uiInputAction != null)
        {
            inventory.uiActionMap = inventory.uiInputAction.FindActionMap("Option");
            inventory.uiActionMap.Enable();
            inventory.uiActionMap.FindAction("OpenStorage").performed += OnOpenStorage;
        }

        if (UIManager.Instance != null)
        {
            storageInventory = UIManager.Instance.storageInventroy;
        }

        if (UIManager.Instance == null)
        {
            Debug.Log("instance없음");
        }
    }

    private void OnDisable()
    {
        if (inventory != null && inventory.uiActionMap != null)
        {
            inventory.uiActionMap.FindAction("OpenStorage").performed -= OnOpenStorage;
            inventory.uiActionMap.Disable();
        }
    }

    private void ItemGet()
    {
        if (currentItem.Item.Type > ItemType.NONE)
        {
            InventorySlot[] allitems = inventory.GetAllItems();

            int count = 0;
            for (; count < allitems.Length; ++count)
            {
                //현재 아이템 칸이 null 이면 주울 수 있음
                if (allitems[count].Item == null) { TryPickUp(); break; }

                //현제 아이템이 null이 아니지만 중첩 가능하면 주울 수 있음
                if (allitems[count].Item.ItemID == currentItem.Item.ItemID && allitems[count].Item.CanOverlap) { TryPickUp(); break; }
            }

            //다 차고 중첩 아니면 못 주움
            if (count == allitems.Length) { return; }

            //줍는 효과음
        }
    }

    private void OnOpenStorage(InputAction.CallbackContext context)
    {
        if (isStorageActive)
        {
            if (inventory.currentUI == UIType.None)
            {
                storageInventory.SetActive(true);
                inventory.playerProfile.SetActive(false);
                playerAttack.uiClicking = true;
                inventory.currentUI = UIType.Chest;
                Time.timeScale = 0f;
            }
            else if (inventory.currentUI == UIType.Chest)
            {
                Time.timeScale = 1f;
                storageInventory.SetActive(false);
                inventory.playerProfile.SetActive(true);
                playerAttack.uiClicking = false;
                inventory.currentUI = UIType.None;
                isStorageActive = false;
            }
        }

        if (isStoreActive)
        {
            if (inventory.currentUI == UIType.None)
            {
                storeWindow.SetActive(true);
                inventory.playerProfile.SetActive(false);
                playerAttack.uiClicking = true;
                inventory.currentUI = UIType.Store;
                Time.timeScale = 0f;
            }
            else if (inventory.currentUI == UIType.Store)
            {
                Time.timeScale = 1f;
                storeWindow.SetActive(false);
                inventory.playerProfile.SetActive(true);
                playerAttack.uiClicking = false;
                inventory.currentUI = UIType.None;
                isStoreActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            currentItem = other.gameObject.transform.GetComponent<ItemPickUp>();

            ItemGet();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Storage" && !isStorageActive && inventory.currentUI == UIType.None)
        {
            isStorageActive = true;
        }

        if (other.tag == "Store" && !isStoreActive && inventory.currentUI == UIType.None)
        {
            isStoreActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Storage")
        {
            isStorageActive = false;
        }

        if (other.tag == "Store")
        {
            isStoreActive = false;
        }
    }

    /// <summary>
    /// 아이템 습득
    /// </summary>
    private void TryPickUp()
    {

        if (currentItem.Item.Type != ItemType.NONE)
        {
            inventory.AcquireItem(currentItem.Item);
            Destroy(currentItem.gameObject);
        }
    }
}
