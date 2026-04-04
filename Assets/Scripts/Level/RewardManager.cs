using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public DataStationData stationData;
    public List<int> receivedLevels = new List<int>(); //이미 보상을 받은 레벨들 저장

    public static RewardManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    //보상을 받을 수 있는지 확인하는 함수
    public bool CanReceiveReward(int level)
    {
        int playerLevel = GameManager.instance.playerLevel;

        return playerLevel >= level && !receivedLevels.Contains(level);
    }
}
