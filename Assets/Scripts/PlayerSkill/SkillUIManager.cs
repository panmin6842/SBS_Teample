using UnityEngine;
using UnityEngine.InputSystem;

public class SkillUIManager : MonoBehaviour
{
    //[Header("인벤토리")]
    //[SerializeField] private GameObject activeSkillSlotParent;
    //[SerializeField] private GameObject passiveSkillSlotParent;
    //private SkillUISlot[] activeSkillSlots;
    public SkillUISlot checkSkillSlot;

    public int slotClickSlot = 0;

    [SerializeField] private GameObject playerInfo;

    private InventoryMain inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryMain>();

        inventory.uiActionMap = inventory.uiInputAction.FindActionMap("Option");
    }

    private void OnEnable()
    {
        inventory.uiActionMap.Enable();
        inventory.uiActionMap.FindAction("OpenInfoUI").performed += OnOpenInfoUI;
    }

    private void OnOpenInfoUI(InputAction.CallbackContext context)
    {
        if (inventory.uiOpen == 0)
        {
            if (!playerInfo.activeSelf)
            {
                playerInfo.SetActive(true);
                inventory.playerProfile.SetActive(false);
                inventory.playerAttack.uiClicking = true;
                inventory.uiOpen = 3;
                Time.timeScale = 0f;
            }
        }
        else if (inventory.uiOpen == 3 && playerInfo.activeSelf)
        {
            Time.timeScale = 1f;
            playerInfo.SetActive(false);
            inventory.playerProfile.SetActive(true);
            inventory.playerAttack.uiClicking = false;
            slotClickSlot = 0;
            inventory.uiOpen = 0;
        }
    }
    public void Install(SkillPick skillSlot)
    {
        if (checkSkillSlot != null)
        {
            if (checkSkillSlot.IsMask(skillSlot.SkillItem))
            {
                checkSkillSlot.AddItem(skillSlot.SkillItem);
            }
            else
            {
                Debug.Log("타입이 다름 장착 불가");
            }
        }
    }
}
