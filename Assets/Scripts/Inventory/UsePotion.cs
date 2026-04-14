using UnityEngine;
using UnityEngine.InputSystem;

public class UsePotion : MonoBehaviour
{
    public InputActionAsset uiInputAction;
    public InputActionMap uiActionMap;

    [SerializeField] private PlayerProfilePotion hpSlot;
    [SerializeField] private PlayerProfilePotion mpSlot;

    private PlayerProfile playerProfile;
    void Awake()
    {
        uiActionMap = uiInputAction.FindActionMap("Option");
    }

    private void OnEnable()
    {
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();
        uiActionMap.Enable();
        uiActionMap.FindAction("HpPotion").performed += OnHpPotion;
        uiActionMap.FindAction("MpPotion").performed += OnMpPotion;
    }

    private void OnDisable()
    {
        if (uiActionMap != null)
        {
            uiActionMap.FindAction("HpPotion").performed -= OnHpPotion;
            uiActionMap.FindAction("MpPotion").performed -= OnMpPotion;
            uiActionMap.Disable();
        }
    }

    private void OnHpPotion(InputAction.CallbackContext context)
    {
        if (hpSlot.add)
        {
            playerProfile.HPBuff(hpSlot.slot.Item.Buff);
            hpSlot.Use();
        }
    }

    private void OnMpPotion(InputAction.CallbackContext context)
    {
        if (mpSlot.add)
        {
            playerProfile.MPBuff((int)mpSlot.slot.Item.Buff);
            mpSlot.Use();
        }
    }
}
