using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    public int selectedLevel = -1;
    public RewardSlot currentSelectedSlot;
    public Button receiveButton;
    public TextMeshProUGUI skillPointText;
    public GameObject checkMark; //수령 완료 표시

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform contentParent;
    public DataStationData stationData;
    public List<int> receivedLevels = new List<int>(); //이미 보상을 받은 레벨들 저장

    public static RewardManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateRewardList();
    }

    public void CreateRewardList()
    {
        //기존에 생성된 슬롯 있으면 삭제
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var rewardData in stationData.levelRewards)
        {
            GameObject go = Instantiate(slotPrefab, contentParent);

            RewardSlot slot = go.GetComponent<RewardSlot>();

            slot.Setup(rewardData);
        }
    }

    //슬롯 새로고침
    [ContextMenu("Refresh RewardSlot")]
    public void UpdateAllSlots()
    {
        RewardSlot[] slots = contentParent.GetComponentsInChildren<RewardSlot>();

        foreach (var slot in slots)
        {
            slot.RefreshUI();
        }
    }

    //보상을 받을 수 있는지 확인하는 함수
    public bool CanReceiveReward(int level)
    {
        int playerLevel = GameManager.instance.level;

        return playerLevel >= level && !receivedLevels.Contains(level);
    }

    //보상 받기
    public void GiveReward(int level)
    {
        StationLevelReward rewardInfo = stationData.levelRewards.Find(x => x.level == level);

        if (rewardInfo != null)
        {
            GameManager.instance.skillPoint += rewardInfo.rewards;

            receivedLevels.Add(level);

            if (currentSelectedSlot != null)
            {
                currentSelectedSlot.RefreshUI();
            }
        }
    }

    public void UpdateReceiveButtonState()
    {
        if (selectedLevel == -1)
        {
            receiveButton.interactable = false;
            return;
        }

        bool canGet = CanReceiveReward(selectedLevel);
        receiveButton.interactable = canGet;
    }

    public void OnClickReceiveButton()
    {
        if (selectedLevel != -1)
        {
            GiveReward(selectedLevel);
        }
    }
}
