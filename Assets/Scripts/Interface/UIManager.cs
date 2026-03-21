using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject jobChoiceUI;
    public JobChoice jobChoice;
    public GameObject storageInventroy;
    public InventoryMain inventory;

    [Header("HP관련 오브젝트")]
    public Image hpBackground;
    public Image hpMask;
    public TextMeshProUGUI hpText;
    [Header("MP관련 오브젝트")]
    public Image mpBackground;
    public Image mpMask;
    public TextMeshProUGUI mpText;
    [Header("행동력 관련 오브젝트")]
    public Slider acSlider;
    public TextMeshProUGUI acText;
    [Header("스테이터스 표시")]
    public TextMeshProUGUI hpStatusText;
    public TextMeshProUGUI mpStatusText;
    public TextMeshProUGUI atkStatusText;
    public TextMeshProUGUI basicAtkStatusText;
    public TextMeshProUGUI defStatusText;
    public TextMeshProUGUI moveSpeedStatusText;
    [Header("프로필 스킬 슬롯 부모")]
    public GameObject slotParent;
    [Header("쿨타임")]
    public Slider[] coolTimeSlider;

    private void Awake()
    {
        Instance = this;
    }
}
