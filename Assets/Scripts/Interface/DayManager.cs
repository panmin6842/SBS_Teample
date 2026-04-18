using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class DayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    public Button dayEndButton;
    public GameObject sunLight;
    public Vector3 daySunRotation;
    public Vector3 nightSunRotation;

    private StorageToInventory storageToInventory;

    public static DayManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        storageToInventory = GameObject.Find("InventorySystem").GetComponent<StorageToInventory>();
        dayEndButton.interactable = false;
        daySunRotation = new Vector3(50, -30, 0);
        nightSunRotation = new Vector3(-49, -195, -11);
    }

    public void DayEnd()
    {
        dayEndButton.interactable = false;
        sunLight.transform.rotation = Quaternion.Euler(daySunRotation);
        GameManager.instance.dayCount++;
        dayText.text = GameManager.instance.dayCount.ToString();
        GameManager.instance.dayEnd = true;

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

    private void VillageStoreReset()
    {

    }

    private void PortalZoom()
    {
        Time.timeScale = 0;
        UIManager.Instance.portalDirector.Play();
        StartCoroutine(PlayAndCheck());
    }

    IEnumerator PlayAndCheck()
    {
        yield return new WaitForSecondsRealtime((float)UIManager.Instance.storageDirector.duration);
        Time.timeScale = 1;
        DialogueManager.instance.OnDialogueComplete -= PortalZoom;
        UIManager.Instance.storageDirector.gameObject.SetActive(false);
    }
}
