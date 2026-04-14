using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardSlot : MonoBehaviour, IPointerClickHandler
{
    private StationLevelReward myData;

    [SerializeField] private TextMeshProUGUI levelText;

    public void Setup(StationLevelReward data)
    {
        myData = data;
        levelText.text = $"Lv. {data.level}";
        RefreshUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        RewardManager.instance.receiveButton.gameObject.SetActive(true);
        RewardManager.instance.skillPointText.gameObject.SetActive(true);

        RewardManager.instance.selectedLevel = myData.level;
        RewardManager.instance.currentSelectedSlot = this;
        RewardManager.instance.skillPointText.text = myData.rewards.ToString();

        RewardManager.instance.UpdateReceiveButtonState();
        RefreshUI();
    }

    public void RefreshUI()
    {
        //수령 시 체크 표시 등 연출
        bool alreadyGot = RewardManager.instance.receivedLevels.Contains(myData.level);
        RewardManager.instance.checkMark.SetActive(alreadyGot);
    }
}
