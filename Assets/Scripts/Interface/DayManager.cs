using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public enum Day
{
    day,
    night
}

public class DayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    public Button dayEndButton;
    public GameObject sunLight;
    public Vector3 daySunRotation;
    public Vector3 nightSunRotation;

    private StorageToInventory storageToInventory;
    private StoreManager storeManager;
    private PlayerProfile playerProfile;

    public Day curDay = Day.day;

    public static DayManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        storageToInventory = GameObject.Find("InventorySystem").GetComponent<StorageToInventory>();
        storeManager = GameObject.Find("InventorySystem").GetComponent<StoreManager>();
        playerProfile = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProfile>();
        dayEndButton.interactable = false;
        daySunRotation = new Vector3(50, -30, 0);
        nightSunRotation = new Vector3(-49, -195, -11);
    }

    public void DayEnd()
    {
        if (UIManager.Instance.inventory.currentUI == UIType.None)
        {
            dayEndButton.interactable = false;
            sunLight.transform.rotation = Quaternion.Euler(daySunRotation);
            curDay = Day.day;
            GameManager.instance.dayCount++;
            dayText.text = GameManager.instance.dayCount.ToString();
            GameManager.instance.dayEnd = true;
            storeManager.VillageStoreReset();
            playerProfile.ActCountReset();

            if (!GameManager.instance.dayTutorial)
            {
                if (!DialogueManager.instance.start)
                {
                    DialogueManager.instance.OnDialogue(UIManager.Instance.endExplainDialogue);
                    DialogueManager.instance.OnDialogueComplete += PortalZoom;
                    GameManager.instance.dayTutorial = true;
                }
            }
        }
    }

    private void PortalZoom()
    {
        Time.timeScale = 0;
        UIManager.Instance.portalDirector.Play();
        StartCoroutine(PlayAndCheck());
    }

    IEnumerator PlayAndCheck()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime((float)UIManager.Instance.storageDirector.duration);
        Time.timeScale = 1;
        DialogueManager.instance.OnDialogueComplete -= PortalZoom;
        UIManager.Instance.storageDirector.gameObject.SetActive(false);
    }
}
