using TMPro;
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

    [SerializeField] private TextMeshProUGUI skillPointText;
    private int skillPointCount = 9;


    [SerializeField] private GameObject playerInfo;

    [Header("엑티브 스킬 슬롯")]
    [SerializeField] private SkillUISlot[] slots;

    private InventoryMain inventory;
    private SkillPlay skillPlay;

    private void Awake()
    {
        inventory = GetComponent<InventoryMain>();

        inventory.uiActionMap = inventory.uiInputAction.FindActionMap("Option");
        skillPlay = inventory.player.GetComponent<SkillPlay>();
    }

    private void OnEnable()
    {
        inventory.uiActionMap.Enable();
        inventory.uiActionMap.FindAction("OpenInfoUI").performed += OnOpenInfoUI;
    }

    public void SkillPointUse()
    {
        skillPointCount--;
        skillPointText.text = "SkillPoint : " + skillPointCount;
    }

    public int SkillPointCount()
    {
        return skillPointCount;
    }

    private void OnOpenInfoUI(InputAction.CallbackContext context)
    {
        if (inventory.currentUI == UIType.None)
        {
            if (!playerInfo.activeSelf)
            {
                playerInfo.SetActive(true);
                inventory.playerProfile.SetActive(false);
                inventory.playerAttack.uiClicking = true;
                skillPointText.text = "SkillPoint : " + skillPointCount;
                inventory.currentUI = UIType.SkillWindow;
                Time.timeScale = 0f;
            }
        }
        else if (inventory.currentUI == UIType.SkillWindow && playerInfo.activeSelf)
        {
            Time.timeScale = 1f;
            playerInfo.SetActive(false);
            inventory.playerProfile.SetActive(true);
            inventory.playerAttack.uiClicking = false;
            slotClickSlot = 0;
            inventory.currentUI = UIType.None;
        }
    }

    /// <summary>
    /// 스킬 장착
    /// </summary>
    /// <param name="skillSlot"></param>
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

    /// <summary>
    /// 이미 존재하는지 확인
    /// </summary>
    /// <param name="skillSlot"></param>
    /// <returns></returns>
    public bool InstallPossibility(SkillPick skillSlot)
    {
        if (slots[0].SkillItem == skillSlot.skillItem || slots[1].SkillItem == skillSlot.skillItem)
        {
            return true;
        }
        return false;
    }
}
